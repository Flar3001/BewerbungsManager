using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public delegate void ZuAusbilder(Models.Ausbilder ausbilder);
    public delegate void AusbilderLoeschen(Models.Ausbilder ausbilder);

    public interface IAusbilderUebersichtView : IUebersichtView<Models.Ausbilder>
    {
        event ZuAusbilder ZuAusbilder;
        event AusbilderLoeschen AusbilderLoeschen; 
    }
}