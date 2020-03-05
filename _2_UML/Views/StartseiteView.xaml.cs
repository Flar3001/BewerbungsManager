using System;
using System.Collections.Generic;
using System.Globalization;
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
using _2_UML.Interfaces;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für StartseiteView.xaml
    /// </summary>
    public partial class StartseiteView : Page, IStartseiteView
    {
        public StartseiteView()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        public string Willkommenstext { get; private set; }

        //Entscheidet, on der Nutzer die Ausbildungsoptionen erhält
        public bool AusbilderAnsichtIstSichtbar { get; private set; }


        public event SeitenAnsicht ZeigeViewFertig;

        public event Logout LoeseLogoutAus;
        public event Firmen GeheZuFirmenUebersicht;
        public event Bewerbungen GeheZuBewerbungenUebersicht;
        public event Profil GeheZuTeilnehmerAnsicht;
        public event Teilnehmer GeheZuTeilnehmerUebersicht;
        public event Ausbilder GeheZuAusbilderUebersicht;
        public event Berufe GeheZuBerufeUebersicht;

        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        public void NameUndOptionenEinstellen(string Username, bool ErweiterteBerechtigungen)
        {
            Willkommenstext = $"Willkommen, {Username}";
            AusbilderAnsichtIstSichtbar = ErweiterteBerechtigungen;
        }


        /// <summary>
        /// Feuert das LoeseLogoutAus event ab, welches im Controller bereits mit der Methode LogoutAusführen verknüpft wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            LoeseLogoutAus();
        }

        private void FirmenUebersichtAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuFirmenUebersicht();
        }

        private void BewerbungenUebersichtAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuBewerbungenUebersicht();
        }

        private void ProfilAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuTeilnehmerAnsicht();
        }

        private void TeilnehmerUebersichtAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuTeilnehmerUebersicht();
        }

        private void AusbilderUebersichtAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuAusbilderUebersicht();
        }

        private void BerufeUebersichtAnzeigen(object sender, RoutedEventArgs e)
        {
            GeheZuBerufeUebersicht();
        }
    }

}
