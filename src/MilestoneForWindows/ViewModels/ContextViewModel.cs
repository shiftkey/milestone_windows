using System.Collections.ObjectModel;
using Caliburn.Micro;
using MilestoneForWindows.Models;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    
    public class ContextViewModel : Screen
    {
        public new string DisplayName { get { return User.Login; } }
        public User User { get; set; }
        public ObservableCollection<Repo> Repositories { get; private set; }
        public ContextViewModel()
        {
            Repositories = new ObservableCollection<Repo>();
        }
    }
}
