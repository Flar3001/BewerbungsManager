using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Abteilung
    {
        public int Id { get; set; }

        public Ansprechpartner Ansprechpartner
        {
            get => default(Ansprechpartner);
            set
            {
            }
        }

        public Firma Firma
        {
            get => default(Firma);
            set
            {
            }
        }

        public Beruf Beruf
        {
            get => default(Beruf);
            set
            {
            }
        }
    }
}