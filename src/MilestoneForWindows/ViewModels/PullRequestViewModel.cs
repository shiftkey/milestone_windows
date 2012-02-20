using System.ComponentModel;
using MilestoneForWindows.Models;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    public class PullRequestViewModel : INotifyPropertyChanged
    {
        private readonly Repository _repo;
        public PullRequest Pull { get; set; }
        public string RepoName { get { return _repo.Name; } }

        public PullRequestViewModel(PullRequest pr, Repository repo)
        {
            _repo = repo;
            Pull = pr;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}