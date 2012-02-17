using System.ComponentModel;
using NGitHub.Models;

namespace MilestoneForWindows.ViewModels
{
    public class CommentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Comment _comment;

        public CommentViewModel(Comment comment)
        {
            _comment = comment;
        }

        
    }
}