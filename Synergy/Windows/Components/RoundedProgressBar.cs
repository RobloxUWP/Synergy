using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System;
using System.Diagnostics;

namespace Synergy.Windows.Components
{
    internal class RoundedProgressBar : Panel
    {
        private int progress = 0;
        private int progressMultiplier = 5;
        private int displayedProgress = 0;
        private readonly System.Timers.Timer animationTimer;

        public int Progress
        {
            get => progress / 5;
            set
            {
                progress = (value * progressMultiplier) < 0 ? 0 : (value * progressMultiplier) > (progressMultiplier * 100) ? (progressMultiplier * 100) : (value * progressMultiplier);
                StartAnimation();
            }
        }

        public Color ProgressBarColor { get; set; } = Color.Blue;
        public Color RoundedBackgroundColor { get; set; } = Color.LightGray;

        public RoundedProgressBar()
        {
            DoubleBuffered = true;
            animationTimer = new System.Timers.Timer(16); // ~60 FPS
            animationTimer.Elapsed += AnimateProgress;
        }

        private void StartAnimation()
        {
            animationTimer.Start();
        }

        private void AnimateProgress(object sender, ElapsedEventArgs e)
        {
            if (displayedProgress == progress)
            {
                animationTimer.Stop();
                return;
            }

            int maths = progress - displayedProgress;
            displayedProgress += Math.Min(Math.Sign(maths) * 5, maths);
            if (Math.Abs(progress - displayedProgress) < 2)
                displayedProgress = progress;

            Invoke((MethodInvoker)Invalidate);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var realBackgroundBrush = new SolidBrush(BackColor))
            using (var roundedBackgroundBrush = new SolidBrush(RoundedBackgroundColor))
            using (var progressBrush = new SolidBrush(ProgressBarColor))
            {
                var cornerRadius = Height / 2;

                var realBackgroundRect = new Rectangle(0, 0, Width, Height);
                var roundedBackgroundRect = new Rectangle(0, 0, Width, Height);
                var progressRect = new Rectangle(0, 0, Width * displayedProgress / (progressMultiplier * 100), Height);

                using (var roundedBackgroundPath = new GraphicsPath())
                using (var progressPath = new GraphicsPath())
                {
                    roundedBackgroundPath.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
                    roundedBackgroundPath.AddArc(Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
                    roundedBackgroundPath.AddArc(Width - cornerRadius * 2, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                    roundedBackgroundPath.AddArc(0, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                    roundedBackgroundPath.CloseFigure();

                    progressPath.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
                    progressPath.AddArc(progressRect.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
                    progressPath.AddArc(progressRect.Width - cornerRadius * 2, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                    progressPath.AddArc(0, Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
                    progressPath.CloseFigure();

                    g.FillRectangle(realBackgroundBrush, realBackgroundRect);
                    g.FillPath(roundedBackgroundBrush, roundedBackgroundPath);
                    if (displayedProgress > 0)
                        g.FillPath(progressBrush, progressPath);
                }
            }
        }
    }
}
