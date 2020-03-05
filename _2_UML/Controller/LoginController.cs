using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using _2_UML.Interfaces;
using _2_UML.Views;
using _2_UML.Persistence;
using System.Drawing;

namespace _2_UML.Controller
{
    public class LoginController : BasisController, IController
    {
        public LoginController(bool logout=false)
        {
            //Schritt 0: Navigation starten
            NavigationsHistorie = new List<IController>();


            //Schritt 1: Ein neues View erschaffen
            LoginView = new LoginView();

            //Schritt 2: Events abonnieren
            LoginView.UeberpruefeLogin += Login;
            LoginView.ZeigeViewFertig += SeiteWechseln;

            //Schritt 3: Neuen View anpassen
            if (logout)
            {
                LoginView.MeldungBearbeiten("Sie haben sich erfolgreich ausgeloggt", "Black");
            }

            //Finaler Schritt: View anzeigen
            LoginView.ZeigeView();
        }



        private ILoginView LoginView { get; set; }

        public void SeiteNeuLaden()
        {
            LoginView.ZeigeView();
        }

        /// <summary>
        /// Ueberprueft Logindaten und, wenn erfolgreich, führt das Login aus
        /// </summary>
        /// <param name="name"></param>
        /// <param name="passwort"></param>
        private void Login(string email, string passwort)
        {
            if(MySQLHandler.CheckLogin(email, passwort))
            {
                StartseiteController startseite = new StartseiteController();
            }
            else
            {
                LoginView.MeldungBearbeiten("Login fehlgeschlagen. Bitte überprüfen Sie Ihr Passwort", "Red");
            }
        }



    }
}