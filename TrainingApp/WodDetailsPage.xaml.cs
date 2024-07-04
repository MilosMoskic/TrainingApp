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
using TrainingApp.Aplication.Services;
using TrainingApp.Domain.Models;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for WodDetailsPage.xaml
    /// </summary>
    public partial class WodDetailsPage : Window
    {
        private readonly IWodService _wodService;
        public WodDetailsPage(Wod wod, IWodService wodService)
        {
            _wodService = wodService;
            InitializeComponent();
            DataContext = wod;
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

        private void Return_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService);
            this.Close();
            objCrossFitPage.Show();
        }
    }
}
