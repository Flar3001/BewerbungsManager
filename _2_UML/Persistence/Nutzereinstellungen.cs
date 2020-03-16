using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Persistence
{
    static class Nutzereinstellungen
    {
        public static void EinstellungenSpeichern(Models.Ausbilder ausbilder)
        {
            NutzerEinstellungenSpeichern(ausbilder);
        }

        public static void EinstellungenSpeichern(Models.Teilnehmer teilnehmer)
        {
            NutzerEinstellungenSpeichern(teilnehmer);

            Application.Current.Properties.Add("User_Beruf", teilnehmer.Beruf.Bezeichnung);
            Application.Current.Properties.Add("User_Wohnort", teilnehmer.Adresse.Ort);
            Application.Current.Properties.Add("User_Postleitzahl", teilnehmer.Adresse.Postleitzahl);
            Application.Current.Properties.Add("User_Straße", teilnehmer.Adresse.Straße);
            Application.Current.Properties.Add("User_Hausnummer", teilnehmer.Adresse.Hausnummer);
            Application.Current.Properties.Add("User_Land", teilnehmer.Adresse.Land);
            Application.Current.Properties.Add("User_Ausbilder_Vorname", teilnehmer.Ausbilder.Vorname);
            Application.Current.Properties.Add("User_Ausbilder_Nachname", teilnehmer.Ausbilder.Name);
            Application.Current.Properties.Add("User_Ausbilder_Id", teilnehmer.Ausbilder.Id);            
        }

        private static void NutzerEinstellungenSpeichern(INutzer nutzer)
        {
            Application.Current.Properties.Clear();

            Application.Current.Properties.Add("User_Vorname", nutzer.Vorname);
            Application.Current.Properties.Add("User_Nachname", nutzer.Name);
            Application.Current.Properties.Add("User_Telefone", nutzer.Telefonnummer);
            Application.Current.Properties.Add("User_E_Mail", nutzer.EMail);
            Application.Current.Properties.Add("User_Nutzertyp", nutzer.Nutzer.Nutzertyp.Typ);
            Application.Current.Properties.Add("User_Id", nutzer.Id);
            Application.Current.Properties.Add("User_Nutzer_Id", nutzer.Nutzer.Id);
            
        }
    }
}
