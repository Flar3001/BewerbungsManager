using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using _2_UML.Interfaces;

namespace _2_UML.Controller
{
    public class BasisController
    {
        /// <summary>
        /// Alamiert das MainWindow und sorgt dafür, dass die neue Seite angezeigt wird. Muss im Konstruktor jedes Controllers mit dem aktullen ZeigeViewFertigEvent
        /// verknüpft werden
        /// </summary>
        /// <param name="page">Neue Seite</param>
        protected void SeiteWechseln(Page page)
        {
            MainWindowController.NavigateTo(page);
        }

        /// <summary>
        /// Ermöglicht die "Zurück zur letzten bekannten Seite"-Funktion
        /// </summary>
        protected void SeiteZurueck()
        {
            int length = NavigationsHistorie.Count;

            NavigationsHistorie.RemoveAt(length - 1);
            IController basisController = NavigationsHistorie[length - 2];
            basisController.SeiteNeuLaden();
        }

        /// <summary>
        /// Führt das Logout aus
        /// </summary>
        protected void LogoutAusfuehren()
        {
            Application.Current.Properties.Clear();
            NavigationsHistorie.Clear();
            LoginController login = new LoginController(true);
        }

        static protected List<IController> NavigationsHistorie;
    }
}