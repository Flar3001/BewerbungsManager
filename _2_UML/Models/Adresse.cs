using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Adresse
    {
        public int Id { get; set; }


        public string Land { get; set; }


        public string Ort { get; set; }

        public string Straße { get; set; }

        public string Hausnummer { get; set; }

        public string Postleitzahl { get; set; }
    }
}