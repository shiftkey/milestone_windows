using System.ComponentModel.Composition.Hosting;
using Analects.SettingsService;
using Autofac;
using MilestoneForWindows.ViewModels;

namespace MilestoneForWindows
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
                .RegisterType<PullRequestRepository>()
                .AsSelf()
                .SingleInstance();
		}
	}
}

