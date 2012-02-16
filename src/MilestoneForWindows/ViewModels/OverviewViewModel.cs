using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;


namespace MilestoneForWindows.ViewModels
{
    public class OverviewViewModel : Screen
    {
        public override string DisplayName { get { return "Overview"; } }
        public ObservableCollection<ContextViewModel> Contexts { get; set; }
        public OverviewViewModel()
        {
            Contexts = new ObservableCollection<ContextViewModel>();
            //Contexts.SelectMany(c => c.Repositories).SelectMany(r => r.Issues);
        }
    }
}