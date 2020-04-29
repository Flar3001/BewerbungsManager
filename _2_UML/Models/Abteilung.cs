using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Abteilung
    {
        public int Id { get; set; }

        public Ansprechpartner Ansprechpartner { get; set; }

        public Firma Firma { get; set; }

        public Beruf Beruf { get; set; }
    }
}