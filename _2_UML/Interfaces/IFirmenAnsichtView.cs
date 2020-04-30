using _2_UML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_UML.Interfaces
{
    public delegate void Zurueck();

    public interface IFirmenAnsichtView : IView 
    {
        event Zurueck GeheZuLetzteSeite;

        void ObjektAnzeigen(Firma firma);
    }
}
