﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace _2_UML.Interfaces
{
    //Hinweis: ObjektLoeschen kann kein ganzes Objekt unbekanntes Model vorgeben, das hast du schon 4-mal versucht
    //public delegate void ObjektLoeschen(int objektid);
    public delegate void ZurStartseite();
    public delegate void ObjektHinzufuegen();

    public interface IUebersichtView<T>: IView
    {
        ObservableCollection<T> AngezeigteObjekte { get; set; }

        void ZeigeMessageBox(string Nachricht);
        void ZeigeAlleObjekte(ObservableCollection <T> ts);
        void ObjektLoeschenButton(object sender, RoutedEventArgs e);
        void ObjektHinzufuegenButton(object sender, RoutedEventArgs e);
        void ZurStartseiteButton(object sender, RoutedEventArgs e);

        //event ObjektLoeschen AusbilderLoeschen;
        event ZurStartseite ZurStartseite;
        event ObjektHinzufuegen ObjektHinzufuegen;
    }
}