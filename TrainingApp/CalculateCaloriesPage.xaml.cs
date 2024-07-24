using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Constants;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for CalculateCaloriesPage.xaml
    /// </summary>
    public partial class CalculateCaloriesPage : Window
    {
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IRunningSessionService _runningSessionService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;

        public CalculateCaloriesPage(INutritionService nutritionService, IWodService wodService, IWeightService weightService, IRunningSessionService runningSessionService, IStreakService streakService)
        {
            _nutritionService = nutritionService;
            _wodService = wodService;
            _weightService = weightService;
            _runningSessionService = runningSessionService;
            _streakService = streakService;

            InitializeComponent();

            ActivityLevelComboBox.ItemsSource = Enum.GetValues(typeof(ActivityLevel)).Cast<ActivityLevel>();
            CalorieMaintainceDisplay();
        }

        private void Calculate_Calories(object sender, RoutedEventArgs e)
        {

            var validationService = App.ServiceProvider.GetRequiredService<INutritionValidationService>();

            if (!validationService.ValidateWodForm(Agetxt.Text, Heighttxt.Text, ActivityLevelComboBox.Text, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string gender;

            if (MaleRadioButton.IsChecked == true)
            {
                gender = "M";
            }
            else if (FemaleRadioButton.IsChecked == true)
            {
                gender = "F";
            }
            else
            {
                MessageBox.Show("Please select a gender.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var weightEntries = _weightService.GetAllWeights();

            var lastWeightEntry = weightEntries.OrderByDescending(e => e.Date).FirstOrDefault();

            Nutrition nutrition = new();

            nutrition.Gender = gender;
            nutrition.Age = Int32.Parse(Agetxt.Text);
            nutrition.Weight = lastWeightEntry.WeightAmount;
            nutrition.Height = decimal.Parse(Heighttxt.Text);
            nutrition.ActivityLevel = ActivityLevelComboBox.SelectedIndex;

            _nutritionService.CalculateAndAddNutritionStats(nutrition);

            MessageBox.Show("Calories calculated successfully.");

            CalorieMaintainceDisplay();

            ClearForm();
        }

        private void Return_To_WeightPage(object sender, RoutedEventArgs e)
        {
            WeightPage objWeightPage = new WeightPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objWeightPage.Show();
            this.Close();
        }

        private void CalorieMaintainceDisplay()
        {
            var nutritionEntries = _nutritionService.GetAllNutritions();
            var lastNutritionEntry = nutritionEntries.OrderByDescending(n => n.Id).FirstOrDefault();

            if (lastNutritionEntry != null)
            {
                CaloriesLabel.Content = $"{lastNutritionEntry.Calories:F2}g";
                CarbsLabel.Content = $"{lastNutritionEntry.Carbs:F2}g";
                FatsLabel.Content = $"{lastNutritionEntry.Fat:F2}g";
                ProteinLabel.Content = $"{lastNutritionEntry.Protein:F2}g";
            }
            else
            {
                // Clear the labels if there is no data
                CaloriesLabel.Content = "N/A";
                CarbsLabel.Content = "N/A";
                FatsLabel.Content = "N/A";
                ProteinLabel.Content = "N/A";
            }
        }

        private void ClearForm()
        {
            MaleRadioButton.IsChecked = false;
            FemaleRadioButton.IsChecked = false;
            Agetxt.Text = string.Empty;
            Heighttxt.Text = string.Empty;
            ActivityLevelComboBox.SelectedIndex = -1;
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
