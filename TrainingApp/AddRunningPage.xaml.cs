using Microsoft.Extensions.DependencyInjection;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Aplication.Services;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for AddRunningPage.xaml
    /// </summary>
    public partial class AddRunningPage : Window
    {
        private readonly IWodService _wodService;
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWeightService _weightService;
        public AddRunningPage(IRunningSessionService runningSessionService, IWodService wodService, IWeightService weightService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            InitializeComponent();
        }

        public void AddRunningSession_Click(object sender, RoutedEventArgs e)
        {
            var validationService = App.ServiceProvider.GetRequiredService<IRunningSessionValidationService>();

            if (!validationService.ValidateWodForm(Timetxt.Text, Distancetxt.Text, Locationtxt.Text, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RunningSession runningSession = new();

            runningSession.Time = decimal.Parse(Timetxt.Text);
            runningSession.Distance = decimal.Parse(Distancetxt.Text);
            runningSession.Location = Locationtxt.Text;

            _runningSessionService.CreateRunningSession(runningSession);

            MessageBox.Show("Running Session added successfully.");

            ClearForm();
        }

        private void Return_To_RunningSessionPage(object sender, EventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService, _wodService, _weightService);
            objRunningPage.Show();
            this.Close();
        }

        private void ClearForm()
        {
            Timetxt.Clear();
            Distancetxt.Clear();
            Locationtxt.Clear();
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
