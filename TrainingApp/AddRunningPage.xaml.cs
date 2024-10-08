﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;

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
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public AddRunningPage(IRunningSessionService runningSessionService, IWodService wodService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
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
            RunningPage objRunningPage = new RunningPage(_runningSessionService, _wodService, _weightService, _streakService, _nutritionService);
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
