using _2_UML.Interfaces;
using _2_UML.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class TeilnehmerUebersichtController : UebersichtController<Models.Teilnehmer>
    {
        //public ITeilnehmerUebersichtView TeilnehmerUebersichtView = new TeilnehmerUebersichtView();
        public ITeilnehmerUebersichtView ITeilnehmerUebersichtView { get; set; } = new TeilnehmerUebersichtView();
    }
}