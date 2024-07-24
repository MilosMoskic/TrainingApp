using System.Windows;
using System.Windows.Controls;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.UIComponents
{
    /// <summary>
    /// Interaction logic for UCRunningSession.xaml
    /// </summary>
    public partial class UCRunningSession : UserControl
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public UCRunningSession(IRunningSessionService runningSessionService, IWodService wodService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
            InitializeComponent();
            DataContext = this;
            Loaded += UCRunningSession_Loaded;
        }

        private void UCRunningSession_Loaded(object sender, RoutedEventArgs e)
        {
            ParentWindow = Window.GetWindow(this);
        }

        public Window ParentWindow { get; set; }
        public int Time { get; set; }
        public decimal Distance { get; set; }
        public string Location { get; set; }
        public int? Cal { get; set; }

        private void DeleteRunningSession(object sender, RoutedEventArgs e)
        {
            if (DataContext is RunningSession runningSession)
            {
                var runningSessionToDelete = _runningSessionService.GetRunningSession(runningSession.Id);
                if(runningSessionToDelete != null)
                {
                    _runningSessionService.DeleteRunningSession(runningSession);
                    RunningPage runningPage = new RunningPage(_runningSessionService, _wodService, _weightService, _streakService, _nutritionService);
                    runningPage.Show();
                    ParentWindow?.Close();
                }
            }
        }
    }
}
