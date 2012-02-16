using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MilestoneForWindows.Aspects;
using NGitHub.Models;

namespace MilestoneForWindows.Models
{
    
    public class Repo : INotifyPropertyChanged
    {
        public Repository Repository { get; set; }
        public ObservableCollection<Issue> Issues { get; set; }
        public Dictionary<Issue, ObservableCollection<Comment>> IssueComments { get; set; }
        public RepoType Type { get; set; }

        public Repo()
        {
            Issues = new ObservableCollection<Issue>();
            IssueComments = new Dictionary<Issue, ObservableCollection<Comment>>();
            Type = RepoType.None;
        }

        public Repo(Repository repository)
            : this()
        {
            Repository = repository;
        }

        public Repo(Repository repository, IEnumerable<Issue> issues)
            :this()
        {
            Repository = repository;
            Issues = new ObservableCollection<Issue>(issues);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}