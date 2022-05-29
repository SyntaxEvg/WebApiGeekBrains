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

namespace WpfClient.UserControl
{
    /// <summary>
    /// Логика взаимодействия для Calendars.xaml
    /// </summary>
    public partial class Calendars : System.Windows.Controls.UserControl
    {
        public Calendars()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Calendar_SelectedDateFromChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDate = CalendarFrom.SelectedDate;

            if (selectedDate != null)
            {
                MainWindow._appModel!.From = (DateTimeOffset)selectedDate.Value.Date;
            }
        }
        private void Calendar_SelectedDateToChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDate = CalendarTo.SelectedDate;

            if (selectedDate != null)
            {
                MainWindow._appModel!.To = (DateTimeOffset)selectedDate.Value.Date;
            }
        }
    }
}
