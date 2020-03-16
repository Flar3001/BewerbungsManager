using _2_UML.Interfaces;
using _2_UML.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für NutzerAnlegenBearbeitenView2.xaml
    /// </summary>
    /// <summary>
    /// Interaktionslogik für NutzerAnlegenBearbeitenView.xaml
    /// </summary>
    public partial class NutzerAnlegenBearbeitenView2 : BasePage, INutzerAnlegenBearbeitenView
    {
        public NutzerAnlegenBearbeitenView2()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event SeitenAnsicht ZeigeViewFertig;
        public event GeheZurueck GeheZurueck;
        public event SpeichereNeuenAusbilder SpeichereNeuenAusbilder;
        public event SpeichereNeuenTeilnehmer SpeichereNeuenTeilnehmer;
        public event SpeichereAenderungenAusbilder SpeichereAenderungenAusbilder;
        public event SpeichereAenderungenTeilnehmer SpeichereAenderungenTeilnehmer;


        //ACHTUNG: AktuellerNutzer ist NICHT UNBEDINGT EIN TEILNEHMER!!! Es kann sich auch um einen Ausbilder handeln. 
        //Dies ist eine Preview darauf, wie die Versionsverwaltung in Zukunft aussehen wird
        private Models.Teilnehmer aktuellerNutzer;
        public Models.Teilnehmer AktuellerNutzer { get { return aktuellerNutzer; } set { SetValue(ref aktuellerNutzer, value); } }


        public List<Models.Ausbilder> AlleAusbilder { get; private set; }
        public List<Sicherheitsfrage> AlleSicherheitsfragen { get; private set; }
        public List<Beruf> AlleBerufe { get; private set; }

        public bool IstTeilnehmer { get; private set; }
        public bool WirdNeuErschaffen { get; set; }
        public bool WirdNichtNeuErschaffen { get { return !WirdNeuErschaffen; } }

        public string Ueberschrift
        {
            get
            {
                string nutzer;
                string aktion;

                //Nutzer = (IstTeilnehmer) ? "Teilnehmer" : "Ausbilder";
                
                nutzer = AktuellerNutzer.Nutzer.Nutzertyp.Typ;
                aktion = (WirdNeuErschaffen) ?"erstellen" : "bearbeiten";
                return $"{nutzer} {aktion}";
            }
        }


        public void DatenAnzeigen(Models.Ausbilder ausbilder, bool neuErschaffen, List<Sicherheitsfrage> AlleSicherheitsfragen)
        {

            AktuellerNutzer = new Models.Teilnehmer
            {
                Id = ausbilder.Id,
                Vorname=ausbilder.Vorname,
                Name=ausbilder.Name,
                Telefonnummer=ausbilder.Telefonnummer,
                EMail=ausbilder.EMail,
                Nutzer=ausbilder.Nutzer,
            };
            WirdNeuErschaffen = neuErschaffen;
            this.AlleSicherheitsfragen = AlleSicherheitsfragen;
            IstTeilnehmer = false;
        }

        public void DatenAnzeigen(Models.Teilnehmer teilnehmer, bool neuErschaffen, List<Models.Ausbilder> AlleAusbilder, List<Sicherheitsfrage> AlleSicherheitsfragen, List<Beruf> AlleBerufe)
        {
            AktuellerNutzer = teilnehmer;
            WirdNeuErschaffen = neuErschaffen;
            this.AlleSicherheitsfragen = AlleSicherheitsfragen;
            this.AlleAusbilder = AlleAusbilder;
            this.AlleBerufe = AlleBerufe;
            this.IstTeilnehmer = true;
        }


        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        private void AenderungenSpeichernButton(object sender, RoutedEventArgs e)
        {
            if (IstInputOK())
            {
                if (AktuellerNutzer.Nutzer.Nutzertyp.Typ == "Teilnehmer")
                {
                    SpeichereAenderungenTeilnehmer(AktuellerNutzer);
                }
                else
                {
                    SpeichereAenderungenAusbilder(ErschaffeAusbilder());
                }
            }
            else
            {
                ZeigeFehlermeldung("Bitte füllen Sie alle Felder vollständig aus");
            }
        }

        private void AendernAbbrechenButton(object sender, RoutedEventArgs e)
        {
            GeheZurueck();
        }

        private void NeuenNutzerSpeichernButton(object sender, RoutedEventArgs e)
        {
            if (IstInputOK())
            {
                if (Passwort.Password == Antwort.Text)
                {
                    ZeigeFehlermeldung("Passwort und Sicherheitsantwort dürfen nicht gleich sein");
                }
                else if (Passwort.Password != Passwort_bestaetigen.Password || Antwort.Text != Antwort_bestaetigen.Text)
                {
                    ZeigeFehlermeldung("Bitte überprüfen Sie Passwort und Sicherheitsantwort auf Differenzen");
                }
                else
                {
                    if (AktuellerNutzer.Nutzer.Nutzertyp.Typ == "Teilnehmer")
                    {
                        SpeichereNeuenTeilnehmer(AktuellerNutzer);
                    }
                    else
                    {
                        SpeichereNeuenAusbilder(ErschaffeAusbilder());
                    }                
                }
            }
            else
            {
                ZeigeFehlermeldung("Bitte füllen Sie alle Felder vollständig aus");
            }
        }

        private Models.Ausbilder ErschaffeAusbilder()
        {
            return new Models.Ausbilder
            {
                Id = AktuellerNutzer.Id,
                Vorname = AktuellerNutzer.Vorname.Trim(),
                Name = AktuellerNutzer.Name.Trim(),
                EMail = AktuellerNutzer.EMail.Trim(),
                Telefonnummer = AktuellerNutzer.Telefonnummer.Trim(),
                Nutzer= new Nutzer
                {
                    Passwort = this.Passwort.Password,
                    Sicherheitsfrage = (WirdNeuErschaffen) ? (Sicherheitsfrage)this.Sicherheitsfrage.SelectionBoxItem : new Sicherheitsfrage { },
                    Sicherheitsantwort = Antwort.Text,
                    Id = AktuellerNutzer.Nutzer.Id,
                    Nutzertyp = AktuellerNutzer.Nutzer.Nutzertyp,
                },
            };
        }


        /// <summary>
        /// Überprüft, ob alle relevanten Einträge nicht leer sind
        /// </summary>
        /// <returns></returns>
        private bool IstInputOK()
        {
            if (string.IsNullOrWhiteSpace(AktuellerNutzer.Name)) return false; 
            if (string.IsNullOrWhiteSpace(AktuellerNutzer.Vorname))  return false; 
            if (string.IsNullOrWhiteSpace(AktuellerNutzer.EMail)) return false;
            if (string.IsNullOrWhiteSpace(AktuellerNutzer.Telefonnummer)) return false;

            if (AktuellerNutzer.Nutzer.Nutzertyp.Typ == "Teilnehmer")
            {
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Beruf.Bezeichnung)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Ausbilder.Name)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Adresse.Ort)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Adresse.Postleitzahl)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Adresse.Straße)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Adresse.Hausnummer)) return false;
                if (string.IsNullOrWhiteSpace(AktuellerNutzer.Adresse.Land)) return false;
            }
            if (WirdNeuErschaffen)
            {
                if (string.IsNullOrWhiteSpace(Passwort.Password)) return false;
                if (string.IsNullOrWhiteSpace(Passwort_bestaetigen.Password)) return false;
                //Sicherheitsfrage kann nicht leer sein
                if (string.IsNullOrWhiteSpace(Antwort.Text)) return false;
                if (string.IsNullOrWhiteSpace(Antwort_bestaetigen.Text)) return false;
            }
            return true;
        }

        public void ZeigeFehlermeldung(string Fehlermeldung)
        {
            MessageBox.Show(Fehlermeldung);
        }
    }
}
