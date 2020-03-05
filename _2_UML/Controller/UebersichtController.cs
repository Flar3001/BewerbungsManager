using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Persistence;

namespace _2_UML.Controller
{
    public class UebersichtController<T> : BasisController
    {
        /*
        protected List<T> LadeAlleObjekte()
        {
            return MySQLHandler.SelectAllObjects();
        }
        */
        protected void LadeSpezifischeObjekte(T Suchvorlage)
        {

        }

        protected void LoescheObjekt(T ZuLoeschendesObjekt)
        {

        }
    }
}