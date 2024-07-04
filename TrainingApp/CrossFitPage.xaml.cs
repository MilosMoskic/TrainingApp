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
        private readonly IWodService _wodService;
        public CrossFitPage(IWodService wodService)
        {
            _wodService = wodService;
            InitializeComponent();
            PopulateWods();
        }

        private void PopulateWods()
        {
            List<Wod> wods = _wodService.GetAllWods(); // Assuming GetAllWods() fetches data from repository
            foreach (var wod in wods)
            {
                UCWods ucWod = new UCWods();
                ucWod.DataContext = wod; // Set DataContext of each UCWods instance to a Wod object
                WodsListView.Items.Add(ucWod); // Assuming WodsListView is your ListView in XAML
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
            Dashboard objDashboardPage = new Dashboard(_wodService);
            this.Close();
            objDashboardPage.Show();
        }

        private void Navigate_To_AddWodPage(object sender, EventArgs e)
        {
            AddWodPage objAddWodPage = new AddWodPage(_wodService);
            this.Close();
            objAddWodPage.Show();
        }
    }
}
