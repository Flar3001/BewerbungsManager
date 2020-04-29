using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Firma
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public string BewerbungsTelefonummer { get; set; }
        public string BewerbungsEMailAdresse { get; set; }

        public Adresse Adresse { get; set; }

    }

    /// <summary>
    /// Eine spezielle Klasse, welche genutzt wird, um die Firmen in der Tabelle aller Firmen zusammen mit Eigenschaften
    /// darzustellen, welche nicht in der normalen Firma vorhanden sind
    /// </summary>
    public class AngezeigteFirma : Firma
    {
        public List<string> AbteilungenDerFirma { get; set; }

        public int DurchschnittlicheAntwortDauerInTagen { get; set; }

        public int AnzahlBewerbungen { get; set; }
    }
}