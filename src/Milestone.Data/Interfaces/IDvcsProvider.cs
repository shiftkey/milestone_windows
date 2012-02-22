using System.Collections.Generic;
using System.Threading.Tasks;
using NGitHub.Models;


namespace Milestone.Core.Interfaces
{
    public interface IDvcsProvider
    {
        void Login(string username, string password);
        
        Task<IEnumerable<User>> GetOrganisations(string username, int page = 0);
        Task<User> GetUser();
        Task<IEnumerable<Repository>> GetProjects(Milestone.Core.Models.User user, int page=0);
        Task<IEnumerable<Issue>> GetIssues(string username, string reponame, State state = State.Open, int page = 0);
        Task<IEnumerable<PullRequest>> GetPullRequests(string username, string reponame, State state = State.Open, int page = 0);

        void GetProject();
        void GetIssue();
        void GetPullRequest();
    }
}
