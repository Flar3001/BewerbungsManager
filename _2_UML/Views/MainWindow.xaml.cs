using _2_UML.Interfaces;
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
using System.Windows.Shapes;
using _2_UML.Controller;
using System.Diagnostics;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für MainVindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public event SeitenAnsicht ZeigeViewFertig;

        public void ZeigeView()
        {
            this.Show();

            //ACHTUNG: Es ist zwar nicht MVP-Konform, dass dieses View einen Controller kennt, jedoch ist dies in WPF nicht anders möglich. Werden MainWindow und Login
            //als eine Einheit betrachtet, ist MVP wieder vorhanden
            LoginController login = new LoginController();
        }

        public void ChangeFrameContent(Page newPage)
        {
            MainFrame.Content = newPage;
        }
    }
}
