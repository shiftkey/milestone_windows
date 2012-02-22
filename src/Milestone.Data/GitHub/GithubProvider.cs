using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Milestone.Core.Interfaces;

using NGitHub;
using NGitHub.Models;
using NGitHub.Web;
using RestSharp;

namespace Milestone.Core.GitHub
{

    public class GitHubProvider : IDvcsProvider
    {
        private GitHubClient _client;
        private readonly Action<GitHubException> _exceptionAction = ex => Console.WriteLine("Error: " + Enum.GetName(typeof(ErrorType), ex.ErrorType), "");

        private string _password;
        private string _username;
        public void Login(string username, string password)
        {
            _username = username;
            _password = password;
            _client = new GitHubClient
            {
                Authenticator = new HttpBasicAuthenticator(_username, _password)
            };
        }

        public Task<User> GetUser()
        {
            return NGitHub<User>(_client.Users.GetAuthenticatedUserAsync);
        }

        public Task<IEnumerable<User>> GetOrganisations(string username, int page=0)
        {
            return NGitHub<IEnumerable<User>, string, int>(_client.Organizations.GetOrganizationsAsync, username, page);
        }

        public Task<IEnumerable<Repository>> GetProjects(Models.User user, int page=0)
        {
            //if (user.IsOrganisation)
            //    return NGitHub<IEnumerable<Repository>, string, int, RepositoryTypes>(_client.Organizations., user.Username, page, RepositoryTypes.Member);
            //else
            return NGitHub<IEnumerable<Repository>, string, int, RepositoryTypes>(_client.Repositories.GetRepositoriesAsync, user.Username, page, RepositoryTypes.All);
        }

        public void GetProject()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Issue>> GetIssues(string username, string reponame, State state = State.Open, int page = 0)
        {
            return NGitHub<IEnumerable<Issue>, string, string, State, int>(_client.Issues.GetIssuesAsync, username, reponame, state, page);
        }

        public void GetIssue()
        {
            throw new NotImplementedException();
        }

        public void GetPullRequest()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PullRequest>> GetPullRequests(string username, string reponame, State state = State.Open, int page = 0)
        {
            return NGitHub<IEnumerable<PullRequest>, string, string, State, int>(_client.PullRequests.GetPullRequestsAsync, username, reponame, state, page);
        }

        private Task<T> NGitHub<T>(Func<Action<T>, Action<Exception>, GitHubRequestAsyncHandle> call)
        {
            var completionSource = new TaskCompletionSource<T>();
            call(completionSource.SetResult, completionSource.SetException);
            return completionSource.Task;
        }

        private Task<T> NGitHub<T, TArg>(Func<TArg, Action<T>, Action<Exception>, GitHubRequestAsyncHandle> call, TArg t1)
        {
            var completionSource = new TaskCompletionSource<T>();
            call(t1, completionSource.SetResult, completionSource.SetException);
            return completionSource.Task;
        }

        private Task<T> NGitHub<T, TArg, TArg2>(Func<TArg, TArg2, Action<T>, Action<Exception>, GitHubRequestAsyncHandle> call, TArg t1, TArg2 t2)
        {
            var completionSource = new TaskCompletionSource<T>();
            call(t1, t2, completionSource.SetResult, completionSource.SetException);
            return completionSource.Task;
        }

        private Task<T> NGitHub<T, TArg, TArg2, TArg3>(Func<TArg, TArg2, TArg3, Action<T>, Action<Exception>, GitHubRequestAsyncHandle> call, TArg t1, TArg2 t2, TArg3 t3)
        {
            var completionSource = new TaskCompletionSource<T>();
            call(t1, t2, t3, completionSource.SetResult, completionSource.SetException);
            return completionSource.Task;
        }

        private Task<T> NGitHub<T, TArg, TArg2, TArg3, TArg4>(Func<TArg, TArg2, TArg3, TArg4, Action<T>, Action<Exception>, GitHubRequestAsyncHandle> call, TArg t1, TArg2 t2, TArg3 t3, TArg4 t4)
        {
            var completionSource = new TaskCompletionSource<T>();
            call(t1, t2, t3, t4, completionSource.SetResult, completionSource.SetException);
            return completionSource.Task;
        }
    }
}
