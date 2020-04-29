using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;


namespace _2_UML.Interfaces
{
    public delegate void ZuFirma(Firma firma);
    public delegate void FirmaLoeschen(AngezeigteFirma firma);
    public delegate void ZuBewerbungenVonFirma(Firma firma);

    public interface IFirmenuebersichtView : IUebersichtView<AngezeigteFirma>
    {
        event ZuFirma ZuFirma;
        event FirmaLoeschen FirmaLoeschen;
        event ZuBewerbungenVonFirma ZuBewerbungen;
    }
}