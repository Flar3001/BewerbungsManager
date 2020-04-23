using System;
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
    public partial class TeilnehmerUebersichtView : BasePage, ITeilnehmerUebersichtView
    {
        public TeilnehmerUebersichtView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ObservableCollection<AngezeigterTeilnehmer> AngezeigteObjekte {get;set;}

        public event SeitenAnsicht ZeigeViewFertig;
        public event ZurStartseite ZurStartseite;
        public event ObjektHinzufuegen ObjektHinzufuegen;
        public event TeilnehmerLoeschen TeilnehmerLoeschen;
        public event ZuTeilnehmer ZuTeilnehmer;

        public void ObjektHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ObjektLoeschenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlterTeilnehmer = ((Button)sender).Tag as Models.AngezeigterTeilnehmer;

            MessageBoxResult result = MessageBox.Show($"Sie sind dabei, {AusgewaehlterTeilnehmer.Vorname} {AusgewaehlterTeilnehmer.Name} aus dem System zu Löschen. Sind Sie sich sicher, dass Sie dies tun möchten?", "Sicher?", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    TeilnehmerLoeschen(AusgewaehlterTeilnehmer);
                    break;
                case MessageBoxResult.No:
                default:
                    break;
            }
        }

        public void ZeigeAlleObjekte(ObservableCollection<Models.AngezeigterTeilnehmer> ts)
        {
            AngezeigteObjekte = ts;
        }

        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }

        public void ZurStartseiteButton(object sender, RoutedEventArgs e)
        {
            ZurStartseite();
        }
    }
}
