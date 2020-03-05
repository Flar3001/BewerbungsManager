using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace _2_UML.Interfaces
{
    public delegate void Logout();

    public delegate void Firmen();
    public delegate void Bewerbungen();
    public delegate void Profil();
    public delegate void Teilnehmer();
    public delegate void Ausbilder();
    public delegate void Berufe();

    public interface IStartseiteView : IView
    {
        event Logout LoeseLogoutAus;

        event Firmen GeheZuFirmenUebersicht;
        event Bewerbungen GeheZuBewerbungenUebersicht;
        event Profil GeheZuTeilnehmerAnsicht;
        event Teilnehmer GeheZuTeilnehmerUebersicht;
        event Ausbilder GeheZuAusbilderUebersicht;
        event Berufe GeheZuBerufeUebersicht;

        void NameUndOptionenEinstellen(string Username, bool istAusbilder);
    }
}