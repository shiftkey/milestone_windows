using System;
using System.Reactive.Linq;
using Caliburn.Micro;
using Milestone.Core.Models;
using MilestoneForWindows.Collections;
using MilestoneForWindows.Repositories;

namespace MilestoneForWindows.ViewModels
{
    public class ContextViewModel : Screen
    {
        private readonly IssueRepository _allissues;
        public new string DisplayName { get { return User.Username; } }
        public User User { get; set; }
        public ThreadSafeObservableCollection<IssueViewModel> Issues { get; set; }

        public ContextViewModel(IssueRepository allissues, User user)
        {
            _allissues = allissues;
            User = user;
            Issues = new ThreadSafeObservableCollection<IssueViewModel>();

            _allissues
                .Where(x => x.Repo.Owner.Login == user.Username)
                .Subscribe(CheckInternal);
        }

        private void CheckInternal(IssueViewModel obj)
        {
            Issues.Add(obj);
        }
    }
}
