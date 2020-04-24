using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public delegate void ZuTeilnehmer(Models.Teilnehmer teilnehmer);
    public delegate void ZuBewerbungen(Models.Teilnehmer teilnehmer);
    public delegate void TeilnehmerLoeschen(AngezeigterTeilnehmer angezeigterTeilnehmer);

    public interface ITeilnehmerUebersichtView : IUebersichtView<AngezeigterTeilnehmer>
    {
        event TeilnehmerLoeschen TeilnehmerLoeschen;
        event ZuTeilnehmer ZuTeilnehmer;
        event ZuBewerbungen ZuBewerbungen;
    }
}