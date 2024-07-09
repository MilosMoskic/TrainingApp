using System;
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
using TrainingApp.Domain.Models;

namespace TrainingApp.UIComponents
{
    /// <summary>
    /// Interaction logic for UCWods.xaml
    /// </summary>
    public partial class UCWods : UserControl
    {
        private readonly IRunningSessionService _runningSessionService;
        private readonly IWodService _wodService;
        private readonly IWeightService _weightService;
        public UCWods(IWodService wodService, IRunningSessionService runningSessionService, IWeightService weightService)
        {
            _runningSessionService = runningSessionService;
            _wodService = wodService;
            _weightService = weightService;
            InitializeComponent();
            DataContext = this;
        }

        public Window ParentWindow { get; set; }
        public string Time { get; set; }
        public string Day { get; set; }
        public string Type { get; set; }
        public string Reps { get; set; }

        private void UCWod_Click(object sender, MouseButtonEventArgs e)
        {
            Wod selectedWod = DataContext as Wod;
            if (selectedWod != null)
            {
                WodDetailsPage wodDetailsPage = new WodDetailsPage(selectedWod, _wodService, _runningSessionService, _weightService);
                wodDetailsPage.Show();
                ParentWindow.Close();
            }
        }
    }
}
