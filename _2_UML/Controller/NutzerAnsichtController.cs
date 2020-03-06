using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2_UML.Views;
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class NutzerAnsichtController : BasisController, IController
    {
        /// <summary>
        /// 
        /// </summary>
        public NutzerAnsichtController()
        {
            MainController();

            NutzerErschaffen();
      
            NutzerAnsichtView.ZeigeView();//SeiteAnzeigen
        }

        public NutzerAnsichtController(Models.Teilnehmer teilnehmer)
        {
            MainController();

            bool istEigenesProfil = (teilnehmer.Nutzer.Id == _Id) ? true : false;
            NutzerAnsichtView.DatenFestlegen(teilnehmer, istEigenesProfil);
            AusgewaehlterTeilnehmer = teilnehmer;

            NutzerAnsichtView.ZeigeView();//SeiteAnzeigen
        }

        public NutzerAnsichtController(Models.Ausbilder ausbilder)
        {
            MainController();

            bool istEigenesProfil = (ausbilder.Nutzer.Id == _Id) ? true : false;
            NutzerAnsichtView.DatenFestlegen(ausbilder, istEigenesProfil);
            AusgewaehlterAusbilder = ausbilder;

            NutzerAnsichtView.ZeigeView();//SeiteAnzeigen
        }

        private void MainController()
        {
            NutzerAnsichtView = new NutzerAnsichtView();

            NutzerAnsichtView.ZeigeViewFertig += SeiteWechseln;
            NutzerAnsichtView.GeheZuLetzteSeite += SeiteZurueck;
            NutzerAnsichtView.GeheZuNutzerAendern += NutzerAendern;

            Nutzertyp = Application.Current.Properties["User_Nutzertyp"].ToString();
            //Geht wahrscheinlich effizienter, aber (int)Application.Current.Properties["User_Id"] wurde nicht genommen
            Int32.TryParse(Application.Current.Properties["User_Id"].ToString(), out int number);
            _Id = number;

            NavigationsHistorie.Add(this);//Zur Navigation hinzufügen
        }


        internal INutzerAnsichtView NutzerAnsichtView { get; set; }

        private string Nutzertyp { get; set; }
        //Id des momentanen Nutzers
        private int _Id { get; set; }
        private Models.Teilnehmer AusgewaehlterTeilnehmer { get; set; }
        private Models.Ausbilder AusgewaehlterAusbilder { get; set; }

        public void SeiteNeuLaden()
        {
            NutzerAnsichtView.ZeigeView();
        }

        /// <summary>
        /// Erschafft einen Nutzer aus den Daten des aktuellen Nutzers des Programmes und zeigt ihn im Profil an
        /// </summary>
        private void NutzerErschaffen()
        {
;
            string _Vorname = Application.Current.Properties["User_Vorname"].ToString();
            string _Name = Application.Current.Properties["User_Nachname"].ToString();
            string _Telefonnummer = Application.Current.Properties["User_Telefone"].ToString();
            string _E_Mail = Application.Current.Properties["User_E_Mail"].ToString();

            if(Nutzertyp == "Ausbilder")
            {
                Models.Ausbilder nutzer= new Models.Ausbilder()
                {
                    Id = _Id,
                    Vorname = _Vorname,
                    Name = _Name,
                    Telefonnummer = _Telefonnummer,
                    EMail = _E_Mail,
                };

                NutzerAnsichtView.DatenFestlegen(nutzer,true);
                AusgewaehlterAusbilder = nutzer;
            }
            else if (Nutzertyp == "Teilnehmer")
            {
                Int32.TryParse(Application.Current.Properties["User_Ausbilder_Id"].ToString(), out int IdAusbilder); 

                Models.Teilnehmer nutzer = new Models.Teilnehmer()
                {
                    Id = _Id,
                    Vorname = _Vorname,
                    Name = _Name,
                    Telefonnummer = _Telefonnummer,
                    EMail = _E_Mail,
                    Beruf = new Beruf { Id = 1, Bezeichnung = Application.Current.Properties["User_Beruf"].ToString()},
                    Adresse = new Adresse
                    {
                        Ort = Application.Current.Properties["User_Wohnort"].ToString(),
                        Postleitzahl = Application.Current.Properties["User_Postleitzahl"].ToString(),
                        Straße = Application.Current.Properties["User_Straße"].ToString(),
                        Hausnummer = Application.Current.Properties["User_Hausnummer"].ToString(),
                        Land = Application.Current.Properties["User_Land"].ToString(),
                    },
                    Ausbilder = new Models.Ausbilder
                    {                      
                        Id = IdAusbilder,
                        Vorname = Application.Current.Properties["User_Ausbilder_Vorname"].ToString(),
                        Name = Application.Current.Properties["User_Ausbilder_Nachname"].ToString(),
                    },
                };

                NutzerAnsichtView.DatenFestlegen(nutzer, true);
                AusgewaehlterTeilnehmer = nutzer;
            }
        }

        private void NutzerAendern()
        {
            if(Nutzertyp == "Ausbilder")
            {
                NutzerAnlegenBearbeitenController nutzerAnlegenBearbeitenController = new NutzerAnlegenBearbeitenController(AusgewaehlterAusbilder);
            }
            else if (Nutzertyp == "Teilnehmer")
            {
                NutzerAnlegenBearbeitenController nutzerAnlegenBearbeitenController = new NutzerAnlegenBearbeitenController(AusgewaehlterTeilnehmer);
            }        
        }
    }
}
