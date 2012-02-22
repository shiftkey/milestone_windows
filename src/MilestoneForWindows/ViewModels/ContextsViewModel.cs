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
        private readonly IssueRepository _issues;

        public OverviewViewModel Overview { get; set; }

        public ContextsViewModel(UserRepository users, IssueRepository issues)
        {
            _users = users;
            _issues = issues;
            foreach (var u in _users)
                Open(new ContextViewModel(_issues, u));
            _users.CollectionChanged += UsersCollectionChanged;
        }

        private void UsersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            foreach (var i in e.NewItems)
                Open(new ContextViewModel(_issues, (User)i));
        }

        public void Open(IScreen screen)
        {
            if (screen is OverviewViewModel)
                Overview = (OverviewViewModel)screen;

            Items.Add(screen);
        }
    }
}