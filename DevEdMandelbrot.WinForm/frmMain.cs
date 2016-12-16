using DevEdMandelbrot.Messages;
using EasyNetQ;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEdMandelbrot.WinForm
{
    public partial class frmMain : Form
    {
        private Viewport CurrentViewport;
        private Stack<Viewport> PreviousViewports;
        private IBus bus;
        private ConcurrentDictionary<Guid, Request> outstandingRequests = new ConcurrentDictionary<Guid, Request>();
        private Bitmap output;
        private Graphics g;
        private readonly object mutex = new object();
        private Stopwatch timer;
        private HashSet<string> participatingRenderers;
        private string[,] owners;

        public frmMain()
        {
            InitializeComponent();
            PreviousViewports = new Stack<Viewport>();
            SetViewport(new Viewport()
            {
                StartX = -2.1,
                StartY = -1.3,
                EndX = 1,
                EndY = 1.3
            });
            owners = new string[5, 5];
            bus = RabbitHutch.CreateBus("host=btn-mq-dev-cluster.15b.local;username=guest;password=guest;virtualHost=rg-test");
            bus.SubscribeAsync<Response>(Guid.NewGuid().ToString(), HandleMessage, cfg => cfg.WithAutoDelete());
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            Render();
        }

        private void Render()
        {
            output = new Bitmap(600, 600);
            g = Graphics.FromImage(output);

            owners = new string[5, 5];
            participatingRenderers = new HashSet<string>();
            timer = Stopwatch.StartNew();
            outstandingRequests.Clear();

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Send(new Request()
                    {
                        CorrelationId = Guid.NewGuid(),
                        Viewport = CurrentViewport,
                        TotalWidth = 600,
                        TotalHeight = 600,
                        RenderArea = new Rectangle(x * 120, y * 120, 120, 120)
                    });
                }
            }
        }

        private void Send(Request request)
        {
            outstandingRequests.Add(request.CorrelationId, request);
            bus.PublishAsync(request, cfg => cfg.WithExpires(5000));
        }

        private Task HandleMessage(Response response)
        {
            return Task.Run(() =>
            {
                lock (mutex)
                {
                    Request request;

                    if (outstandingRequests.TryGetValue(response.CorrelationId, out request))
                    {
                        try
                        {
                            var image = ImageHandling.ByteToImage(response.ImageData);

                            if (image.Width == 120 && image.Height == 120)
                            {
                                g.FillRectangle(Brushes.Aquamarine, request.RenderArea);
                                g.DrawImage(image, request.RenderArea.X, request.RenderArea.Y);

                                pbResults.InvokeAction(pb =>
                                {
                                    pb.Image = output;
                                });

                                txtInfo.InvokeAction(txt =>
                                {
                                    txt.Text = String.Format("Total {0:n0}ms\r\n\r\nRendered by:\r\n{1}", timer.ElapsedMilliseconds, String.Join("\r\n", participatingRenderers.Select(s => s).OrderBy(s => s)));
                                });

                                owners[request.RenderArea.X / 120, request.RenderArea.Y / 120] = response.ServerName;

                                participatingRenderers.Add(response.ServerName + " :-)");
                            }
                            else
                            {
                                participatingRenderers.Add(response.ServerName + " :-(");
                            }
                        }
                        catch (Exception ex)
                        {
                            participatingRenderers.Add(response.ServerName + " EXCEPTION");
                        }
                    }
                }
            });
        }

        private void pbResults_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PopViewport();
            }
            else if (e.Button == MouseButtons.Left)
            {
                var scale = 0.4;
                var width = CurrentViewport.EndX - CurrentViewport.StartX;
                var height = CurrentViewport.EndY - CurrentViewport.StartY;
                var cx = (double)e.X / (double)pbResults.Width;
                var cy = (double)e.Y / (double)pbResults.Height;
                var vx = CurrentViewport.StartX + (width * cx);
                var vy = CurrentViewport.StartY + (height * cy);

                SetViewport(new Viewport()
                {
                    StartX = vx - (vx - CurrentViewport.StartX) * scale,
                    StartY = vy - (vy - CurrentViewport.StartY) * scale,
                    EndX = vx + (CurrentViewport.EndX - vx) * scale,
                    EndY = vy + (CurrentViewport.EndY - vy) * scale
                });
            }

            Render();
        }

        private void btnSpawnRenderer_Click(object sender, EventArgs e)
        {
            var renderer = new frmRenderer();
            renderer.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bus.Dispose();
        }

        private void SetViewport(Viewport viewport)
        {
            PreviousViewports.Push(CurrentViewport);
            CurrentViewport = viewport;

            txtStartX.Text = viewport.StartX.ToString();
            txtStartY.Text = viewport.StartY.ToString();
            txtEndX.Text = viewport.EndX.ToString();
            txtEndY.Text = viewport.EndY.ToString();
        }

        private void PopViewport()
        {
            var previous = PreviousViewports.Pop();

            if (previous != null)
            {
                CurrentViewport = previous;

                txtStartX.Text = CurrentViewport.StartX.ToString();
                txtStartY.Text = CurrentViewport.StartY.ToString();
                txtEndX.Text = CurrentViewport.EndX.ToString();
                txtEndY.Text = CurrentViewport.EndY.ToString();
            }
        }

        private void pbResults_MouseMove(object sender, MouseEventArgs e)
        {
            StatusLabel.Text = String.Format("{0}x{1} {2}", e.X.ToString(), e.Y.ToString(), owners[e.X / 120, e.Y / 120] ?? "");
        }
    }
}
