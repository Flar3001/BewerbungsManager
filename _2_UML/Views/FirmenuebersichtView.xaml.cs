using _2_UML.Interfaces;
using System;
using System.Collections.Generic;
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
using _2_UML.Models;
using System.Collections.ObjectModel;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für FirmenuebersichtView.xaml
    /// </summary>
    public partial class FirmenuebersichtView : Page, IFirmenuebersichtView
    {
        public FirmenuebersichtView()
        {
            InitializeComponent();
        }

        public List<Firma> AngezeigteObjekte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        ObservableCollection<Firma> IUebersichtView<Firma>.AngezeigteObjekte { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event SeitenAnsicht ZeigeViewFertig;
        public event FirmaLoeschen FirmaLoeschen;
        public event ZurStartseite ZurStartseite;
        public event ObjektHinzufuegen AusbilderHinzufuegen;
        public event ZuFirma ZuFirma;

        public void ObjektHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ObjektLoeschenButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ZeigeAlleObjekte(ObservableCollection<Firma> ts)
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
