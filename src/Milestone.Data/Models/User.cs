using System;
using System.ComponentModel;

namespace Milestone.Core.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string GravatarId { get; set; }
        public Uri Gravatar { get { return new Uri("http://www.gravatar.com/avatar/" + GravatarId); } }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}