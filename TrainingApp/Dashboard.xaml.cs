using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingApp.Aplication.Interfaces;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        public Dashboard(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            InitializeComponent();
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

        private void Navigate_ToWeight_Page(object sender, RoutedEventArgs e)
        {
            WeightPage objWeightPage = new WeightPage(_wodService, _runningSessionService, _weightService);
            objWeightPage.Show();
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