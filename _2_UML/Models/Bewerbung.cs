using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Bewerbung
    {
        public int Id { get; set; }

        public DateTime Datum_Gesendet { get; set; }

        public DateTime Datum_Antwort { get; set; }

        public Bewerbung_Status Bewerbung_Status
        {
            get => default(Bewerbung_Status);
            set
            {
            }
        }

        public Bewerbung_Typ Bewerbung_Typ
        {
            get => default(Bewerbung_Typ);
            set
            {
            }
        }

        public Teilnehmer Teilnehmer
        {
            get => default(Teilnehmer);
            set
            {
            }
        }

        public Abteilung Abteilung
        {
            get => default(Abteilung);
            set
            {
            }
        }
    }
}