using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2_UML.Views;
using _2_UML.Models;
using _2_UML.Persistence;
using System.Collections.ObjectModel;

namespace _2_UML.Controller
{
    public class FirmenUebersichtController : UebersichtController<Firma>,IController
    {
        public FirmenUebersichtController()
        {
            FirmenuebersichtView = new FirmenuebersichtView();
            FirmenuebersichtView.ZeigeAlleObjekte(AlleFirmen());

            FirmenuebersichtView.FirmaLoeschen += FirmaLoeschen;
            FirmenuebersichtView.ObjektHinzufuegen += FirmaHinzufuegen;
            FirmenuebersichtView.ZeigeViewFertig += SeiteWechseln;
            FirmenuebersichtView.ZuFirma += FirmaAnzeigen;
            FirmenuebersichtView.ZurStartseite += ZurStartseite;
            FirmenuebersichtView.ZuBewerbungen += ZuBewerbungen;

            NavigationsHistorie.Add(this);
            FirmenuebersichtView.ZeigeView();
        }

        public IFirmenuebersichtView FirmenuebersichtView { get; set; }

        public void SeiteNeuLaden()
        {
            FirmenuebersichtView.ZeigeView();
        }

        private ObservableCollection<AngezeigteFirma> AlleFirmen()
        {
            ObservableCollection<AngezeigteFirma> ObservableAlleFirmen = new ObservableCollection<AngezeigteFirma>();
            List<AngezeigteFirma> alleFirmen = MySQLHandler.AlleFirmen();

            foreach(AngezeigteFirma angezeigteFirma in alleFirmen)
            {
                ObservableAlleFirmen.Add(angezeigteFirma);
            }

            return ObservableAlleFirmen;
        }

        private void FirmaLoeschen(AngezeigteFirma firma)
        {
            if (MySQLHandler.RemoveFirma(firma))
            {
                FirmenuebersichtView.AngezeigteObjekte.Remove(firma);
                FirmenuebersichtView.ZeigeMessageBox("Die Firma wurde erfolgreich aus dem System gelöscht");
            }
            else
            {
                FirmenuebersichtView.ZeigeMessageBox($"Folgender Fehler ist aufgetreten: {MySQLHandler.Errormessage}");
            }
        }

        private void FirmaHinzufuegen()
        {
            //FirmaAnlegenBearbeitenController firmaAnlegenBearbeitenController = new FirmaAnlegenBearbeitenController(new Firma { });
        }

        private void FirmaAnzeigen(Firma firma)
        {
            FirmenAnsichtController firmenAnsichtController = new FirmenAnsichtController(firma);
        }

        private void ZuBewerbungen(Firma firma)
        {

        }
    }
}