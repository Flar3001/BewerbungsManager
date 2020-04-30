using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Interfaces;
using _2_UML.Views;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class FirmenAnsichtController : BasisController,IController
    {
        public FirmenAnsichtController(Firma firma)
        {
            FirmenAnsichtView = new FirmenAnsichtView();
            FirmenAnsichtView.ObjektAnzeigen(firma);

            FirmenAnsichtView.ZeigeViewFertig += SeiteWechseln;
            FirmenAnsichtView.GeheZuLetzteSeite += SeiteZurueck;

            NavigationsHistorie.Add(this);
            FirmenAnsichtView.ZeigeView();
        }

        public IFirmenAnsichtView FirmenAnsichtView { get; set; }

        public void SeiteNeuLaden()
        {
            FirmenAnsichtView.ZeigeView();
        }
    }
}
