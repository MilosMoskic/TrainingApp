using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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

        public Dashboard(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;

            InitializeComponent();

            GetLastWeightEntry();
            LoadWeightChart();
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

            // Update Y-axis range
            var maxWeight = (int)Math.Ceiling(values.Max());
            var minWeight = (int)Math.Floor(values.Min());

            var yAxisMax = maxWeight + 25; // Adjust to your desired range, e.g., 100
            var yAxisMin = minWeight - 25; // Adjust to your desired range, e.g., 50

            WeightChart.AxisY.Clear();
            WeightChart.AxisY.Add(new Axis
            {
                Title = "Weight",
                MinValue = yAxisMin,
                MaxValue = yAxisMax,
                LabelFormatter = value => value.ToString(), // Format labels as integers
                Separator = new LiveCharts.Wpf.Separator() // Optional: Customize separator if needed
            });

            WeightChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Weight",
                    Values = values,
                    PointGeometrySize = 15,
                    StrokeThickness = 3,
                    Fill = System.Windows.Media.Brushes.Transparent, // Ensure series fill is transparent
                    Stroke = System.Windows.Media.Brushes.Blue // Adjust stroke color if needed
                }
            };

            WeightChart.AxisX.Clear();
            WeightChart.AxisX.Add(new Axis
            {
                Title = "Date",
                Labels = dates,
                Separator = new LiveCharts.Wpf.Separator() // Optional: Customize separator if needed
            });
        }

        private void GetLastWeightEntry()
        {
            // Retrieve all weight entries
            var weightEntries = _weightService.GetAllWeights();

            // Find the last inserted weight entry
            var lastWeightEntry = weightEntries.OrderByDescending(e => e.Date).FirstOrDefault();

            if (lastWeightEntry != null)
            {
                // Update the current weight value
                CurrentWeightValue = lastWeightEntry.WeightAmount;

                // Update the label text directly
                CurrentWeightLabel.Content = $"{CurrentWeightValue} kg"; // Adjust label content as needed
            }
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
