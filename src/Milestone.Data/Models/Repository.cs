using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Milestone.Core.Models
{
    public class Repository : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Issue> Issues { get; set; }
        public ObservableCollection<PullRequest> PullRequests { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
    }
}
