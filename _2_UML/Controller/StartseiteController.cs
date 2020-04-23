using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using _2_UML.Interfaces;
using _2_UML.Views;

namespace _2_UML.Controller
{
    public class StartseiteController : BasisController, IController
    {
        public StartseiteController()
        {
            StartseiteView = new StartseiteView();//View erstellen
            StartseiteView.ZeigeViewFertig += SeiteWechseln;//View in MainWindow anzeigen

            StartseiteView.LoeseLogoutAus += LogoutAusfuehren;            
            StartseiteView.GeheZuAusbilderUebersicht += GeheZuAusbilder;
            StartseiteView.GeheZuBerufeUebersicht += GeheZuBerufe;
            StartseiteView.GeheZuBewerbungenUebersicht += GeheZuBewerbungen;
            StartseiteView.GeheZuFirmenUebersicht += GeheZuFirmen;
            StartseiteView.GeheZuTeilnehmerAnsicht += GeheZuNutzerProfil;
            StartseiteView.GeheZuTeilnehmerUebersicht += GeheZuTeilnehmer;

            string username = $"{Application.Current.Properties["User_Vorname"]} {Application.Current.Properties["User_Nachname"]}";
            bool istAusbilder = Application.Current.Properties["User_Nutzertyp"].ToString() == "Ausbilder" ? true : false;
            StartseiteView.NameUndOptionenEinstellen(username, istAusbilder);

            NavigationsHistorie.Add(this);
            StartseiteView.ZeigeView();
        }

        public IStartseiteView StartseiteView { get; set; }



        private void GeheZuAusbilder()
        {
            AusbilderUebersichtController ausbilderUebersichtController = new AusbilderUebersichtController();
        }

        private void GeheZuBerufe()
        {
            BerufeUebersichtController berufeUebersichtController = new BerufeUebersichtController();
        }

        private void GeheZuBewerbungen()
        {
            BewerbungUebersichtController bewerbungUebersichtController = new BewerbungUebersichtController();
        }

        private void GeheZuFirmen()
        {
            FirmenUebersichtController firmenUebersichtController = new FirmenUebersichtController();
        }

        private void GeheZuNutzerProfil()
        {
            NutzerAnsichtController nutzerAnsichtController = new NutzerAnsichtController();
        }

        private void GeheZuTeilnehmer()
        {
            TeilnehmerUebersichtController teilnehmerUebersichtController = new TeilnehmerUebersichtController();
        }

        public void SeiteNeuLaden()
        {
            StartseiteView.ZeigeView();
        }
    }
}