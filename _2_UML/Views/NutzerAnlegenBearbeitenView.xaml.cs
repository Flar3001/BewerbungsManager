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
    /// Interaktionslogik für NutzerAnlegenBearbeitenView.xaml
    /// </summary>
    public partial class NutzerAnlegenBearbeitenView : BasePage, INutzerAnlegenBearbeitenView
    {
        public NutzerAnlegenBearbeitenView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event SeitenAnsicht ZeigeViewFertig;

        public void DatenAnzeigen(Models.Ausbilder ausbilder, bool neuErschaffen)
        {
            throw new NotImplementedException();
        }

        public void DatenAnzeigen(Models.Teilnehmer teilnehmer, bool neuErschaffen)
        {
            throw new NotImplementedException();
        }

        /*
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
private string ausbilder;
public string Ausbilder { get { return ausbilder; } set { SetValue(ref ausbilder, value); } }
private string land;
public string Land { get { return land; } set { SetValue(ref land, value); } }

public bool IstTeilnehmer { get; private set; }
public bool WirdNeuErschaffen { get; private set; }


public void DatenAnzeigen(Models.Ausbilder ausbilder, bool neuErschaffen)
{
   IstTeilnehmer = false;
   WirdNeuErschaffen = neuErschaffen;

   Vorname = ausbilder.Vorname;
   Nachname = ausbilder.Name;
   Telefonnummer = ausbilder.Telefonnummer;
   E_Mail = ausbilder.EMail;
}

public void DatenAnzeigen(Models.Teilnehmer teilnehmer, bool neuErschaffen)
{
   IstTeilnehmer = true;
   WirdNeuErschaffen = neuErschaffen;

   Vorname = teilnehmer.Vorname;
   Nachname = teilnehmer.Name;
   Telefonnummer = teilnehmer.Telefonnummer;
   E_Mail = teilnehmer.EMail;
   Ort = teilnehmer.Adresse.Ort;
   Postleitzahl = teilnehmer.Adresse.Postleitzahl;
   Straße = teilnehmer.Adresse.Straße;
   Hausnummer = teilnehmer.Adresse.Hausnummer;
   Ausbilder = $"{teilnehmer.Ausbilder.Vorname} {teilnehmer.Ausbilder.Name}";
   Land = teilnehmer.Adresse.Land;
}

*/

        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }
    }
}
