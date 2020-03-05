using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Views;

namespace _2_UML.Controller
{
    class SicherheitsdatenController
    {
        internal Interfaces.ISicherheitsdatenView ISicherheitsdatenView { get; set; } = new SicherheitsdatenView();

    }
}
