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

namespace RadioactivityDecayCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml of the Radioactive Decay Calculator
    /// </summary>
    public partial class MainWindow : Window
    {
        NuclideManager nm;

        public MainWindow()
        {
            InitializeComponent();
            nm = new NuclideManager();
            dgNuclide.ItemsSource = nm.Nuclides;
            nm.NuclideChange += Nm_Nuclide_Change;
        }

        private void Nm_Nuclide_Change(object sender, EventArgs e)
        {
            tbTotalActivityDecay.Text = nm.GetCurrentMaxDecayTime().ToString();
        }

        private void New_Nuclide_Click(object sender, RoutedEventArgs e)
        {
            NewNuclideWindow inputDialog = new NewNuclideWindow();
            if (inputDialog.ShowDialog() == true)
                nm.AddNuclide(inputDialog.GetNuclide);
        }
    }
}
