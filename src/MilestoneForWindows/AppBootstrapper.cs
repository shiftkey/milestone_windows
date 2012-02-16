using System.ComponentModel.Composition.Hosting;
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
			//builder.RegisterModule<ServicesModule>();
			//builder.RegisterType<JumpListIntegration>().SingleInstance();
		}
	}
}

