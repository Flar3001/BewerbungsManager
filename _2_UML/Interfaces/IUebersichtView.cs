using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace _2_UML.Interfaces
{
    public interface IUebersichtView<T>: IView
    {
        ObservableCollection<T> AngezeigteObjekte { get; set; }

        void ZeigeAlleObjekte(ObservableCollection <T> ts);
        void ZeigeAusgewaehlteObjekte();
        void AktualisiereObjekt(T Objekt);
        void LoeseSpeichernAus(T Objekt);
    }
}