using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayMessageControl : ContentControl
    {
        private LayMessageHost Host;
        private DispatcherTimer timer;
        private TimeSpan Time;
        public LayMessageControl(LayMessageHost host, TimeSpan time)
        {
            Host = host;
            Time = time;
        }
        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
            timer = new DispatcherTimer()
            {
                Interval = Time,
            };
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e);
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
                timer = null;
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer == null) return;
            timer.Stop();
            timer.Tick -= Timer_Tick;
            Host?.Items?.Children?.Remove(this);
        }
    }
}
