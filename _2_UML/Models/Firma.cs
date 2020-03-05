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
}