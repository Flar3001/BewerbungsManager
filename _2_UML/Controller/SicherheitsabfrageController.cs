﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Interfaces;
using _2_UML.Views;

namespace _2_UML.Controller
{
    class SicherheitsabfrageController
    {
        internal ISicherheitsabfrageView ISicherheitsabfrageView { get; set; } = new SicherheitsabfrageView();

    }
}
