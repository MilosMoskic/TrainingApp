using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.UIComponents;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for CrossFitPage.xaml
    /// </summary>
    public partial class CrossFitPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public CrossFitPage(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            if (wodService == null) throw new ArgumentNullException(nameof(wodService));

            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
            InitializeComponent();
            PopulateWods();
        }

        private void PopulateWods()
        {
            try
            {
                List<Wod> wods = _wodService.GetAllWods();
                foreach (var wod in wods)
                {
                    UCWods ucWod = new UCWods(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
                    ucWod.DataContext = wod;

                    if (string.IsNullOrEmpty(wod.Time))
                        ucWod.Time = "N/A";
                    if (wod.Reps == null || wod.Reps == 0)
                        ucWod.Reps = "N/A";

                    ucWod.ParentWindow = this;
                    WodsListView.Items.Add(ucWod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating WODs: {ex.Message}");
            }
        }

        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboardPage = new Dashboard(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objDashboardPage.Show();
            this.Close();
        }

        private void Navigate_To_AddWodPage(object sender, EventArgs e)
        {
            AddWodPage objAddWodPage = new AddWodPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objAddWodPage.Show();
            this.Close();
        }

        private void Navigate_To_RunningPage(object sender, RoutedEventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService,_wodService, _weightService, _streakService, _nutritionService);
            objRunningPage.Show();
            this.Close();
        }

        private void Navigate_ToWeightPage(object sender, RoutedEventArgs e)
        {
            WeightPage objWeightPage = new WeightPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objWeightPage.Show();
            this.Close();
        }

        private void Navigate_To_EatingPage(object sender, RoutedEventArgs e)
        {
            EatingPage objEatingPage = new EatingPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objEatingPage.Show();
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
