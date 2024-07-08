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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Aplication.Services;
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
        public UCRunningSession(IRunningSessionService runningSessionService, IWodService wodService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            InitializeComponent();
            DataContext = this;
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
                    RunningPage runningPage = new RunningPage(_runningSessionService, _wodService);
                    runningPage.Show();
                    ParentWindow.Close();
                }
            }
        }
    }
}