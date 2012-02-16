using System.Threading;
using System.Windows;
using Caliburn.Micro;

namespace MilestoneForWindows.ViewModels
{
    
    public class ContextsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public OverviewViewModel Overview { get; set; }

        public void Open(IScreen screen)
        {
            if (screen is ContextViewModel)
            {
                if (Overview != null)
                {
                    Application.Current.Dispatcher.BeginInvoke((ThreadStart) (() => Overview.Contexts.Add((ContextViewModel) screen)));
                }
            }
            else if (screen is OverviewViewModel)
            {
                Overview = (OverviewViewModel)screen;
            }

            ActivateItem(screen);
        }
    }
}