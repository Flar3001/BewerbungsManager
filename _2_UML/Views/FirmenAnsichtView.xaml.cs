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
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für FirmenAnsichtView.xaml
    /// </summary>
    public partial class FirmenAnsichtView : BasePage, IFirmenAnsichtView
    {
        public FirmenAnsichtView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public Firma AngezeigteFirma { get; private set; }

        private Abteilung ausgewaehlteAbteilung;
        public Abteilung AusgewaehlteAbteilung { get { return ausgewaehlteAbteilung; } private set { SetValue(ref ausgewaehlteAbteilung, value); } }

        private bool ansprechpartnerSichtbar = false;
        public bool AnsprechpartnerSichtbar { get { return ansprechpartnerSichtbar; } private set { SetValue(ref ansprechpartnerSichtbar, value); } }

        public event SeitenAnsicht ZeigeViewFertig;
        public event Zurueck GeheZuLetzteSeite;

        public void LoeseNeueSeiteAus()
        {
            throw new NotImplementedException();
        }

        public void ObjektAnzeigen(Firma firma)
        {
            AngezeigteFirma = firma;
        }

        private void LetzteSeite(object sender, RoutedEventArgs e)
        {
            GeheZuLetzteSeite();
        }


        private void NutzerAendern(object sender, RoutedEventArgs e)
        {

        }

        private void AbteilungAuswaehlen(object sender, RoutedEventArgs e)
        {
            Abteilung abteilung = (Abteilung)(sender as ListBox).SelectedItem;

            AusgewaehlteAbteilung = abteilung;
            AnsprechpartnerSichtbar = true;
        }


        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }
    }
}
