using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Interfaces;

namespace _2_UML.Models
{
    public class Ansprechpartner : IPerson
    {
        public int Id { get; set; }


        public string Vorname { get; set; }


        public string Name { get; set; }


        public string Telefonnummer { get; set; }


        public string EMail { get; set; }
    }
}