using _2_UML.Interfaces;
using _2_UML.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class TeilnehmerAnlegenBearbeitenController : AnlegenBearbeitenController<Models.Teilnehmer>
    {
        //public ITeilnehmerAnlegenBearbeitenView TeilnehmerAnlegenBearbeitenView = new TeilnehmerAnlegenBearbeitenView();
        public ITeilnehmerAnlegenBearbeitenView ITeilnehmerAnlegenBearbeitenView { get; set; } = new TeilnehmerAnlegenBearbeitenView();
    }
}