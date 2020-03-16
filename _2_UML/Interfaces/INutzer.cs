using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public interface INutzer : IPerson
    {
        int Id { get; set; }

        string Telefonnummer { get; set; }

        string EMail { get; set; }

        Nutzer Nutzer { get; set; }
    }
}
