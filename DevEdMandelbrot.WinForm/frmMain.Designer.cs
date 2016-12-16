namespace DevEdMandelbrot.WinForm
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbResults = new System.Windows.Forms.PictureBox();
            this.txtStartX = new System.Windows.Forms.TextBox();
            this.txtStartY = new System.Windows.Forms.TextBox();
            this.txtEndX = new System.Windows.Forms.TextBox();
            this.txtEndY = new System.Windows.Forms.TextBox();
            this.btnRender = new System.Windows.Forms.Button();
            this.btnSpawnRenderer = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbResults)).BeginInit();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbResults
            // 
            this.pbResults.BackColor = System.Drawing.Color.Black;
            this.pbResults.Location = new System.Drawing.Point(12, 12);
            this.pbResults.Name = "pbResults";
            this.pbResults.Size = new System.Drawing.Size(600, 600);
            this.pbResults.TabIndex = 0;
            this.pbResults.TabStop = false;
            this.pbResults.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbResults_MouseClick);
            this.pbResults.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResults_MouseMove);
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(619, 12);
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new System.Drawing.Size(190, 20);
            this.txtStartX.TabIndex = 1;
            // 
            // txtStartY
            // 
            this.txtStartY.Location = new System.Drawing.Point(619, 38);
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.Size = new System.Drawing.Size(190, 20);
            this.txtStartY.TabIndex = 1;
            // 
            // txtEndX
            // 
            this.txtEndX.Location = new System.Drawing.Point(619, 64);
            this.txtEndX.Name = "txtEndX";
            this.txtEndX.Size = new System.Drawing.Size(190, 20);
            this.txtEndX.TabIndex = 1;
            // 
            // txtEndY
            // 
            this.txtEndY.Location = new System.Drawing.Point(619, 90);
            this.txtEndY.Name = "txtEndY";
            this.txtEndY.Size = new System.Drawing.Size(190, 20);
            this.txtEndY.TabIndex = 1;
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(619, 117);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(190, 23);
            this.btnRender.TabIndex = 2;
            this.btnRender.Text = "Render";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // btnSpawnRenderer
            // 
            this.btnSpawnRenderer.Location = new System.Drawing.Point(620, 143);
            this.btnSpawnRenderer.Name = "btnSpawnRenderer";
            this.btnSpawnRenderer.Size = new System.Drawing.Size(190, 23);
            this.btnSpawnRenderer.TabIndex = 2;
            this.btnSpawnRenderer.Text = "Spawn Renderer";
            this.btnSpawnRenderer.UseVisualStyleBackColor = true;
            this.btnSpawnRenderer.Click += new System.EventHandler(this.btnSpawnRenderer_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(618, 172);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(190, 196);
            this.txtInfo.TabIndex = 1;
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 647);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(821, 22);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 669);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.btnSpawnRenderer);
            this.Controls.Add(this.btnRender);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.txtEndY);
            this.Controls.Add(this.txtEndX);
            this.Controls.Add(this.txtStartY);
            this.Controls.Add(this.txtStartX);
            this.Controls.Add(this.pbResults);
            this.Name = "frmMain";
            this.Text = "DevEdMandelbrot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbResults)).EndInit();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbResults;
        private System.Windows.Forms.TextBox txtStartX;
        private System.Windows.Forms.TextBox txtStartY;
        private System.Windows.Forms.TextBox txtEndX;
        private System.Windows.Forms.TextBox txtEndY;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.Button btnSpawnRenderer;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
    }
}

