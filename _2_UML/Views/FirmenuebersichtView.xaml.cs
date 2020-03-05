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

        public void AktualisiereObjekt(Firma Objekt)
        {
            throw new NotImplementedException();
        }

        public void LoeseSpeichernAus(Firma Objekt)
        {
            throw new NotImplementedException();
        }

        public void ZeigeAlleObjekte()
        {
            throw new NotImplementedException();
        }

        public void ZeigeAlleObjekte(ObservableCollection<Firma> ts)
        {
            throw new NotImplementedException();
        }

        public void ZeigeAusgewaehlteObjekte()
        {
            throw new NotImplementedException();
        }

        public void ZeigeBestaetigungsbox(string AreYouSureMessage)
        {
            throw new NotImplementedException();
        }

        public void ZeigeView()
        {
            throw new NotImplementedException();
        }
    }
}
