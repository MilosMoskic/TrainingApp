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
    /// Interaction logic for CrossFitPage.xaml
    /// </summary>
    public partial class CrossFitPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        public CrossFitPage(IWodService wodService, IRunningSessionService runningSessionService)
        {
            if (wodService == null) throw new ArgumentNullException(nameof(wodService));

            _runningSessionService = runningSessionService;
            _wodService = wodService;
            InitializeComponent();
            PopulateWods();
        }

        private void PopulateWods()
        {
            try
            {
                List<Wod> wods = _wodService.GetAllWods(); // Assuming GetAllWods() fetches data from repository
                foreach (var wod in wods)
                {
                    UCWods ucWod = new UCWods(_wodService, _runningSessionService);
                    ucWod.DataContext = wod; // Set DataContext of each UCWods instance to a Wod object
                    ucWod.ParentWindow = this;
                    WodsListView.Items.Add(ucWod); // Add UCWods to the ListView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while populating WODs: {ex.Message}");
            }
        }

        private void Navigate_To_DashboardPage(object sender, EventArgs e)
        {
            Dashboard objDashboardPage = new Dashboard(_wodService, _runningSessionService);
            objDashboardPage.Show();
            this.Close();
        }

        private void Navigate_To_AddWodPage(object sender, EventArgs e)
        {
            AddWodPage objAddWodPage = new AddWodPage(_wodService, _runningSessionService);
            objAddWodPage.Show();
            this.Close();
        }

        private void Navigate_To_RunningPage(object sender, RoutedEventArgs e)
        {
            RunningPage objRunningPage = new RunningPage(_runningSessionService,_wodService);
            objRunningPage.Show();
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
