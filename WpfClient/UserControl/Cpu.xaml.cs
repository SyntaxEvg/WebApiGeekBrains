using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;


namespace WpfClient.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class Cpu : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public Cpu()
        {
            InitializeComponent();
            DataContext = this;
        }

        private SeriesCollection columnSeriesValues;

        public SeriesCollection ColumnSeriesValues
        {
            get
            {
                return new SeriesCollection
            {
                    new ColumnSeries
                    {
                        Values = new ChartValues<double> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 }
                    }
                };
            }
            set
            {
                columnSeriesValues = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateOnСlick(object sender, RoutedEventArgs e)
        {
           
                var metrics = MainWindow._cpuModel!.Metrics;
                ColumnSeriesValues.Clear();

                foreach (var metric in metrics)
                {
                  ColumnSeriesValues[0].Values.Add((double)metric.Value);
                }
                TimePowerChart.Update(true);
        }
    }
}
