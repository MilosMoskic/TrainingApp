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
using TrainingApp.Aplication.Services;
using TrainingApp.Domain.Models;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for WodDetailsPage.xaml
    /// </summary>
    public partial class WodDetailsPage : Window
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        private readonly IStreakService _streakService;
        public WodDetailsPage(Wod wod, IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService, IStreakService streakService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            _streakService = streakService;
            InitializeComponent();
            DataContext = wod;
        }

        private void DeleteWod_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Wod wod)
            {
                _wodService.DeleteWod(wod);
                Return_To_CrossFitPage(sender, e);
            }
        }

        private void UpdateWod_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Wod wod)
            {
                UpdateWodPage objUpdateWodPage = new UpdateWodPage(wod, _wodService, _runningSessionService, _weightService, _streakService);
                objUpdateWodPage.Show();
                this.Close();
            }
        }

        private void Return_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService, _runningSessionService, _weightService, _streakService);
            objCrossFitPage.Show();
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
