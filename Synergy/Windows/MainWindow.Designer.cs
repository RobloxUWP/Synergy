namespace Synergy.Windows
{
    partial class MainWindow
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
            this.roundedPanelButton1 = new Synergy.Windows.Components.RoundedPanelButton();
            this.roundedPanelButton2 = new Synergy.Windows.Components.RoundedPanelButton();
            this.roundedPanelButton3 = new Synergy.Windows.Components.RoundedPanelButton();
            this.roundedPanelButton5 = new Synergy.Windows.Components.RoundedPanelButton();
            this.roundedPanelButton6 = new Synergy.Windows.Components.RoundedPanelButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LuaWebExecutor = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.LuaWebExecutor)).BeginInit();
            this.SuspendLayout();
            // 
            // TitlePanel
            // 
            this.TitlePanel.Size = new System.Drawing.Size(700, 32);
            // 
            // roundedPanelButton1
            // 
            this.roundedPanelButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedPanelButton1.ButtonText = "Execute";
            this.roundedPanelButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F);
            this.roundedPanelButton1.Location = new System.Drawing.Point(8, 286);
            this.roundedPanelButton1.Name = "roundedPanelButton1";
            this.roundedPanelButton1.Size = new System.Drawing.Size(79, 26);
            this.roundedPanelButton1.TabIndex = 1;
            this.roundedPanelButton1.Click += new System.EventHandler(this.roundedPanelButton1_Click);
            // 
            // roundedPanelButton2
            // 
            this.roundedPanelButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedPanelButton2.ButtonText = "Clear";
            this.roundedPanelButton2.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F);
            this.roundedPanelButton2.Location = new System.Drawing.Point(93, 286);
            this.roundedPanelButton2.Name = "roundedPanelButton2";
            this.roundedPanelButton2.Size = new System.Drawing.Size(79, 26);
            this.roundedPanelButton2.TabIndex = 2;
            this.roundedPanelButton2.Click += new System.EventHandler(this.roundedPanelButton2_Click);
            // 
            // roundedPanelButton3
            // 
            this.roundedPanelButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedPanelButton3.ButtonText = "Open File";
            this.roundedPanelButton3.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F);
            this.roundedPanelButton3.Location = new System.Drawing.Point(178, 286);
            this.roundedPanelButton3.Name = "roundedPanelButton3";
            this.roundedPanelButton3.Size = new System.Drawing.Size(79, 26);
            this.roundedPanelButton3.TabIndex = 3;
            this.roundedPanelButton3.Click += new System.EventHandler(this.roundedPanelButton3_Click);
            // 
            // roundedPanelButton5
            // 
            this.roundedPanelButton5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedPanelButton5.ButtonText = "Save File";
            this.roundedPanelButton5.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F);
            this.roundedPanelButton5.Location = new System.Drawing.Point(263, 286);
            this.roundedPanelButton5.Name = "roundedPanelButton5";
            this.roundedPanelButton5.Size = new System.Drawing.Size(79, 26);
            this.roundedPanelButton5.TabIndex = 5;
            this.roundedPanelButton5.Click += new System.EventHandler(this.roundedPanelButton5_Click);
            // 
            // roundedPanelButton6
            // 
            this.roundedPanelButton6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.roundedPanelButton6.ButtonText = "Attach";
            this.roundedPanelButton6.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F);
            this.roundedPanelButton6.Location = new System.Drawing.Point(615, 286);
            this.roundedPanelButton6.Name = "roundedPanelButton6";
            this.roundedPanelButton6.Size = new System.Drawing.Size(79, 26);
            this.roundedPanelButton6.TabIndex = 5;
            this.roundedPanelButton6.Click += new System.EventHandler(this.roundedPanelButton6_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panel1.Location = new System.Drawing.Point(573, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(121, 242);
            this.panel1.TabIndex = 6;
            // 
            // LuaWebExecutor
            // 
            this.LuaWebExecutor.AllowExternalDrop = true;
            this.LuaWebExecutor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LuaWebExecutor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.LuaWebExecutor.CreationProperties = null;
            this.LuaWebExecutor.DefaultBackgroundColor = System.Drawing.Color.White;
            this.LuaWebExecutor.Location = new System.Drawing.Point(8, 38);
            this.LuaWebExecutor.Name = "LuaWebExecutor";
            this.LuaWebExecutor.Size = new System.Drawing.Size(559, 242);
            this.LuaWebExecutor.TabIndex = 7;
            this.LuaWebExecutor.ZoomFactor = 1D;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 320);
            this.Controls.Add(this.LuaWebExecutor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundedPanelButton6);
            this.Controls.Add(this.roundedPanelButton5);
            this.Controls.Add(this.roundedPanelButton3);
            this.Controls.Add(this.roundedPanelButton2);
            this.Controls.Add(this.roundedPanelButton1);
            this.MinimumSize = new System.Drawing.Size(700, 320);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.Controls.SetChildIndex(this.TitlePanel, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton1, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton2, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton3, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton5, 0);
            this.Controls.SetChildIndex(this.roundedPanelButton6, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.LuaWebExecutor, 0);
            ((System.ComponentModel.ISupportInitialize)(this.LuaWebExecutor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Components.RoundedPanelButton roundedPanelButton1;
        private Components.RoundedPanelButton roundedPanelButton2;
        private Components.RoundedPanelButton roundedPanelButton3;
        private Components.RoundedPanelButton roundedPanelButton5;
        private Components.RoundedPanelButton roundedPanelButton6;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Web.WebView2.WinForms.WebView2 LuaWebExecutor;
    }
}