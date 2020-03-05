using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_UML.Models;

namespace _2_UML.Interfaces
{
    public delegate void GeheZurueck();
    public delegate void SpeichereNeuenAusbilder(Models.Ausbilder neuerAusbilder);
    public delegate void SpeichereNeuenTeilnehmer(Models.Teilnehmer neuerTeilnehmer);
    public delegate void SpeichereAenderungenAusbilder(Models.Ausbilder geaenderterAusbilder);
    public delegate void SpeichereAenderungenTeilnehmer(Models.Teilnehmer geaenderterTeilnehmer);

    public interface INutzerAnlegenBearbeitenView : IView
    {
        void DatenAnzeigen(Models.Ausbilder ausbilder, bool neuErschaffen, List<Sicherheitsfrage> AlleSicherheitsfragen);
        void DatenAnzeigen(Models.Teilnehmer teilnehmer, bool neuErschaffen,List<Models.Ausbilder>AlleAusbilder, List<Sicherheitsfrage> AlleSicherheitsfragen, List<Beruf>AlleBerufe);
        void ZeigeFehlermeldung(string Fehlermeldung);

        event GeheZurueck GeheZurueck;
        event SpeichereNeuenAusbilder SpeichereNeuenAusbilder;
        event SpeichereNeuenTeilnehmer SpeichereNeuenTeilnehmer;
        event SpeichereAenderungenAusbilder SpeichereAenderungenAusbilder;
        event SpeichereAenderungenTeilnehmer SpeichereAenderungenTeilnehmer;

    }
}
