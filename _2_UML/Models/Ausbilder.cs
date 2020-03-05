using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Ausbilder :  INutzer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Telefonnummer { get; set; }
        public string EMail { get; set; }

        public Nutzer Nutzer { get; set; }
    }
}