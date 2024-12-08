using System;
using System.Windows.Forms;

namespace Synergy.Windows
{
    public partial class Loader : Titlebar
    {
        public Timer Timer { get; set; }

        public Loader()
        {
            InitializeComponent();

            this.Resizable = false;

            this.Timer = new Timer();
            this.Timer.Interval = 2000;
            this.Timer.Tick += Timer_Tick;
            this.Timer.Start();

            roundedProgressBar1.Progress = 20;
        }

        private void Timer_Tick(object sender, EventArgs e) => roundedProgressBar1.Progress += 10;
    }
}
