using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using WpfClient.Client.Interfaces;
using WpfClient.Client;
using AutoMapper;
using WpfClient.MappingSettings;
using Quartz.Spi;
using Quartz;
using WpfClient.Jobs.JobFactory;
using Quartz.Impl;
using WpfClient.Jobs;
using WpfClient.Jobs.Schedule;
using WpfClient.Services;
using System.Threading.Tasks;
using MetricsManagerClient.Data.Interfaces;
using MetricsManagerClient.Data.Models;
using MetricsManagerClient.Data;
using WpfClient.ViewModel;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private const string perjob = "0/5 * * * * ?";
		private IHost _host;

		public App()
		{
			var serviceCollection = new ServiceCollection();

			_host = new HostBuilder()
				.ConfigureServices((hostContext, services) =>
				{
					ConfigureServices(services);
				})
			.ConfigureLogging(logging =>
			{
				logging.AddDebug();
				logging.ClearProviders();
				logging.SetMinimumLevel(LogLevel.Error);
			})
			.UseNLog()
			.Build();
		}

		private void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<MainWindow>();
			services.AddSingleton<IAppModel, AppModel>();
			services.AddSingleton<ICpuMetricModel, CpuMetricModel>();
			services.AddSingleton<IDotNetMetricModel, DotNetMetricModel>();
			services.AddSingleton<IHddMetricModel, HddMetricModel>();
			services.AddSingleton<INetworkMetricModel, NetworkMetricModel>();
			services.AddSingleton<IRamMetricModel, RamMetricModel>();

			services.AddHttpClient<IAgentInfoClient, AgentInfoClient>();
			services.AddHttpClient<ICpuMetricsClient, CpuMetricsClient>();
			services.AddHttpClient<IDotNetMetricsClient, DotNetMetricsClient>();
			services.AddHttpClient<IHddMetricsClient, HddMetricsClient>();
			services.AddHttpClient<INetworkMetricsClient, NetworkMetricsClient>();
			services.AddHttpClient<IRamMetricsClient, RamMetricsClient>();
			ConfigureMapper(services);
			JobsSheduleSettings(services);
		}

		private void ConfigureMapper(IServiceCollection services)
		{
			var mapperConfiguration = new MapperConfiguration(
				mp => mp.AddProfile(new MapperProfile()));
			var mapper = mapperConfiguration.CreateMapper();
			services.AddSingleton(mapper);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			using (_host)			
				NLog.LogManager.Shutdown();
				await _host.StopAsync(TimeSpan.FromSeconds(5));
			base.OnExit(e);
		}

		private void JobsSheduleSettings(IServiceCollection services)
		{
			services.AddSingleton<IJobFactory, SingletonJobFactory>();
			services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

			services.AddSingleton<CpuMetricJob>();
			services.AddSingleton<DotNetMetricJob>();
			services.AddSingleton<HddMetricJob>();
			services.AddSingleton<NetworkMetricJob>();
			services.AddSingleton<RamMetricJob>();

			services.AddSingleton(new JobSchedule(typeof(CpuMetricJob),perjob));
			services.AddSingleton(new JobSchedule(typeof(DotNetMetricJob),perjob));
			services.AddSingleton(new JobSchedule(typeof(HddMetricJob),perjob));
			services.AddSingleton(new JobSchedule(typeof(NetworkMetricJob),perjob));
			services.AddSingleton(new JobSchedule(typeof(RamMetricJob), perjob));
			services.AddHostedService<QuartzHostedService>();

		}

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
			using var serviceScope = _host.Services.CreateScope();
				var services = serviceScope.ServiceProvider;
				try
				{
					await _host.StartAsync();
					var mainWindow = services.GetRequiredService<MainWindow>();
					mainWindow.Show();
				}
				catch (Exception)
				{
					Environment.Exit(0);
				}
			
		}
    }
}
