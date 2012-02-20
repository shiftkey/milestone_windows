using MilestoneForWindows.Application.Interfaces;
using MilestoneForWindows.ViewModels;

namespace MilestoneForWindows.Repositories
{
    public class PullRequestRepository : GenericRepository<PullRequestViewModel>
    {
        public PullRequestRepository(IStorage storage)
            : base(storage)
        {

        }
    }
}