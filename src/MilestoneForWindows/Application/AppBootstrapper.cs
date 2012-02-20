using System.ComponentModel.Composition.Hosting;
using Analects.SettingsService;
using Autofac;
using Milestone.Core.GitHub;
using MilestoneForWindows.Repositories;
using MilestoneForWindows.ViewModels;

namespace MilestoneForWindows.Application
{
    public class AppBootstrapper : Caliburn.Micro.Autofac.AutofacBootstrapper<ShellViewModel>
    {
        CompositionContainer _container;

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            //EnforceNamespaceConvention = false;
            AutoSubscribeEventAggegatorHandlers = true;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterType<SettingsService>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .RegisterType<IssueRepository>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<UserRepository>()
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<PullRequestRepository>()
                .AsSelf()
                .SingleInstance();


            builder
                .RegisterType<Storage>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<GitHubProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}

