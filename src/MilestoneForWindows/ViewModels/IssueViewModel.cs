using System.Collections.ObjectModel;
using System.ComponentModel;
using MilestoneForWindows.Models;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    public class IssueViewModel : INotifyPropertyChanged
    {
        //TODO: just import the values needed from these two POCOs. They don't implement INPC, so WPF will leak memory on them.
        public Issue Issue { get; set; }
        public Repository Repo { get; set; }

        public string RepoName { get { return Repo.Name; } }
        public ObservableCollection<CommentViewModel> Comments { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public IssueViewModel(Issue issue, Repository r)
        {
            Issue = issue;
            Repo = r;
        }
    }
}