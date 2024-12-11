namespace Synergy
{
    partial class LauncherWindow
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
            this.components = new System.ComponentModel.Container();
            this.robloxTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.roundedProgressBar1 = new Synergy.Windows.Components.RoundedProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // robloxTimer
            // 
            this.robloxTimer.Enabled = true;
            this.robloxTimer.Interval = 1;
            this.robloxTimer.Tick += new System.EventHandler(this.RobloxTimer_Tick);
            // 
            // SuspendTimer
            // 
            this.SuspendTimer.Enabled = true;
            this.SuspendTimer.Interval = 500;
            this.SuspendTimer.Tick += new System.EventHandler(this.SuspendTimer_Tick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(32, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Checking Version .";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // roundedProgressBar1
            // 
            this.roundedProgressBar1.Location = new System.Drawing.Point(35, 117);
            this.roundedProgressBar1.Name = "roundedProgressBar1";
            this.roundedProgressBar1.Progress = 0;
            this.roundedProgressBar1.ProgressBarColor = System.Drawing.Color.Blue;
            this.roundedProgressBar1.RoundedBackgroundColor = System.Drawing.Color.LightGray;
            this.roundedProgressBar1.Size = new System.Drawing.Size(254, 10);
            this.roundedProgressBar1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(27, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Status";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LauncherWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(317, 159);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.roundedProgressBar1);
            this.Controls.Add(this.label1);
            this.Name = "LauncherWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LauncherWindow";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.roundedProgressBar1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer robloxTimer;
        private System.Windows.Forms.Timer SuspendTimer;
        private System.Windows.Forms.Label label1;
        private Windows.Components.RoundedProgressBar roundedProgressBar1;
        private System.Windows.Forms.Label label2;
    }
}