using System;
using System.ComponentModel;

namespace Milestone.Core.Models
{
    public abstract class GitHubItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        public string Title { get; set; }
        public string RepoName { get; set; }
        public string RepoOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}