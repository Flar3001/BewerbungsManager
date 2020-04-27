using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Models;
using _2_UML.Views;
using _2_UML.Persistence;
using System.Reflection;

namespace _2_UML.Controller
{
    public class NutzerAnlegenBearbeitenController : AnlegenBearbeitenController<Nutzer>, IController
    {
        /// <summary>
        /// Konstruktor für das Erschaffen oder Bearbeiten eines Ausbilderprofils
        /// </summary>
        /// <param name="ausbilder">Der zu bearbeitende Ausbilder. Wenn neu erstellt werden soll, wird ein Ausbilder mit Id=0 übergeben</param>
        public NutzerAnlegenBearbeitenController(Models.Ausbilder ausbilder)
        {
            if(ausbilder.Id==0 ) ausbilder.Nutzer=new Nutzer { Id=0, Nutzertyp = new Nutzertyp { Id=1, Typ="Ausbilder"} } ;
            MainNutzerAnlegenBearbeitenController(ausbilder);

            //Ausbilderspezifische Events vorbereiten
            NutzerAnlegenBearbeitenView.SpeichereAenderungenAusbilder += GeaendertenAusbilderSpeichern;
            NutzerAnlegenBearbeitenView.SpeichereNeuenAusbilder += NeuenAusbilderSpeichern;

            //Bereitet alle nötigen Informationen für die Anzeige eines Ausbilders vor
            NutzerAnlegenBearbeitenView.DatenAnzeigen(ausbilder, NeuErschaffen, AlleSicherheitsfragen);

            NavigationsHistorie.Add(this);
            NutzerAnlegenBearbeitenView.ZeigeView();
        }

        /// <summary>
        /// Konstruktor für das Erschaffen oder Bearbeiten eines Ausbilderprofils
        /// </summary>
        /// <param name="ausbilder">Der zu bearbeitende Teilnehmer. Wenn neu erstellt werden soll, wird ein Teilnehmer mit Id=0 übergeben</param>
        public NutzerAnlegenBearbeitenController(Models.Teilnehmer teilnehmer)
        {
            if (teilnehmer.Id == 0) teilnehmer.Nutzer = new Nutzer { Id=0, Nutzertyp = new Nutzertyp { Id=2, Typ = "Teilnehmer" } };
            MainNutzerAnlegenBearbeitenController(teilnehmer);

            //Teilnehmerspezifische Events vorbereiten
            NutzerAnlegenBearbeitenView.SpeichereAenderungenTeilnehmer += GeaendertenTeilnehmerSpeichern;
            NutzerAnlegenBearbeitenView.SpeichereNeuenTeilnehmer += NeuenTeilnehmerSpeichern;

            //Bereitet alle nötigen Informationen für die Anzeige eines Teilnehmers vor
            LadeAusbilder();
            LadeBerufe();
            NutzerAnlegenBearbeitenView.DatenAnzeigen(teilnehmer, NeuErschaffen,AlleAusbilder,AlleSicherheitsfragen, AlleBerufe);

            NavigationsHistorie.Add(this);
            NutzerAnlegenBearbeitenView.ZeigeView();
        }

        /// <summary>
        /// Beinhaltet von beiden Kontruktoren durchgeführte Aktionen
        /// </summary>
        /// <param name="nutzer">Übergebene Nutzer</param>
        private void MainNutzerAnlegenBearbeitenController(INutzer nutzer)
        {
            NutzerAnlegenBearbeitenView = new NutzerAnlegenBearbeitenView2();
            NutzerAnlegenBearbeitenView.ZeigeViewFertig += SeiteWechseln;
            NutzerAnlegenBearbeitenView.GeheZurueck += SeiteZurueck;

            OriginalNutzer = nutzer;

            if (nutzer.Id == 0)
            {
                NeuErschaffen = true;
                LadeSicherheitsfragen();
            }
            else
            {
                NeuErschaffen = false;
                AlleSicherheitsfragen = new List<Sicherheitsfrage>();
            }
        }

        INutzerAnlegenBearbeitenView NutzerAnlegenBearbeitenView { get; set; }

        private bool NeuErschaffen { get; set; }
        private List<Models.Ausbilder> AlleAusbilder{get;set;}
        private List<Sicherheitsfrage> AlleSicherheitsfragen { get; set; }
        private List<Beruf> AlleBerufe { get; set; }
        private INutzer OriginalNutzer { get; set; }


        public void SeiteNeuLaden()
        {
            NutzerAnlegenBearbeitenView.ZeigeView();
        }

        /// <summary>
        /// Lädt eine Liste aller Ausbilder aus der Datenbank
        /// </summary>
        private void LadeAusbilder()
        {
            AlleAusbilder = MySQLHandler.SelectAusbilder();

            //Ist die einfachste Sortiermöglichkeit, theoretisch wäre .Sort() aber effizienter
            AlleAusbilder = AlleAusbilder.OrderBy(o => o.Name).ToList();
        }

        /// <summary>
        /// Lädt alle Sicherheitsfragen aus der Datenbank
        /// </summary>
        private void LadeSicherheitsfragen()
        {
            AlleSicherheitsfragen = MySQLHandler.SelectAllSicherheitsfragen();
        }

        /// <summary>
        /// Lädt alle Berufe aus der Datenbank
        /// </summary>
        private void LadeBerufe()
        {
            AlleBerufe = MySQLHandler.SelectAllBerufe();
        }

        /// <summary>
        /// Speichert einen neuen Teilnehmer in der Datenbank
        /// </summary>
        /// <param name="teilnehmer">Der neue Teilnehmer</param>
        private void NeuenTeilnehmerSpeichern(Models.Teilnehmer teilnehmer)
        {
            if (NutzerdatenUeberpruefen(teilnehmer, true))
            {
                if (MySQLHandler.AddNewTeilnehmer(teilnehmer))
                {
                    TeilnehmerUebersichtController teilnehmerUebersichtController = new TeilnehmerUebersichtController();
                }
                else
                {
                    NutzerAnlegenBearbeitenView.ZeigeFehlermeldung($"{MySQLHandler.Errormessage}");
                }
            }
            
        }

        /// <summary>
        /// Speichert einen neuen Ausbilder in der Datenbank
        /// </summary>
        /// <param name="ausbilder">Der neue Ausbilder</param>
        private void NeuenAusbilderSpeichern(Models.Ausbilder ausbilder)
        {
            if (NutzerdatenUeberpruefen(ausbilder, true))
            {
                if (MySQLHandler.AddNewAusbilder(ausbilder))
                {
                    AusbilderUebersichtController ausbilderUebersichtController = new AusbilderUebersichtController();
                }
                else
                {
                    NutzerAnlegenBearbeitenView.ZeigeFehlermeldung($"{MySQLHandler.Errormessage}");
                }
            }
            
        }

        /// <summary>
        /// Speichert Änderungen an einem Teilnehmer in der Datenbank
        /// </summary>
        /// <param name="teilnehmer">Der geänderte Teilnehmer</param>
        private void GeaendertenTeilnehmerSpeichern(Models.Teilnehmer teilnehmer)
        {
            if (NutzerdatenUeberpruefen(teilnehmer, false))
            {
                if (MySQLHandler.UpdateTeilnehmer(teilnehmer))
                {
                    Nutzereinstellungen.EinstellungenSpeichern(teilnehmer);
                    NutzerAnsichtController nutzerAnsichtController = new NutzerAnsichtController();
                }
                else
                {
                    NutzerAnlegenBearbeitenView.ZeigeFehlermeldung($"{MySQLHandler.Errormessage}");
                }
            }
        }

        /// <summary>
        /// Speichert Änderungen an einem Ausbilder in der Datenbank 
        /// </summary>
        /// <param name="ausbilder"></param>
        private void GeaendertenAusbilderSpeichern(Models.Ausbilder ausbilder)
        {
            if (NutzerdatenUeberpruefen(ausbilder, false))
            {                
                if (MySQLHandler.UpdateAusbilder(ausbilder))
                {
                    Nutzereinstellungen.EinstellungenSpeichern(ausbilder);
                    AusbilderUebersichtController ausbilderUebersichtController = new AusbilderUebersichtController();
                }
                else
                {
                    NutzerAnlegenBearbeitenView.ZeigeFehlermeldung($"{MySQLHandler.Errormessage}");
                }
            }
        }


        /// <summary>
        /// Überprüft, ob die eingegebenen Nutzerdaten korrekt vorliegen
        /// </summary>
        /// <param name="nutzer"></param>
        /// <returns>Boolean ob vollständig oder nicht</returns>
        private bool NutzerdatenUeberpruefen(INutzer nutzer, bool istNeu)
        {
            //Wir erstellen einen komplett neuen Nutzer
            if (NeuErschaffen)
            {
                if(MySQLHandler.EMailUnique(nutzer.EMail) == false)
                {
                    NutzerAnlegenBearbeitenView.ZeigeFehlermeldung("Diese E-Mail wurde bereits vergeben");
                    return false;
                }
            }
            else
            {
                if(nutzer.EMail != OriginalNutzer.EMail)
                {
                    if(MySQLHandler.EMailUnique(nutzer.EMail) == false)
                    {
                        NutzerAnlegenBearbeitenView.ZeigeFehlermeldung("Diese E-Mail wurde bereits vergeben");
                        return false;
                    }
                }
            }
            return true; 
        }

        private bool NutzerUeberpruefen(Models.Nutzer nutzer)
        {
            return true;
        }
    }
}
