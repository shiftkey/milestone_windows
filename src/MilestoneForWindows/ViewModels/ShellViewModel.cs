using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using Analects.SettingsService;
using Caliburn.Micro;
using MilestoneForWindows.Events;
using MilestoneForWindows.Models;
using NGitHub;
using NGitHub.Models;
using NGitHub.Web;
using RestSharp;

namespace MilestoneForWindows.ViewModels
{
    public class ShellViewModel : Screen, IShell, IHandle<LoginEvent>
    {
        private readonly IssueRepository _issues;
        private readonly PullRequestRepository _pullrequests;
        private readonly ISettingsService _settings;
        public bool HasProgress { get; set; }
        public LoginViewModel Login { get; set; }
        public ContextsViewModel Contexts { get; set; }
        public bool IsAuthenticated { get; set; }

        private readonly GitHubClient _client;
        private readonly Action<GitHubException> _exceptionAction = ex => Console.WriteLine("Error: " + Enum.GetName(typeof(ErrorType), ex.ErrorType), "");

        public ShellViewModel(
            IssueRepository issues,
            PullRequestRepository pullrequests,
            ISettingsService settings,
            LoginViewModel login,
            ContextsViewModel contexts,
            OverviewViewModel overview)
        {
            _issues = issues;
            _pullrequests = pullrequests;
            _settings = settings;
            Login = login;
            Contexts = contexts;
            _client = new GitHubClient();
            DisplayName = "Milestone For Windows";
            HasProgress = false;

            if (settings.ContainsKey("username") && settings.ContainsKey("password"))
                PerformLogin(settings.Get<string>("username"), settings.Get<string>("password"));

            Contexts.Open(overview);
        }

        public void PerformLogin(string username, string password)
        {
            IsAuthenticated = true;
            _client.Authenticator = new HttpBasicAuthenticator(username, password);
            _settings.Set("username", username);
            _settings.Set("password", password);
            _settings.Save();
            GetContext();
        }

        public void Handle(LoginEvent message)
        {
            PerformLogin(message.Username, message.Password);
        }
        public void GetContext()
        {

            _client.Users.GetAuthenticatedUserAsync(u =>
                                                        {
                                                            var context = new ContextViewModel { User = u };
                                                            GetRepos(context, u);
                                                            Contexts.Open(context);

                                                            _client.Organizations.GetOrganizationsAsync(
                                                                u.Login,
                                                                0,
                                                                orgs =>
                                                                {
                                                                    foreach (var o in orgs)
                                                                    {
                                                                        var c = new ContextViewModel { User = o };
                                                                        GetRepos(c, o);
                                                                        Contexts.Open(c);
                                                                    }
                                                                },
                                                                _exceptionAction
                                                            );


                                                        },
                                                    _exceptionAction);
        }

        public void GetRepos(ContextViewModel context, User u)
        {
            _client.Repositories.GetRepositoriesAsync(u.Login, 0, repos => EndRepos(context, repos), _exceptionAction);
        }

        private void EndRepos(ContextViewModel context, IEnumerable<Repository> repos)
        {
            Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() =>
                                                                          {
                                                                              foreach (var r in repos)
                                                                              {
                                                                                  var repo = new Repo(r);
                                                                                  context.Repositories.Add(repo);

                                                                                  if (repo.Repository.HasIssues)
                                                                                      GetIssues(repo);

                                                                                  GetPullRequests(repo);
                                                                              }
                                                                          }));
        }

        private void GetPullRequests(Repo repo)
        {
            _client.PullRequests.GetPullRequestsAsync(
                repo.Repository.Owner.Login,
                repo.Repository.Name, State.Open,
                0,
                pullrequests => EndPullRequests(pullrequests, repo),
                _exceptionAction);
        }

        private void EndPullRequests(IEnumerable<PullRequest> pullrequests, Repo repo)
        {
            foreach (var pr in pullrequests)
            {
                _pullrequests.Add(new PullRequestViewModel(pr, repo));
            }
        }

        public void GetIssues(Repo r)
        {
            _client.Issues.GetIssuesAsync(r.Repository.Owner.Login, r.Repository.Name, State.Open, 0, issues => EndIssues(issues, r), _exceptionAction);
        }

        private void EndIssues(IEnumerable<Issue> issues, Repo r)
        {
            Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() =>
                                                                          {
                                                                              foreach (var i in issues)
                                                                              {
                                                                                  r.Issues.Add(i);
                                                                                  _issues.Add(new IssueViewModel(i, r));
                                                                              }
                                                                          }));
        }
    }
}
