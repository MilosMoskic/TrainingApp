using System;
using System.Collections.Generic;
using System.Data;
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
using TrainingApp.Domain.Models;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for WeightPage.xaml
    /// </summary>
    public partial class WeightPage : Window
    {
        private readonly IWodService _wodService;
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWeightService _weightService;
        public WeightPage(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService)
        {
            _wodService = wodService;
            _runningSessionService = runningSessionService;
            _weightService = weightService;
            InitializeComponent();
            updateDataGrid();
        }

        private void updateDataGrid()
        {
            var weights = _weightService.GetAllWeights();
            WeightsDataGrid.ItemsSource = weights;
        }

        private void AddWeight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (decimal.TryParse(Weighttxt.Text, out decimal weight))
                {
                    _weightService.CreateWeight(new Weight { WeightAmount = weight });
                    updateDataGrid();
                    Weighttxt.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter a valid weight.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding weight: {ex.Message}");
            }
        }

        private void DeleteWeight_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WeightsDataGrid.SelectedItem is Weight selectedWeight)
                {
                    var weightToDelete = _weightService.GetWeight(selectedWeight.Id);
                    _weightService.DeleteWeight(weightToDelete);
                    updateDataGrid();
                }
                else
                {
                    MessageBox.Show("Please select a weight to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting weight: {ex.Message}");
            }
        }


        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboardPage = new Dashboard(_wodService, _runningSessionService, _weightService);
            objDashboardPage.Show();
            this.Close();
        }

        private void Navigate_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService);
            objCrossFitPage.Show();
            this.Close();
        }

        private void Navigate_To_RunningPage(object sender, RoutedEventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService, _wodService, _weightService);
            objRunningPage.Show();
            this.Close();
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
    }
}
