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
            this.Timer.Interval = 1000;
            this.Timer.Tick += Timer_Tick;
            this.Timer.Start();

            roundedProgressBar1.Progress = 1;
        }

        private void Timer_Tick(object sender, EventArgs e) => roundedProgressBar1.Progress += 10;
    }
}
