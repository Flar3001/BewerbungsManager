using _2_UML.Interfaces;
using _2_UML.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;
using System.Collections.ObjectModel;
using _2_UML.Persistence;

namespace _2_UML.Controller
{
    public class TeilnehmerUebersichtController : UebersichtController<Models.Teilnehmer>, IController
    {
        public TeilnehmerUebersichtController()
        {
            TeilnehmerUebersichtView = new TeilnehmerUebersichtView();
            TeilnehmerUebersichtView.ZeigeAlleObjekte(AngezeigteTeilnehmer());

            TeilnehmerUebersichtView.ZeigeViewFertig += SeiteWechseln;
            TeilnehmerUebersichtView.ZurStartseite += ZurStartseite;
            TeilnehmerUebersichtView.ObjektHinzufuegen += TeilnehmerHinzufuegen;
            TeilnehmerUebersichtView.TeilnehmerLoeschen += TeilnehmerLoeschen;
            TeilnehmerUebersichtView.ZuTeilnehmer += ZuTeilnehmer;
            TeilnehmerUebersichtView.ZuBewerbungen += ZuBewerbungen;

            NavigationsHistorie.Add(this);
            TeilnehmerUebersichtView.ZeigeView();
        }

        //public ITeilnehmerUebersichtView TeilnehmerUebersichtView = new TeilnehmerUebersichtView();
        public ITeilnehmerUebersichtView TeilnehmerUebersichtView { get; set; }

        public void SeiteNeuLaden()
        {
            TeilnehmerUebersichtView.ZeigeView();
        }

        /// <summary>
        /// Erstellt eine ObservableCollection aller Teilnehmer, inklusive Anzahl geschriebener Bewerbungen und Datum der letzten Bewerbung
        /// </summary>
        /// <returns>Die fertige ObservableCollection</returns>
        private ObservableCollection<AngezeigterTeilnehmer> AngezeigteTeilnehmer()
        {
            ObservableCollection<AngezeigterTeilnehmer> angezeigterTeilnehmer = new ObservableCollection<AngezeigterTeilnehmer>();
            List<AngezeigterTeilnehmer> alleTeilnehmer = MySQLHandler.SelectAllTeilnehmer();

            foreach(AngezeigterTeilnehmer einzelnerTeilnehmer in alleTeilnehmer)
            {
                angezeigterTeilnehmer.Add(einzelnerTeilnehmer);
            }
            return angezeigterTeilnehmer;
        }

        /// <summary>
        /// Leitet weiter zur Teilnehmererstellung
        /// </summary>
        private void TeilnehmerHinzufuegen()
        {
            NutzerAnlegenBearbeitenController nutzerAnlegenBearbeitenController = new NutzerAnlegenBearbeitenController(new Models.Teilnehmer { });
        }

        /// <summary>
        /// Löscht einen Teilnehmer sowie alle zu diesem Teilnehmer gehörigen Daten aus der Datenbank.
        /// </summary>
        /// <param name="angezeigterTeilnehmer"></param>
        private void TeilnehmerLoeschen(AngezeigterTeilnehmer angezeigterTeilnehmer)
        {

        }

        /// <summary>
        /// Leitet zum Profil des Teilnehmers weiter
        /// </summary>
        /// <param name="teilnehmer">Der Teilnehmer, welcher genauer angesehen werden soll</param>
        private void ZuTeilnehmer(Models.Teilnehmer teilnehmer)
        {
            NutzerAnsichtController nutzerAnsichtController = new NutzerAnsichtController(teilnehmer);
        }

        private void ZuBewerbungen(Models.Teilnehmer teilnehmer)
        {
            //BewerbungUebersichtView bewerbungUebersichtView = new BewerbungUebersichtView(teilnehmer);
        }
    }
}