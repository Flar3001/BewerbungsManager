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
    public partial class FirmenuebersichtView : BasePage, IFirmenuebersichtView
    {
        public FirmenuebersichtView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ObservableCollection<AngezeigteFirma> AngezeigteObjekte { get; set; }

        public event SeitenAnsicht ZeigeViewFertig;
        public event FirmaLoeschen FirmaLoeschen;
        public event ZurStartseite ZurStartseite;
        public event ObjektHinzufuegen ObjektHinzufuegen;
        public event ZuFirma ZuFirma;
        public event ZuBewerbungenVonFirma ZuBewerbungen;

        public void ObjektHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            ObjektHinzufuegen();
        }

        public void ObjektLoeschenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlteFirma = ((Button)sender).Tag as AngezeigteFirma;

            MessageBoxResult result = MessageBox.Show($"Sie sind dabei, die Firma {AusgewaehlteFirma.Name} aus dem System zu Löschen. " +
                $"Dies wird alle zu dieser Firma gehörigen Daten vollständig aus dem System entfernen. Sind Sie sich absolut sicher, dass Sie dies tun möchten?", "Bestätigen", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    FirmaLoeschen(AusgewaehlteFirma);
                    break;
                case MessageBoxResult.No:
                default:
                    break;
            }
        }

        public void ZeigeAlleObjekte(ObservableCollection<AngezeigteFirma> ts)
        {
            AngezeigteObjekte = ts;
        }

        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        public void GeheZuFirmaProfilButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlteFirma = (AngezeigteFirma)((TextBlock)sender).BindingGroup.Items[0];

            ZuFirma(AusgewaehlteFirma);
        }

        public void ZurStartseiteButton(object sender, RoutedEventArgs e)
        {
            ZurStartseite();
        }

        public void GeheZuBewerbungenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlteFirma = (AngezeigteFirma)((TextBlock)sender).BindingGroup.Items[0];

            ZuBewerbungen(AusgewaehlteFirma);
        }

        /*
        private Firma FirmaTransformieren(AngezeigteFirma angezeigteFirma)
        {
            return new Firma
            {
                Id = angezeigteFirma.Id,
                Adresse = angezeigteFirma.Adresse,
                Beschreibung = angezeigteFirma.Beschreibung,
                BewerbungsEMailAdresse = angezeigteFirma.BewerbungsEMailAdresse,
                BewerbungsTelefonummer = angezeigteFirma.BewerbungsTelefonummer,
                Name = angezeigteFirma.Name,
            };
        }
        */
    }
}
