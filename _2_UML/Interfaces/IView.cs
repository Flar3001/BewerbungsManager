using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2_UML.Interfaces
{
    public interface IView
    {
        void ZeigeView();

        event SeitenAnsicht ZeigeViewFertig;
    }
}