using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.Infastructure.Constants;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for AddWodPage.xaml
    /// </summary>
    public partial class AddWodPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public AddWodPage(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
            InitializeComponent();


            Daycbx.ItemsSource = Enum.GetValues(typeof(DaysOfWeek)).Cast<DaysOfWeek>();
            Typecbx.ItemsSource = Enum.GetValues(typeof(WorkoutType)).Cast<WorkoutType>();
        }

        public void AddWod_Click(object sender, RoutedEventArgs e)
        {
            var validationService = App.ServiceProvider.GetRequiredService<IWodValidationService>();

            if (!validationService.ValidateWodForm(Daycbx.Text, Typecbx.Text, Datedp.Text, WODtxt.Text, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Wod wod = new();

            wod.Day = Daycbx.Text;
            wod.Type = Typecbx.Text;
            wod.Date = DateTime.Parse(Datedp.Text);
            wod.WOD = WODtxt.Text;
            if(Timetxt.Text == "")
            {
                wod.Time = null;
            }
            else
            {
                wod.Time = Timetxt.Text;
            }

            if(Repstxt.Text == null)
            {
                wod.Reps = Int32.Parse(Repstxt.Text);
            }
                
            _wodService.CreateWod(wod);

            MessageBox.Show("Wod added successfully.");

            ClearForm();
        }

        private void Return_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objCrossFitPage.Show();
            this.Close();
        }

        private void ClearForm()
        {
            Daycbx.SelectedIndex = -1;
            Typecbx.SelectedIndex = -1;
            Datedp.SelectedDate = null;
            WODtxt.Clear();
            Timetxt.Clear();
            Repstxt.Clear();
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
