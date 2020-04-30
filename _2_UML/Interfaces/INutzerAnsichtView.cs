using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public delegate void Aendern();
    //public delegate void Zurueck2();

    interface INutzerAnsichtView : IView
    {
        event Zurueck GeheZuLetzteSeite;
        event Aendern GeheZuNutzerAendern;

        void DatenFestlegen(Models.Teilnehmer nutzer, bool istEigenesProfil);
        void DatenFestlegen(Models.Ausbilder nutzer, bool istEigenesProfil);
    }
}
