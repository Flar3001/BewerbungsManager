using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Interfaces;

namespace _2_UML.Models
{
    public class Teilnehmer : INutzer
    {
        public int Id { get; set; }

        public string Vorname { get; set; }

        public string Name { get; set; }

        public string Telefonnummer { get; set; }

        public string EMail { get; set; }

        public Ausbilder Ausbilder { get; set; }

        public Beruf Beruf { get; set; }

        public Adresse Adresse { get; set; }

        public Nutzer Nutzer { get; set; }

    }

    /// <summary>
    /// Eine spezielle Klasse, welche genutzt wird, um die Teilnehmer in der Tabelle aller Teilnehmer zusammen mit Eigenschaften
    /// darzustellen, welche nicht im normalen Teilnehmer vorhanden sind
    /// </summary>
    public class AngezeigterTeilnehmer : Teilnehmer
    {
        //Neu hinzugefügt
        public int Anzahl_Bewerbungen { get; set; }
        //Neu hinzugefügt
        public DateTime Letzte_Bewerbung { get; set; }
    }
}