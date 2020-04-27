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
    /// Interaktionslogik für NutzerAnsichtView.xaml
    /// </summary>
    public partial class NutzerAnsichtView : BasePage, INutzerAnsichtView
    {
        public NutzerAnsichtView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string Vorname { get; private set; }
        public string Nachname { get; private set; }
        public string Telefonnummer { get; private set; }
        public string E_Mail { get; private set; }
        public string Ort { get; private set; }
        public string Postleitzahl { get; private set; }
        public string Straße { get; private set; }
        public string Hausnummer { get; private set; }
        public string Ausbilder { get; private set; }
        public string Land { get; private set; }

        public bool TeilnehmerAnsicht { get; private set; }
        //Bestimmt, ob es sich bei dem angezeigten Profil um das eigene Profil des Benutzers handelt und der Ändern-Button angezeigt wird
        public bool IstNutzerProfil { get; private set; }


        public event SeitenAnsicht ZeigeViewFertig;

        public event Zurueck GeheZuLetzteSeite;
        public event Aendern GeheZuNutzerAendern;


        public void DatenFestlegen(Models.Ausbilder nutzer, bool eigenesProfil)
        {
            Vorname = nutzer.Vorname;
            Nachname = nutzer.Name;
            Telefonnummer = nutzer.Telefonnummer;
            E_Mail = nutzer.EMail;

            TeilnehmerAnsicht = false;
            IstNutzerProfil = eigenesProfil;
        }

        public void DatenFestlegen(Models.Teilnehmer nutzer, bool eigenesProfil)
        {
            Vorname = nutzer.Vorname;
            Nachname = nutzer.Name;
            Telefonnummer = nutzer.Telefonnummer;
            E_Mail = nutzer.EMail;
            Ort = nutzer.Adresse.Ort;
            Postleitzahl = nutzer.Adresse.Postleitzahl;
            Straße = nutzer.Adresse.Straße;
            Hausnummer = nutzer.Adresse.Hausnummer;
            Ausbilder = $"{nutzer.Ausbilder.Vorname} {nutzer.Ausbilder.Name}";
            Land = nutzer.Adresse.Land;

            TeilnehmerAnsicht = true;
            IstNutzerProfil = eigenesProfil;
        }

        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        private void NutzerAendern(object sender, RoutedEventArgs e)
        {
            GeheZuNutzerAendern();
        }

        private void LetzteSeite(object sender, RoutedEventArgs e)
        {
            GeheZuLetzteSeite();
        }
    }
}
