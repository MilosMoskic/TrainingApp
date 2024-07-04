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

namespace TrainingApp.UIComponents
{
    /// <summary>
    /// Interaction logic for UCWods.xaml
    /// </summary>
    public partial class UCWods : UserControl
    {
        public UCWods()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Time { get; set; }
        public string Day { get; set; }
        public string Type { get; set; }
        public string Reps { get; set; }
    }
}
