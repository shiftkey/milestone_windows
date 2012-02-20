using System.Collections.Specialized;
using System.Threading;
using System.Windows;
using Caliburn.Micro;
using Milestone.Core.Models;
using MilestoneForWindows.Repositories;

namespace MilestoneForWindows.ViewModels
{
    public class ContextsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly UserRepository _users;

        public OverviewViewModel Overview { get; set; }

        public ContextsViewModel(UserRepository users)
        {
            _users = users;
            foreach (var u in _users)
                Open(new ContextViewModel {User = u});
            _users.CollectionChanged += UsersCollectionChanged;
        }

        private void UsersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (var i in e.NewItems)
                Open(new ContextViewModel { User = (User)i});
        }

        public void Open(IScreen screen)
        {
            if (screen is ContextViewModel)
            {
                if (Overview != null)
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() => Overview.Contexts.Add((ContextViewModel)screen)));
                }
            }
            else if (screen is OverviewViewModel)
            {
                Overview = (OverviewViewModel)screen;
            }

            Items.Add(screen);
            //ActivateItem(screen);
        }
    }
}