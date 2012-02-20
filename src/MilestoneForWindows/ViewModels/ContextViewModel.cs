using System.Collections.ObjectModel;
using Caliburn.Micro;
using Milestone.Core.Models;
using MilestoneForWindows.Models;
namespace MilestoneForWindows.ViewModels
{
   
    public class ContextViewModel : Screen
    {
        public new string DisplayName { get { return User.Username; } }
        public User User { get; set; }
//        public ObservableCollection<Repo> Repositories { get; private set; }
        public ContextViewModel()
        {
  //          Repositories = new ObservableCollection<Repo>();
        }
    }
}
