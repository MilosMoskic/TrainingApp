﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TrainingApp.Aplication.Interfaces;

namespace TrainingApp
{
    public partial class EatingPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        private readonly INutritionService _nutritionService;
        public EatingPage(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService, INutritionService nutritionService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            _nutritionService = nutritionService;
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
            string dayOrDays = currentStreak == 1 ? "day" : "days";
            StreakTextBlock.Text = $"Current Streak: {currentStreak} {dayOrDays}";
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
            Dashboard objDashboard = new Dashboard(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objDashboard.Show();
            this.Close();
        }

        private void Navigate_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService, _streakService, _nutritionService);
            objCrossFitPage.Show();
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
    }
}
