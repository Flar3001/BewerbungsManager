using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Views;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class FirmenUebersichtController : UebersichtController<Firma>
    {
        public IFirmenuebersichtView IFirmenuebersichtView
        {
            get => default(IFirmenuebersichtView);
            set
            {
            }
        }
        //IFirmenuebersichtView FirmenuebersichtView = new FirmenuebersichtView();
    }
}