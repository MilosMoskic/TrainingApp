using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp.UIComponents
{
    /// <summary>
    /// Interaction logic for UCWods.xaml
    /// </summary>
    public partial class UCWods : UserControl
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public UCWods(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
            InitializeComponent();
            DataContext = this;
        }

        public Window ParentWindow { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string Type { get; set; }
        public string Reps { get; set; }

        private void UCWod_Click(object sender, MouseButtonEventArgs e)
        {
            Wod selectedWod = DataContext as Wod;
            if (selectedWod != null)
            {
                WodDetailsPage wodDetailsPage = new WodDetailsPage(selectedWod, _wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
                wodDetailsPage.Show();
                ParentWindow.Close();
            }
        }
    }
}
