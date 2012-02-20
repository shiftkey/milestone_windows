using System.Linq;
using Milestone.Core.Models;
using Raven.Client.Indexes;

namespace Milestone.Core.Indexes
{
    public class IssueByRepoId : AbstractIndexCreationTask<Issue>
    {
        public IssueByRepoId()
        {
            Map = docs => from d in docs
                          select new { d.RepoName, d.RepoOwner };
        }
    }
}
