using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_UML.Interfaces
{
    public interface IController
    {
        /// <summary>
        /// Methode, welche alle Controller/Presenter besitzen müsse, damit Sie über die Navigation neu aufgerufen können 
        /// </summary>
        void SeiteNeuLaden();
    }
}
