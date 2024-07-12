using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Domain.Models;

namespace TrainingApp
{
    public partial class Dashboard : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;

        public decimal CurrentWeightValue { get; private set; }
        private Wod currentRandomWod { get; set; }
        private bool isNavigating = false;
        public decimal? TotalCalories { get; set; }

        public Dashboard(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;

            InitializeComponent();

            GetLastWeightEntry();
            LoadWeightChart();
            LoadImage();
            LoadStreak();
            DisplayRandomWod();
            DisplayTotalCalories();
        }

        private void LoadWeightChart()
        {
            var weightEntries = _weightService.GetAllWeights();

            var dates = new List<string>();
            var values = new ChartValues<decimal>();

            foreach (var entry in weightEntries)
            {
                dates.Add(entry.Date.ToShortDateString());
                values.Add(entry.WeightAmount);
            }

            var maxWeight = (int)Math.Ceiling(values.Max());
            var minWeight = (int)Math.Floor(values.Min());

            var yAxisMax = maxWeight + 25;
            var yAxisMin = minWeight - 25;

            WeightChart.AxisY.Clear();
            WeightChart.AxisY.Add(new Axis
            {
                Title = "Weight",
                MinValue = yAxisMin,
                MaxValue = yAxisMax,
                LabelFormatter = value => value.ToString(),
                Separator = new LiveCharts.Wpf.Separator()
            });

            WeightChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = values,
                    PointGeometrySize = 15,
                    StrokeThickness = 3,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    Stroke = System.Windows.Media.Brushes.Blue
                }
            };

            WeightChart.AxisX.Clear();
            WeightChart.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = dates,
                Separator = new LiveCharts.Wpf.Separator()
            });
        }

        private void GetLastWeightEntry()
        {
            var weightEntries = _weightService.GetAllWeights();

            var lastWeightEntry = weightEntries.OrderByDescending(e => e.Date).FirstOrDefault();

            if (lastWeightEntry != null)
            {
                CurrentWeightValue = lastWeightEntry.WeightAmount;

                CurrentWeightLabel.Content = $"{CurrentWeightValue} kg";
            }
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

        private void DisplayRandomWod()
        {
            currentRandomWod = _wodService.GetRandomWod();

            if (currentRandomWod != null)
            {
                RandomWodLabel.Content = $"Random Wod:";
                WodTextBlock.Text = FormatWodText(currentRandomWod.WOD);
            }
            else
            {
                RandomWodLabel.Content = "Random Wod:";
                WodTextBlock.Text = " No WODs available";
            }

            WodTextBlock.MouseLeftButtonDown += Go_To_WodDetails;
        }

        private void Go_To_WodDetails(object sender, MouseButtonEventArgs e)
        {
            if (!isNavigating && currentRandomWod != null)
            {
                isNavigating = true;

                WodDetailsPage wodDetailsPage = new WodDetailsPage(currentRandomWod, _wodService, _runningSessionService, _weightService, _streakService);
                wodDetailsPage.Show();
                this.Close();
            }
        }

        private string FormatWodText(string description)
        {
            const int maxLength = 50;
            if (description.Length > maxLength)
            {
                List<string> lines = new List<string>();
                for (int i = 0; i < description.Length; i += maxLength)
                {
                    int length = Math.Min(maxLength, description.Length - i);
                    lines.Add(description.Substring(i, length));
                }
                return string.Join(Environment.NewLine, lines);
            }
            return description;
        }

        private void Generate_Random_WOD(object sender, EventArgs e)
        {
            DisplayRandomWod();
        }

        public void DisplayTotalCalories()
        {
            TotalCalories = CalculateTotalCaloriesOfRunningSessions();
            TotalCaloriesValueLabel.Content = TotalCalories.HasValue ? $"{TotalCalories} calories" : "N/A";
        }

        public decimal? CalculateTotalCaloriesOfRunningSessions()
        {
            decimal? totalCalories = 0;
            var runningSessions = _runningSessionService.GetAllRunningSessions();

            foreach (var session in runningSessions)
            {
                totalCalories += session.Cal;
            }

            return totalCalories;
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

        private void Navigate_EatingPage(object sender, RoutedEventArgs e)
        {
            EatingPage objEatingPage = new EatingPage(_wodService, _runningSessionService, _weightService, _streakService);
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
