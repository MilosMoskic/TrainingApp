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
using System.Windows.Shapes;
using TrainingApp.Aplication.Interfaces;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for CrossFitPage.xaml
    /// </summary>
    public partial class CrossFitPage : Window
    {
        private readonly IWodService _wodService;
        public CrossFitPage(IWodService wodService)
        {
            _wodService = wodService;
            InitializeComponent();
        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MinimizeApp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboardPage = new Dashboard(_wodService);
            this.Close();
            objDashboardPage.Show();
        }

        private void Navigate_To_AddWodPage(object sender, EventArgs e)
        {
            AddWodPage objAddWodPage = new AddWodPage(_wodService);
            this.Close();
            objAddWodPage.Show();
        }
    }
}
