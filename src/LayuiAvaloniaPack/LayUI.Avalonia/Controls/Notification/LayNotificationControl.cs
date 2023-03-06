using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml.Templates;
using LayUI.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LayUI.Avalonia.Controls
{
    public class LayNotificationControl: TemplatedControl
    {
        private Button CloseButton;
        private LayNotificationHost Host;
        public LayNotificationControl() { 
        
        }
        public LayNotificationControl(LayNotificationHost host)
        {
            Host=host;
        }

        /// <summary>
        /// Defines the <see cref="Type"/> property.
        /// </summary>
        public static readonly StyledProperty<NotificationType> TypeProperty =
            AvaloniaProperty.Register<LayNotificationControl, NotificationType>(nameof(Type), NotificationType.Info);

        /// <summary>
        /// Comment
        /// </summary>
        public NotificationType Type
        {
            get { return GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            CloseButton = e.NameScope.Find<Button>("PART_CloseButton");
            if (CloseButton != null)
            {
                CloseButton.RemoveHandler(Button.ClickEvent, Closed);
                CloseButton.AddHandler(Button.ClickEvent, Closed);
            }
        }
        protected override void OnDetachedFromLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnDetachedFromLogicalTree(e); 
            if (CloseButton != null)
            {
                CloseButton.RemoveHandler(Button.ClickEvent, Closed); 
            }
        }
        private void Closed(object sender, RoutedEventArgs e)
        {
            Host.Items.Children.Remove(this);
        }
    }
}
