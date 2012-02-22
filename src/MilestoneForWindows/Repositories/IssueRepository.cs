using MilestoneForWindows.Application.Interfaces;
using MilestoneForWindows.ViewModels;

namespace MilestoneForWindows.Repositories
{
    public class IssueRepository : GenericRepository<IssueViewModel>
    {
        public IssueRepository(IStorage storage)
            : base(storage)
        {

        }
    }
}