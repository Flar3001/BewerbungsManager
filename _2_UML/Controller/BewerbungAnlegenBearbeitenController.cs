using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Interfaces;
using _2_UML.Views;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class BewerbungAnlegenBearbeitenController : AnlegenBearbeitenController<Bewerbung>
    {
        //public IBewerbungAnlegenBearbeitenView BewerbungAnlegenBearbeitenView = new BewerbungAnlegenBearbeitenView();
        public IBewerbungAnlegenBearbeitenView IBewerbungAnlegenBearbeitenView { get; set; } = new BewerbungAnlegenBearbeitenView();
    }
}