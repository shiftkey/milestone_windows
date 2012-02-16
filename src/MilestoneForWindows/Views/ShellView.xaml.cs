using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace MilestoneForWindows.Views
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();

            var item = new MenuItem
                           {
                               Text = "Exit"
                           };

            item.Click += (sender, args) => this.Close();

            var ni = new NotifyIcon
                         {
                             //     Icon = new Icon(@"..\..\icon.ico"),
                             ContextMenu = new ContextMenu(new[] {item})
                         };

            ni.Click += NotifyIconClick;
            ni.Visible = true;
        }

        private void NotifyIconClick(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
                Hide();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
