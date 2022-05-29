using WpfClient.Client.Interfaces;
using WpfClient.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfClient.Command;
using MetricsManagerClient.Data.Interfaces;

namespace WpfClient.ViewModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        #region ICommand список команд для view
        RelayCommand get_Metrics;
        public ICommand Get_Metrics => get_Metrics ??= new RelayCommand(GetMetrics);

        RelayCommand follow_The_Agent;
        public ICommand Follow_The_Agent => follow_The_Agent ??= new RelayCommand(FollowTheAgent);

        #endregion
        public MainWindowViewModel(){}
      
        private void GetMetrics(object obj)
        {
            MainWindow._appModel!.ManagerBaseAddress = ManagerAddress;
            var cpuMetrics = MainWindow._cpuClient?.GetMetricsFromAllCluster(new GetAllCpuMetricsRequest
            {
                FromTime = MainWindow._appModel.From,
                ToTime = MainWindow._appModel.To
            });
            if (cpuMetrics !=null)
            {
                var dotnetMetrics = MainWindow._dotnetClient?.GetMetricsFromAllCluster(new GetAllDotNetMetricsRequest
                {
                    FromTime = MainWindow._appModel.From,
                    ToTime = MainWindow._appModel.To
                });
                var hddMetrics = MainWindow._hddClient?.GetMetricsFromAllCluster(new GetAllHddMetricsRequest
                {
                    FromTime = MainWindow._appModel.From,
                    ToTime = MainWindow._appModel.To
                });
                var networkMetrics = MainWindow._networkClient?.GetMetricsFromAllCluster(new GetAllNetworkMetricsRequest
                {
                    FromTime = MainWindow._appModel.From,
                    ToTime = MainWindow._appModel.To
                });
                var ramMetrics = MainWindow._ramClient?.GetMetricsFromAllCluster(new GetAllRamMetricsRequest
                {
                    FromTime = MainWindow._appModel.From,
                    ToTime = MainWindow._appModel.To
                });
                MainWindow._cpuModel?.AddMetrics(cpuMetrics?.Metrics!);
                MainWindow._dotNetModel?.AddMetrics(dotnetMetrics?.Metrics!);
                MainWindow._hddModel?.AddMetrics(hddMetrics?.Metrics!);
                MainWindow._networkModel?.AddMetrics(networkMetrics?.Metrics!);
                MainWindow._ramModel?.AddMetrics(ramMetrics?.Metrics!);
            }
        }

        private void FollowTheAgent(object obj)
        {
            MainWindow._appModel!.IsFollowAgent = FollowAgentCheckBox;
        }

        private bool _followAgentCheckBox = false;

        public bool FollowAgentCheckBox
        {
            get
            {
                return _followAgentCheckBox;
            }
            set
            {
                _followAgentCheckBox = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        private string managerAddress = "http://localhost:44316";

        public string ManagerAddress
        {
            get { return managerAddress; }
            set { managerAddress = value;
                OnPropertyChanged();
            }
        }
    }
}
