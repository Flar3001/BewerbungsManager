using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;


namespace _2_UML.Interfaces
{
    public delegate void ZuFirma(Firma firma);
    public delegate void FirmaLoeschen(Firma firma);

    public interface IFirmenuebersichtView : IUebersichtView<Firma>
    {
        event ZuFirma ZuFirma;
        event FirmaLoeschen FirmaLoeschen;
    }
}