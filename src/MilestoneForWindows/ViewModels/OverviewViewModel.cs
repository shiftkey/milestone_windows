using System.Collections.ObjectModel;
using Caliburn.Micro;
using MilestoneForWindows.Repositories;


namespace MilestoneForWindows.ViewModels
{
    public class OverviewViewModel : Screen
    {
        public IssueRepository Issues { get; set; }
        public PullRequestRepository PullRequests { get; set; }
        public override string DisplayName { get { return "Overview"; } }
        public ObservableCollection<ContextViewModel> Contexts { get; set; }

        public OverviewViewModel(IssueRepository issues, PullRequestRepository pullRequest)
        {
            Issues = issues;
            PullRequests = pullRequest;
            Contexts = new ObservableCollection<ContextViewModel>();
        }
    }
}