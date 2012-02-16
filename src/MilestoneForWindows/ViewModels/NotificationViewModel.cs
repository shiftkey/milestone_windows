using Caliburn.Micro;

namespace MilestoneForWindows.ViewModels
{
    public class NotificationViewModel : Screen
    {
        public NotificationType Type { get; set; }
        public string Who { get; set; }
        public string When { get; set; }
        public string What { get; set; }
        public string Title { get; set; }
    }
}
