using System.Windows;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;
using TrainingApp.UIComponents;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for RunningPage.xaml
    /// </summary>
    public partial class RunningPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public RunningPage(IRunningSessionService runningSessionService, IWodService wodService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
            InitializeComponent();
            PopulateRunningSessions();
        }

        private void PopulateRunningSessions()
        {
            try
            {
                List<RunningSession> runningSessions = _runningSessionService.GetAllRunningSessions(); // Assuming GetAllWods() fetches data from repository
                foreach (var runningSession in runningSessions)
                {
                    UCRunningSession ucRunningSession = new UCRunningSession(_runningSessionService, _wodService, _weightService, _streakService, _nutritionService);
                    ucRunningSession.DataContext = runningSession; // Set DataContext of each UCWods instance to a Wod object
                    //ucRunningSession.ParentWindow = this;
                    RunningSessionsListView.Items.Add(ucRunningSession); // Add UCWods to the ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating Running Sessions: {ex.Message}");
            }
        }

        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboardPage = new Dashboard(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objDashboardPage.Show();
            this.Close();
        }

        private void Navigate_To_CrossFitPage(object sender, RoutedEventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objCrossFitPage.Show();
            this.Close();
        }

        private void Navigate_To_AddRunningSession(object sender, RoutedEventArgs e)
        {
            AddRunningPage objAddRunningPage = new AddRunningPage(_runningSessionService, _wodService, _weightService, _streakService, _nutritionService);
            objAddRunningPage.Show();
            this.Close();
        }

        private void Navigate_To_RunningPage(object sender, RoutedEventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService, _wodService, _weightService, _streakService, _nutritionService);
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
