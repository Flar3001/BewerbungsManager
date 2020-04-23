using _2_UML.Interfaces;
using _2_UML.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Models;

namespace _2_UML.Controller
{
    public class TeilnehmerUebersichtController : UebersichtController<Models.Teilnehmer>, IController
    {
        public TeilnehmerUebersichtController()
        {
            TeilnehmerUebersichtView = new TeilnehmerUebersichtView();

            TeilnehmerUebersichtView.ZeigeViewFertig += SeiteWechseln;
            TeilnehmerUebersichtView.ZurStartseite += ZurStartseite;
            TeilnehmerUebersichtView.ObjektHinzufuegen += TeilnehmerHinzufuegen;
            TeilnehmerUebersichtView.TeilnehmerLoeschen += TeilnehmerLoeschen;


            NavigationsHistorie.Add(this);
            TeilnehmerUebersichtView.ZeigeView();
        }


        //public ITeilnehmerUebersichtView TeilnehmerUebersichtView = new TeilnehmerUebersichtView();
        public ITeilnehmerUebersichtView TeilnehmerUebersichtView { get; set; }

        public void SeiteNeuLaden()
        {
            TeilnehmerUebersichtView.ZeigeView();
        }

        private void TeilnehmerHinzufuegen()
        {

        }

        private void TeilnehmerLoeschen(AngezeigterTeilnehmer angezeigterTeilnehmer)
        {

        }
    }
}