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
using System.IO;
using System.Runtime.CompilerServices;

namespace Tema1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Administrare_Click(object sender, RoutedEventArgs e)
        {
            adminWindow adminWindow = new adminWindow();
            adminWindow.Show();
            this.Close();
        }

        private void Button_CautareCuvinte_Click(object sender, RoutedEventArgs e)
        {
            searchWindow searchWindow = new searchWindow();
            searchWindow.Show();
            this.Close();
        }

        private void Button_Divertisment_Click(object sender, RoutedEventArgs e)
        {
            entertainmentWindow entertainmentWindow = new entertainmentWindow();
            entertainmentWindow.Show();
            this.Close();
        }

    }
}
