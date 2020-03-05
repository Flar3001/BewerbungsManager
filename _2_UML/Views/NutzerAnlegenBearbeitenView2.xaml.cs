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

        private int Id { get; set; }
        private int AdresseId { get; set; }
        private string vorname;
        public string Vorname { get { return vorname; } set { SetValue(ref vorname, value); } }
        private string nachname;
        public string Nachname { get { return nachname; } set { SetValue(ref nachname, value); } }
        private string telefonnummer;
        public string Telefonnummer { get { return telefonnummer; } set { SetValue(ref telefonnummer, value); } }
        private string e_mail;
        public string E_Mail { get { return e_mail; } set { SetValue(ref e_mail, value); } }
        private string ort;
        public string Ort { get { return ort; } set { SetValue(ref ort, value); } }
        private string postleitzahl;
        public string Postleitzahl { get { return postleitzahl; } set { SetValue(ref postleitzahl, value); } }
        private string straße;
        public string Straße { get { return straße; } set { SetValue(ref straße, value); } }
        private string hausnummer;
        public string Hausnummer { get { return hausnummer; } set { SetValue(ref hausnummer, value); } }
        private Models.Ausbilder ausbilder;
        public Models.Ausbilder Ausbilder { get { return ausbilder; } set { SetValue(ref ausbilder, value); } }
        private string land;
        public string Land { get { return land; } set { SetValue(ref land, value); } }
        private Beruf beruf;
        public Beruf Beruf { get { return beruf; } set { SetValue(ref beruf, value); } }

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
                string Nutzer;
                string Aktion;

                Nutzer= (IstTeilnehmer) ? "Teilnehmer" : "Ausbilder";
                Aktion= (WirdNeuErschaffen) ?"erstellen" : "bearbeiten";
                return $"{Nutzer} {Aktion}";
            }
        }


        public void DatenAnzeigen(Models.Ausbilder ausbilder, bool neuErschaffen, List<Sicherheitsfrage> AlleSicherheitsfragen)
        {
            IstTeilnehmer = false;
            WirdNeuErschaffen = neuErschaffen;

            Id = ausbilder.Id;
            Vorname = ausbilder.Vorname;
            Nachname = ausbilder.Name;
            Telefonnummer = ausbilder.Telefonnummer;
            E_Mail = ausbilder.EMail;
        }

        public void DatenAnzeigen(Models.Teilnehmer teilnehmer, bool neuErschaffen, List<Models.Ausbilder> AlleAusbilder, List<Sicherheitsfrage> AlleSicherheitsfragen, List<Beruf> AlleBerufe)
        {
            IstTeilnehmer = true;
            WirdNeuErschaffen = neuErschaffen;
            this.AlleAusbilder = AlleAusbilder;
            this.AlleBerufe = AlleBerufe;

            Id = teilnehmer.Id;
            AdresseId = teilnehmer.Adresse.Id;
            Vorname = teilnehmer.Vorname;
            Nachname = teilnehmer.Name;
            Telefonnummer = teilnehmer.Telefonnummer;
            E_Mail = teilnehmer.EMail;
            Ort = teilnehmer.Adresse.Ort;
            Postleitzahl = teilnehmer.Adresse.Postleitzahl;
            Straße = teilnehmer.Adresse.Straße;
            Hausnummer = teilnehmer.Adresse.Hausnummer;
            Ausbilder = teilnehmer.Ausbilder;//$"{teilnehmer.Ausbilder.Vorname} {teilnehmer.Ausbilder.Name}";
            Land = teilnehmer.Adresse.Land;
            Beruf = teilnehmer.Beruf;
        }


        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        private void AenderungenSpeichernButton(object sender, RoutedEventArgs e)
        {
            if (IstTeilnehmer)
            {
                SpeichereAenderungenTeilnehmer(ErschaffeTeilnehmer());
            }
            else
            {
                SpeichereAenderungenAusbilder(ErschaffeAusbilder());
            }
    }

        private void AendernAbbrechenButton(object sender, RoutedEventArgs e)
        {
            GeheZurueck();
        }

        private void NeuenNutzerSpeichernButton(object sender, RoutedEventArgs e)
        {
            if (Passwort == Antwort)
            {
                ZeigeFehlermeldung("Passwort und Sicherheitsantwort dürfen nicht gleich sein");
            }
            else if (Passwort == Passwort_bestaetigen && Antwort==Antwort_bestaetigen)
            {

                    if (IstTeilnehmer)
                    {
                        SpeichereNeuenTeilnehmer(ErschaffeTeilnehmer());
                    }
                    else
                    {
                        SpeichereNeuenAusbilder(ErschaffeAusbilder());
                    }

            }
            else
            {
                ZeigeFehlermeldung("Bitte überprüfen Sie Passwort und Sicherheitsantwort auf Differenzen");
            }
        }

        private Models.Teilnehmer ErschaffeTeilnehmer()
        {
            return new Models.Teilnehmer
            {
                Id = this.Id,
                Vorname = this.Vorname.Trim(),
                Name = this.Nachname.Trim(),
                Telefonnummer = this.Telefonnummer.Trim(),
                EMail = this.E_Mail.Trim(),
                Ausbilder = this.Ausbilder,
                Beruf = this.Beruf,
                Adresse = new Adresse
                {
                    Id = AdresseId,
                    Ort = Ort,
                    Straße = Straße,
                    Land = Land,
                    Hausnummer = Hausnummer,
                    Postleitzahl = Postleitzahl
                },
                Nutzer = ErschaffeNutzer(),
            };
        }

        private Models.Ausbilder ErschaffeAusbilder()
        {
            return new Models.Ausbilder
            {
                Id = this.Id,
                Vorname = this.Vorname.Trim(),
                Name = this.Nachname.Trim(),
                EMail = this.E_Mail.Trim(),
                Telefonnummer = this.Telefonnummer.Trim(),
                Nutzer=ErschaffeNutzer(),
            };
        }

        private Nutzer ErschaffeNutzer()
        {
            return new Nutzer
            {
                Passwort = this.Passwort.Text,
                Sicherheitsfrage = (WirdNeuErschaffen) ? (Sicherheitsfrage)this.Sicherheitsfrage.SelectionBoxItem : new Sicherheitsfrage { },
                Sicherheitsantwort = this.Antwort.Text,

                //Das folgende muss ersetzt werden, wenn der Wechsel auf ein ordentliches Nutzersystem erfolgt
                Nutzertyp = (IstTeilnehmer) ? new Nutzertyp { Id = 2, Typ = "Teilnehmer" } : new Nutzertyp { Id = 1, Typ = "Ausbilder" }
            };
        }

        public void ZeigeFehlermeldung(string Fehlermeldung)
        {
            MessageBox.Show(Fehlermeldung);
        }
    }






}
