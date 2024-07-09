﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        public RunningPage(IRunningSessionService runningSessionService, IWodService wodService, IWeightService weightService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
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
                    UCRunningSession ucRunningSession = new UCRunningSession(_runningSessionService, _wodService, _weightService);
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
            Dashboard objDashboardPage = new Dashboard(_wodService, _runningSessionService, _weightService);
            objDashboardPage.Show();
            this.Close();
        }

        private void Navigate_To_CrossFitPage(object sender, RoutedEventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService);
            objCrossFitPage.Show();
            this.Close();
        }

        private void Navigate_To_AddRunningSession(object sender, RoutedEventArgs e)
        {
            AddRunningPage objAddRunningPage = new AddRunningPage(_runningSessionService, _wodService, _weightService);
            objAddRunningPage.Show();
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
