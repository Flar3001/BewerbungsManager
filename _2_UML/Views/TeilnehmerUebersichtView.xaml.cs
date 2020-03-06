﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für TeilnehmerUebersichtView.xaml
    /// </summary>
    public partial class TeilnehmerUebersichtView : Page, ITeilnehmerUebersichtView
    {
        public TeilnehmerUebersichtView()
        {
            InitializeComponent();
        }

        public List<Models.Teilnehmer> AngezeigteObjekte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ObservableCollection<Interfaces.Teilnehmer> IUebersichtView<Interfaces.Teilnehmer>.AngezeigteObjekte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event SeitenAnsicht ZeigeViewFertig;
        public event ObjektLoeschen AusbilderLoeschen;
        public event ZurStartseite ZurStartseite;
        public event ObjektHinzufuegen AusbilderHinzufuegen;

        public void ObjektHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ObjektLoeschenButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ZeigeAlleObjekte(ObservableCollection<Interfaces.Teilnehmer> ts)
        {
            throw new NotImplementedException();
        }

        public void ZeigeView()
        {
            throw new NotImplementedException();
        }

        public void ZurStartseiteButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
