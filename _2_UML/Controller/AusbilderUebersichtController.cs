using _2_UML.Interfaces;
using _2_UML.Views;
using _2_UML.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;
using System.Collections.ObjectModel;
using System.Windows;

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
            AusbilderUebersichtView.ObjektHinzufuegen += AusbilderHinzufuegen;

            NavigationsHistorie.Add(this);
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
            foreach(var EinAusbilder in MySQLHandler.SelectAusbilder())
            {
                Ausbilder.Add(EinAusbilder);
            }

            return Ausbilder;
        }

        private void AusbilderAnsehen(Models.Ausbilder ausgewaehlterAusbilder)
        {
            NutzerAnsichtController nutzerAnsichtController = new NutzerAnsichtController(ausgewaehlterAusbilder);
        }

        /// <summary>
        /// Löscht einen Ausbilder aus der Datenbank und der angezeigten Liste
        /// </summary>
        /// <param name="alterAusbilder"></param>
        private void ObjektLoeschen(Models.Ausbilder alterAusbilder)
        {
            MySQLHandler.DeleteAusbilder(alterAusbilder);

            AusbilderUebersichtView.AngezeigteObjekte.Remove(alterAusbilder);

            //Führt Logout aus wenn der eigene Account gelöscht wurde
            Int32.TryParse(Application.Current.Properties["User_Nutzer_Id"].ToString(), out int id);
            if(alterAusbilder.Nutzer.Id == id)
            {
                LogoutAusfuehren();
            }
        }

        /// <summary>
        /// Leitet weiter zur Ansicht zum Erstellen von Ausbildern
        /// </summary>
        private void AusbilderHinzufuegen()
        {
            NutzerAnlegenBearbeitenController nutzerAnlegenBearbeitenController = new NutzerAnlegenBearbeitenController(new Models.Ausbilder { });
        }

    }
}