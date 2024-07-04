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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for AddWodPage.xaml
    /// </summary>
    public partial class AddWodPage : Window
    {
        private readonly IWodService _wodService;
        public AddWodPage(IWodService wodService)
        {
            _wodService = wodService;
            InitializeComponent();

            string[] comboDay = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            string[] comboType = new[] { "For Time", "EMOM", "AMRAP", "Dt" };

            Daycbx.ItemsSource = comboDay;
            Typecbx.ItemsSource = comboType;
        }

        public void AddWod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Wod wod = new();

                wod.Day = Daycbx.Text;
                wod.Type = Typecbx.Text;
                wod.Date = DateTime.Parse(Datedp.Text);
                wod.WOD = WODtxt.Text;
                wod.Time = Timetxt.Text;
                wod.Reps = Int32.Parse(Repstxt.Text);

                _wodService.CreateWod(wod);

                MessageBox.Show("Wod added successfully.");

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Return_To_CrossFitPage(object sender, EventArgs e)
        {
            CrossFitPage objCrossFitPage = new CrossFitPage(_wodService);
            this.Close();
            objCrossFitPage.Show();
        }

        private void ClearForm()
        {
            Daycbx.SelectedIndex = -1;
            Typecbx.SelectedIndex = -1;
            Datedp.SelectedDate = null;
            WODtxt.Clear();
            Timetxt.Clear();
            Repstxt.Clear();
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