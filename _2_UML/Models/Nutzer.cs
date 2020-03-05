using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Models
{
    public class Nutzer
    {
        public int Id { get; set; }
        public Nutzertyp Nutzertyp { get; set; }
        public Sicherheitsfrage Sicherheitsfrage { get; set; }
        public string Passwort { get; set; }
        public string Sicherheitsantwort { get; set; }
    }
}