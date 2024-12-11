using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Synergy.SDK;

namespace Synergy.Windows.Components
{
    public class ToggleSetting : Panel
    {
        private bool isChecked;
        private string content;
        private Timer animationTimer;
        private int animationStep;
        private int currentX = -1;
        private int targetX;
        private const int AnimationSpeed = 3;

        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                currentX = -1;
                Invalidate();
            }
        }

        public EventHandler<ToggleCheckEventArgs> OnCheck;

        public ToggleSetting()
        {
            this.Paint += ToggleSetting_Paint;
            this.MouseClick += ToggleSetting_MouseClick;

            this.DoubleBuffered = true;

            animationTimer = new Timer();
            animationTimer.Interval = 10;
            animationTimer.Tick += AnimationTimer_Tick;

            isChecked = false;
            targetX = currentX;
        }

        private void ToggleSetting_MouseClick(object sender, MouseEventArgs e)
        {
            isChecked = !isChecked;

            var startX = isChecked ? this.Width - 46 + 2 : this.Width - 21;
            targetX = isChecked ? this.Width - 21 : this.Width - 46 + 2;

            animationStep = (targetX > startX) ? AnimationSpeed : -AnimationSpeed;
            currentX = startX;

            OnCheck?.Invoke(this, new ToggleCheckEventArgs(isChecked));

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            currentX += animationStep;

            if ((animationStep > 0 && currentX >= targetX) || (animationStep < 0 && currentX <= targetX))
            {
                currentX = targetX;
                animationTimer.Stop();
            }

            Invalidate();
        }

        private void ToggleSetting_Paint(object sender, PaintEventArgs e)
        {
            if (currentX == -1)
                currentX = isChecked ? this.Width - 21 : this.Width - 46 + 2;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var toggleBoxRect = new Rectangle(this.Width - 46, this.Height / 2 - 10, 45, 20);

            using (GraphicsPath path = Utils.GetRoundedRectangle(toggleBoxRect, 20))
            {
                e.Graphics.FillPath(isChecked ? Brushes.Green : Brushes.Gray, path);
            }

            var circleDiameter = 18;
            var circleRect = new Rectangle(currentX, this.Height / 2 - 9, circleDiameter, circleDiameter);

            e.Graphics.FillEllipse(Brushes.White, circleRect);

            var strMsre = e.Graphics.MeasureString(content, Font);
            e.Graphics.DrawString(content, Font, Brushes.White, 0, (this.Height / 2) - (strMsre.Height / 2));
        }

        public string Content
        {
            get => content;
            set
            {
                content = value;
                Invalidate();
            }
        }
    }

    [ComVisible(true)]
    public class ToggleCheckEventArgs : EventArgs
    {
        private readonly bool currentValue;

        public bool CurrentValue => currentValue;

        public ToggleCheckEventArgs(bool currentValue) => this.currentValue = currentValue;
    }
}