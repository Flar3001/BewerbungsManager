using _2_UML.Interfaces;
using _2_UML.Views;
using _2_UML.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;
using System.Collections.ObjectModel;

namespace _2_UML.Controller
{
    public class AusbilderUebersichtController : UebersichtController<Models.Ausbilder>, IController
    {
        public AusbilderUebersichtController()
        {
            AusbilderUebersichtView = new AusbilderUebersichtView();

            AusbilderUebersichtView.ZeigeAlleObjekte(AusbilderAuswaehlen());

            AusbilderUebersichtView.ZeigeViewFertig += SeiteWechseln;
            AusbilderUebersichtView.ZuAusbilder += AusbilderAnsehen;
            AusbilderUebersichtView.AusbilderLoeschen += ObjektLoeschen;
            AusbilderUebersichtView.ZurStartseite += ZurStartseite;
            AusbilderUebersichtView.AusbilderHinzufuegen += AusbilderHinzufuegen;

            AusbilderUebersichtView.ZeigeView();
        }

        public IAusbilderUebersichtView AusbilderUebersichtView { get; set; }

        public void SeiteNeuLaden()
        {
            AusbilderUebersichtView.ZeigeView();
        }

        /// <summary>
        /// Sucht alle Ausbilder heraus
        /// </summary>
        /// <returns>Alle Ausbilder in Form einer ObservableCollection</returns>
        private ObservableCollection<Models.Ausbilder> AusbilderAuswaehlen()
        {
            ObservableCollection<Models.Ausbilder> Ausbilder = new ObservableCollection<Models.Ausbilder>();
            foreach(var EinAusbilder in MySQLHandler.SelectAllAusbilder())
            {
                Ausbilder.Add(EinAusbilder);
            }

            return Ausbilder;
        }

        private void AusbilderAnsehen(Models.Ausbilder ausgewaehlterAusbilder)
        {
            NutzerAnsichtController nutzerAnsichtController = new NutzerAnsichtController(ausgewaehlterAusbilder);
        }

        private void ZurStartseite()
        {
            StartseiteController startseiteController = new StartseiteController();
        }

        private void ObjektLoeschen(int AusbilderId)
        {
            MySQLHandler.DeleteFromAusbilder(AusbilderId);
        }

        private void AusbilderHinzufuegen()
        {

        }

    }
}