using System.Windows.Controls;
using System.Windows.Data;
using MilestoneForWindows.ViewModels;

namespace MilestoneForWindows.Views
{
    public partial class OverviewView
    {
        private CollectionViewSource _cvsPullRequests;
        private CollectionViewSource _cvs;
        public OverviewView()
        {
            InitializeComponent();
            Loaded += OverviewViewLoaded;
        }

        void OverviewViewLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _cvsPullRequests = Resources["cvsPullRequests"] as CollectionViewSource;
            _cvs = Resources["cvs"] as CollectionViewSource;
            _cvs.Filter += CvsFilter;
            _cvsPullRequests.Filter += CvsPullRequestsFilter;
        }

        private void TxtFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _cvs.View.Refresh();
            _cvsPullRequests.View.Refresh();
        }

        void CvsFilter(object sender, FilterEventArgs e)
        {
            var item = ((IssueViewModel)e.Item);
            e.Accepted = false;
            if (item.Issue.User.Login.Contains(txtFilter.Text))
            {
                e.Accepted = true;
                return;
            }
            else if (item.Issue.Title.Contains(txtFilter.Text))
            {
                e.Accepted = true;
                return;
            }
        }

        void CvsPullRequestsFilter(object sender, FilterEventArgs e)
        {
            var item = ((PullRequestViewModel) e.Item);
            e.Accepted = false;
            if (item.Pull.User.Login.Contains(txtFilter.Text))
            {
                e.Accepted = true;
                return;
            }
            else if (item.Pull.Title.Contains(txtFilter.Text))
            {
                e.Accepted = true;
                return;
            }
        }
    }
}
