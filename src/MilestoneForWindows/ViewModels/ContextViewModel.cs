using System.Collections.ObjectModel;
using Caliburn.Micro;
using MilestoneForWindows.Models;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    public class ContextViewModel : Screen
    {
        public User User { get; set; }
        public ObservableCollection<Repo> Repositories { get; private set; }
        public ContextViewModel()
        {
            Repositories = new ObservableCollection<Repo>();
        }
    }

    public class ContextsViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public void Open(IScreen screen)
        {
            ActivateItem(screen);
        }
        //public ObservableCollection<ContextViewModel> Contexts { get; set; }

    }
}
