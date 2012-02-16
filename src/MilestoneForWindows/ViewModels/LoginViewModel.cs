using Caliburn.Micro;
using MilestoneForWindows.Events;

namespace MilestoneForWindows.ViewModels
{
    
    public class LoginViewModel : Screen
    {
        private readonly IEventAggregator _eventAggregator;
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Signin()
        {
            _eventAggregator.Publish(new LoginEvent { Username = Username, Password = Password });
        }
    }
}
