using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Aplication.Services;

namespace TrainingApp
{
    public partial class EatingPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        public EatingPage(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            InitializeComponent();
            LoadStreak();
            LoadImage();
        }

        private void LoadImage()
        {
            if (_streakService.CalculateCurrentStreak() == 0)
            {
                ChangeImage("pack://application:,,,/images/firenocolor.jfif.png");
            }
            else
            {
                ChangeImage("pack://application:,,,/images/firecolor.jfif.png");
            }
        }

        private void ChangeImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
            bitmap.EndInit();
            DynamicImage.Source = bitmap;
        }

        private void LoadStreak()
        {
            int currentStreak = _streakService.CalculateCurrentStreak();
            StreakTextBlock.Text = $"Current Streak: {currentStreak} days";
        }

        private void IncreaseStreak_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _streakService.IncreaseStreak();
                LoadStreak();
                LoadImage();
                MessageBox.Show("Streak increased successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboard = new Dashboard(_wodService, _runningSessionService, _weightService, _streakService);
            objDashboard.Show();
            this.Close();
        }

        private void Navigate_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService, _streakService);
            objCrossFitPage.Show();
            this.Close();
        }

        private void Navigate_To_RunningPage(object sender, RoutedEventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService, _wodService, _weightService, _streakService);
            objRunningPage.Show();
            this.Close();
        }

        private void Navigate_ToWeightPage(object sender, RoutedEventArgs e)
        {
            WeightPage objWeightPage = new WeightPage(_wodService, _runningSessionService, _weightService, _streakService);
            objWeightPage.Show();
            this.Close();
        }
    }
}
