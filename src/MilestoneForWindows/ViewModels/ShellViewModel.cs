using Analects.SettingsService;
using Caliburn.Micro;
using Milestone.Core.Interfaces;
using MilestoneForWindows.Application.Interfaces;
using MilestoneForWindows.Events;
using MilestoneForWindows.Repositories;

namespace MilestoneForWindows.ViewModels
{
    public class ShellViewModel : Screen, IShell, IHandle<LoginEvent>
    {
        public bool HasProgress { get; set; }
        public LoginViewModel Login { get; set; }
        public ContextsViewModel Contexts { get; set; }
        public bool IsAuthenticated { get; set; }

        private readonly UserRepository _users;
        private readonly IssueRepository _issues;
        private readonly PullRequestRepository _pullrequests;
        private readonly ISettingsService _settings;
        private readonly IDvcsProvider _provider;

        public override string DisplayName
        {
            get { return "Milestone For Windows"; }
            set
            {
                base.DisplayName = value;
            }
        }

        public ShellViewModel(UserRepository users,
                                IssueRepository issues,
                                PullRequestRepository pullrequests,
                                ISettingsService settings,
                                LoginViewModel login,
                                ContextsViewModel contexts,
                                OverviewViewModel overview,
                                IStorage storage,
                                IDvcsProvider provider)
        {
            _users = users;
            _issues = issues;
            _pullrequests = pullrequests;
            _settings = settings;
            _provider = provider;
            Login = login;
            Contexts = contexts;
            HasProgress = false;

            Contexts.Open(overview);

            LoadData();

            if (settings.ContainsKey("username") && settings.ContainsKey("password"))
                PerformLogin(settings.Get<string>("username"), settings.Get<string>("password"));

        }

        public void PerformLogin(string username, string password)
        {
            IsAuthenticated = true;
            _provider.Login(username, password);
            _settings.Set("username", username);
            _settings.Set("password", password);
            _settings.Save();
            GetContext();
        }

        public void Handle(LoginEvent message)
        {
            PerformLogin(message.Username, message.Password);
        }

        public void LoadData()
        {
            return;
            _users.Load();

            foreach (var u in _users)
            {
                var context = new ContextViewModel { User = u };
                Contexts.Open(context);
            }
        }

        public async void GetContext()
        {
            var user = await _provider.GetUser();
            _users.Add(user);

            var orgs = await _provider.GetOrganisations(user.Login);

            foreach (var o in orgs)
                _users.Add(o);

            _users.Save();

            foreach (var u in _users)
            {
                var projects = await _provider.GetProjects(u.Username);
                foreach (var p in projects)
                {
                    if (!p.HasIssues)
                        continue;

                    var issues = await _provider.GetIssues(u.Username, p.Name);
                    foreach (var i in issues)
                    {
                        _issues.Add(new IssueViewModel(i, p));
                    }

                    var pullrequests = await _provider.GetPullRequests(u.Username, p.Name);
                    foreach (var pr in pullrequests)
                    {
                        _pullrequests.Add(new PullRequestViewModel(pr, p));
                    }
                }
            }
        }
    }
}
