using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public delegate void ZuBewerbung(Bewerbung bewerbung);
    public delegate void BewerbungLoeschen(Bewerbung bewerbung);

    public interface IBewerbungUebersichtView : IUebersichtView<Bewerbung>
    {
        event ZuBewerbung ZuBewerbung;
        event BewerbungLoeschen BewerbungLoeschen;
    }
}