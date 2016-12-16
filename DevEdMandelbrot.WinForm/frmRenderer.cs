using DevEdMandelbrot.Messages;
using EasyNetQ;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEdMandelbrot.WinForm
{
    public partial class frmRenderer : Form
    {
        private Color[] Palette;
        private IBus bus;

        public frmRenderer()
        {
            InitializeComponent();
            Palette = GetPalette();
            bus = RabbitHutch.CreateBus("host=btn-mq-dev-cluster.15b.local;username=guest;password=guest;virtualHost=rg-test");
            bus.SubscribeAsync<Request>("Renderer", HandleMessage);
        }

        private Color[] GetPalette()
        {
            Color[] colours = new Color[256];

            for (int i = 0; i < 256; i++)
            {
                colours[i] = Color.FromArgb(i, i, i);
            }

            return colours;
        }

        private Task HandleMessage(Request request)
        {
            textBox1.InvokeAction(tb => tb.AppendText(String.Format("{0}\r\n", request.CorrelationId)));

            return Task.Run(() =>
            {
                var timer = Stopwatch.StartNew();

                var image = new Bitmap(request.RenderArea.Width, request.RenderArea.Height);

                double x, y, x1, y1, xx, xmin, xmax, ymin, ymax = 0.0;

                int looper, s, z = 0;
                double integralX, integralY = 0.0;
                xmin = request.Viewport.StartX;
                ymin = request.Viewport.StartY;
                xmax = request.Viewport.EndX;
                ymax = request.Viewport.EndY;
                integralX = (xmax - xmin) / request.TotalWidth;
                integralY = (ymax - ymin) / request.TotalHeight;
                x = xmin;

                for (s = 0; s < request.TotalWidth; s++)
                {
                    y = ymin;

                    for (z = 0; z < request.TotalHeight; z++)
                    {
                        if (request.RenderArea.X <= s && s < request.RenderArea.X + request.RenderArea.Width &&
                            request.RenderArea.Y <= z && z < request.RenderArea.Y + request.RenderArea.Height)
                        {
                            // Within the viewport, so render
                            x1 = 0;
                            y1 = 0;
                            looper = 0;
                            while (looper < 100 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
                            {
                                looper++;
                                xx = (x1 * x1) - (y1 * y1) + x;
                                y1 = 2 * x1 * y1 + y;
                                x1 = xx;
                            }

                            // Get the percent of where the looper stopped
                            double perc = looper / (100.0);
                            // Get that part of a 255 scale
                            int val = ((int)(perc * 255));
                            // Use that number to set the color
                            image.SetPixel(s - request.RenderArea.X, z - request.RenderArea.Y, Palette[val]);
                        }

                        y += integralY;
                    }

                    x += integralX;

                }
                bus.Publish(new Response()
                {
                    CorrelationId = request.CorrelationId,
                    ImageData = ImageHandling.ImageToByte(image),
                    RenderTimeMs = timer.ElapsedMilliseconds,
                    ServerName = System.Environment.MachineName
                });
            });
        }

        private void frmRenderer_FormClosing(object sender, FormClosingEventArgs e)
        {
            bus.Dispose();
        }
    }
}
