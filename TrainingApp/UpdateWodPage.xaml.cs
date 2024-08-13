using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Constants;

namespace TrainingApp
{
    public partial class UpdateWodPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public UpdateWodPage(Wod wod, IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;

            InitializeComponent();
            DataContext = wod;

            Loaded += UpdateWodPage_Loaded;
        }

        private void UpdateWodPage_Loaded(object sender, RoutedEventArgs e)
        {
            string[] comboDay = new[] { "Monday", "Tuesday", "Wednesday", "Friday", "Saturday" };
            string[] comboType = new[] { "For Time", "EMOM", "AMRAP", "Dt" };

            Daycbx.ItemsSource = comboDay;
            Typecbx.ItemsSource = comboType;

            if (DataContext is Wod wod)
            {
                Daycbx.SelectedItem = wod.Day;
                Typecbx.SelectedItem = wod.Type;
                Datedp.SelectedDate = wod.Date;
                WODtxt.Text = wod.WOD;
                Timetxt.Text = wod.Time;
                Repstxt.Text = wod.Reps?.ToString();
            }
        }

        private void UpdateWod_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Wod wod)
            {
                var validationService = App.ServiceProvider.GetRequiredService<IWodValidationService>();

                if (!validationService.ValidateWodForm(Daycbx.Text, Typecbx.Text, Datedp.Text, WODtxt.Text, out string errorMessage))
                {
                    MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                wod.Day = Daycbx.SelectedItem.ToString();
                wod.Type = Typecbx.SelectedItem.ToString();
                wod.Date = Datedp.SelectedDate;
                wod.WOD = WODtxt.Text;
                wod.Time = Timetxt.Text;

                if (int.TryParse(Repstxt.Text, out int reps))
                {
                    wod.Reps = reps;
                }

                _wodService.UpdateWod(wod);

                MessageBox.Show("Wod updated successfully.");

                CancelUpdateWod_Click(sender, e);
            }
        }

        private void CancelUpdateWod_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Wod wod)
            {
                WodDetailsPage objCrossFitPage = new WodDetailsPage(wod, _wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
                objCrossFitPage.Show();
                this.Close();
            }
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
