using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
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
        public bool HasProgress { get; set; }
        public LoginViewModel Login { get; set; }
        public ContextsViewModel Contexts { get; set; }
        public bool IsAuthenticated { get; set; }

        private readonly GitHubClient _client;
        private readonly Action<GitHubException> _exceptionAction = ex => Console.WriteLine("Error: " + Enum.GetName(typeof (ErrorType), ex.ErrorType), "");
        
        public ShellViewModel(LoginViewModel login, ContextsViewModel contexts)
        {
            Login = login;
            Contexts = contexts;
            //Contexts.Contexts = new ObservableCollection<ContextViewModel>();
            _client = new GitHubClient();
            DisplayName = "Milestone For Windows";
            IsAuthenticated = false;
        }

        public void PerformLogin(string username, string password)
        {
            IsAuthenticated = true;
            _client.Authenticator = new HttpBasicAuthenticator(username, password);
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
                                                            var context = new ContextViewModel {User = u};
                                                            GetRepos(context, u);
                                                            Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() => Contexts.Open(context)), DispatcherPriority.Background);

                                                            _client.Organizations.GetOrganizationsAsync(
                                                                u.Login,
                                                                0,
                                                                orgs => Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() =>
                                                                            {
                                                                                foreach (var o in orgs)
                                                                                {
                                                                                    var c = new ContextViewModel {User = o};
                                                                                    GetRepos(c, o);
                                                                                    Contexts.Open(c);
                                                                                }
                                                                            }), DispatcherPriority.Background),
                                                                _exceptionAction
                                                            );

                                                            
                                                        },
                                                    _exceptionAction);
        }

        public  void GetRepos(ContextViewModel context, User u)
        {
            _client.Repositories.GetRepositoriesAsync(u.Login, 0,
                                          repos => Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() =>
                                                    {
                                                        foreach (var r in repos)
                                                        {
                                                            var repo = new Repo(r);
                                                            context.Repositories.Add(repo);

                                                            if (repo.Repository.HasIssues)
                                                                GetIssues(repo);
                                                        }
                                                    })),
                                          _exceptionAction);
            return;
            _client.Repositories.GetWatchedRepositoriesAsync(u.Login, 0,
                              repos =>
                              {
                                  Application.Current.Dispatcher.BeginInvoke((ThreadStart)(() =>
                                  {
                                      foreach (var r in repos)
                                          context.Repositories.Add(new Repo(r));
                                  }));
                              },
                              _exceptionAction);
        }

        public void GetIssues(Repo r)
        {
            _client.Issues.GetIssuesAsync(r.Repository.Owner.Login, r.Repository.Name, State.Open, 0,
                                          issues => Application.Current.Dispatcher.BeginInvoke((ThreadStart) (() =>
                                                                                                                  {
                                                                                                                      foreach (var i in issues)
                                                                                                                          r.Issues.Add(i);
                                                                                                                  })),
                                          _exceptionAction);
        }
    }
}
