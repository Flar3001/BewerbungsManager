using _2_UML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_UML.Controller
{
    class BerufeUebersichtController : UebersichtController<Beruf>
    {
        internal Interfaces.IBerufUebersichtView IBerufUebersichtView
        {
            get => default(Interfaces.IBerufUebersichtView);
            set
            {
            }
        }
    }
}
