using _2_UML.Controller;
using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : BasePage, ILoginView
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = this;
            MeldungSichtbar = false;
        }

        public string EMail { get; set; }
        //Binding Passwords is bad practice!
        //public string Passwort { get; set; }


        private string meldung;
        public string Meldung { get { return meldung; } private set {SetValue(ref meldung, value); } }

        private string meldungsFarbe;
        public string MeldungsFarbe { get { return meldungsFarbe; } private set {SetValue(ref meldungsFarbe, value); } }

        public bool meldungSichtbar;
        public bool MeldungSichtbar { get { return meldungSichtbar; } private set { SetValue(ref meldungSichtbar, value); } }


        /// <summary>
        /// Zeigt die aktuelle Seite im Frame an
        /// </summary>
        public void ZeigeView()//Verweise: Falsch, muss korrigiert werden
        {
            ZeigeViewFertig(this);
        }

        public event SeitenAnsicht ZeigeViewFertig;

        public event LoginHandler UeberpruefeLogin;

        /// <summary>
        /// Überprüft Login und Passwort und führt gegebenenfalls die Anmeldung aus
        /// </summary>
        /// <param name="sender">Vom Anwender eingegebene E-Mail</param>
        /// <param name="e">Vom Anwender eingegebenes Passwort</param>
        private void LoeseLoginAus(object sender, RoutedEventArgs e)
        {
            UeberpruefeLogin(EMail, Passwort.Password);
            ZeigeViewFertig(this);
        }

        /// <summary>
        /// Gibt eine Meldung heraus
        /// </summary>
        /// <param name="meldungstext">Text der Meldung</param>
        /// <param name="farbe">Textfarbe der Meldung</param>
        public void MeldungBearbeiten(string meldungstext, string farbe)
        {
            Meldung = meldungstext;
            MeldungSichtbar = true;
            MeldungsFarbe = farbe;
        }
    }
}
