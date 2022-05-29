using WpfClient.Client.Interfaces;

using WpfClient.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient
{
    public partial class MainWindow : Window
    {
        public static IServiceProvider? _provider;
        public static ILogger? _logger;
        public static IAppModel? _appModel;
        public static ICpuMetricModel? _cpuModel;
        public static IDotNetMetricModel? _dotNetModel;
        public static IHddMetricModel? _hddModel;
        public static INetworkMetricModel? _networkModel;
        public static IRamMetricModel? _ramModel;
        public static ICpuMetricsClient? _cpuClient;
        public static IDotNetMetricsClient? _dotnetClient;
        public static IHddMetricsClient? _hddClient;
        public static INetworkMetricsClient? _networkClient;
        public static IRamMetricsClient? _ramClient;

        public MainWindow(ILogger<MainWindow> logger, IServiceProvider provider)
        {
            InitializeComponent();
            _logger = logger;
            _provider = provider;
            _appModel = _provider.GetService<IAppModel>()!;
            _cpuModel = _provider.GetService<ICpuMetricModel>()!;
            _dotNetModel = _provider.GetService<IDotNetMetricModel>()!;
            _hddModel = _provider.GetService<IHddMetricModel>()!;
            _networkModel = _provider.GetService<INetworkMetricModel>()!;
            _ramModel = _provider.GetService<IRamMetricModel>()!;
            _cpuClient = _provider.GetService<ICpuMetricsClient>()!;
            _dotnetClient = _provider.GetService<IDotNetMetricsClient>()!;
            _hddClient = _provider.GetService<IHddMetricsClient>()!;
            _networkClient = _provider.GetService<INetworkMetricsClient>()!;
            _ramClient = _provider.GetService<IRamMetricsClient>()!;
        }
    }
}
