﻿using _2_UML.Interfaces;
using _2_UML.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class BewerbungUebersichtController : UebersichtController<Bewerbung>
    {
        //public IBewerbungUebersichtView BewerbungUebersichtView = new BewerbungUebersichtView();
        public IBewerbungUebersichtView IBewerbungUebersichtView { get; set; } = new BewerbungUebersichtView();
    }
}