using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System;

namespace Synergy.Windows.Components
{
    internal class RoundedProgressBar : Panel
    {
        private float progress = 0;
        private float displayedProgress = 0;
        private readonly System.Timers.Timer animationTimer;

        public int Progress
        {
            get => (int)progress;
            set
            {
                progress = value < 0 ? 0 : value > 100 ? 100 : value;
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
            if (Math.Abs(displayedProgress - progress) < 0.1f) // Allow for a small tolerance
            {
                displayedProgress = progress;
                animationTimer.Stop();
            }
            else
            {
                float step = Math.Sign(progress - displayedProgress) * 0.5f; // Smaller steps for smooth transition
                displayedProgress += step;
            }

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
                var progressRect = new Rectangle(0, 0, (int)(Width * displayedProgress / 100), Height);

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