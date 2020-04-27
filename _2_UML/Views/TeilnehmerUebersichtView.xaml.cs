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

        public ObservableCollection<AngezeigterTeilnehmer> AngezeigteObjekte {get; set; }

        public event SeitenAnsicht ZeigeViewFertig;
        public event ZurStartseite ZurStartseite;
        public event ObjektHinzufuegen ObjektHinzufuegen;
        public event TeilnehmerLoeschen TeilnehmerLoeschen;
        public event ZuTeilnehmer ZuTeilnehmer;
        public event ZuBewerbungen ZuBewerbungen;

        public void ObjektHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            ObjektHinzufuegen();
        }

        public void ObjektLoeschenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlterTeilnehmer = ((Button)sender).Tag as Models.AngezeigterTeilnehmer;

            MessageBoxResult result = MessageBox.Show($"Sie sind dabei, {AusgewaehlterTeilnehmer.Vorname} {AusgewaehlterTeilnehmer.Name} aus dem System zu Löschen. " +
                $"Dies wird alle zu diesem Teilnehmer gehörigen Daten vollständig aus dem System entfernen. Sind Sie sich absolut sicher, dass Sie dies tun möchten?", "Sicher?", MessageBoxButton.YesNo);

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

        public void ZeigeAlleObjekte(ObservableCollection<AngezeigterTeilnehmer> ts)
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

        public void GeheZuBewerbungenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlterTeilnehmer = ((Button)sender).Tag as AngezeigterTeilnehmer;

            ZuBewerbungen(TeilnehmerAendern(AusgewaehlterTeilnehmer));
        }

        public void GeheZuProfilButton(object sender, RoutedEventArgs e)
        {
            AngezeigterTeilnehmer AusgewaehlterTeilnehmer = (AngezeigterTeilnehmer)((TextBlock)sender).BindingGroup.Items[0];

            ZuTeilnehmer(TeilnehmerAendern(AusgewaehlterTeilnehmer));
        }

        /// <summary>
        /// Transformiert den AngezeigtenTeilnehmer, welcher nur für diese Anzeige genutzt wird, wieder zu einem normalen Teilnehmer
        /// </summary>
        /// <param name="angezeigterTeilnehmer">Der spezielle Teilnehmer</param>
        /// <returns>Einen normalen Teilnehmer</returns>
        private Models.Teilnehmer TeilnehmerAendern(AngezeigterTeilnehmer angezeigterTeilnehmer)
        {
            return new Models.Teilnehmer
            {
                Id = angezeigterTeilnehmer.Id,
                Adresse = angezeigterTeilnehmer.Adresse,
                Ausbilder = angezeigterTeilnehmer.Ausbilder,
                Beruf = angezeigterTeilnehmer.Beruf,
                EMail = angezeigterTeilnehmer.EMail,
                Name = angezeigterTeilnehmer.Name,
                Nutzer = angezeigterTeilnehmer.Nutzer,
                Telefonnummer = angezeigterTeilnehmer.Telefonnummer,
                Vorname = angezeigterTeilnehmer.Vorname
            };
        }
    }
}
