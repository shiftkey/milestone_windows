using System.ComponentModel;
using MilestoneForWindows.Models;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    public class PullRequestViewModel : INotifyPropertyChanged
    {
        private readonly Repo _repo;
        public PullRequest Pull { get; set; }
        public string RepoName { get { return _repo.Repository.Name; } }

        public PullRequestViewModel(PullRequest pr, Repo repo)
        {
            _repo = repo;
            Pull = pr;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}