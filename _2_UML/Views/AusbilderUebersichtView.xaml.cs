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
    /// Interaktionslogik für AusbilderUebersichtView.xaml
    /// </summary>
    public partial class AusbilderUebersichtView : BasePage, IAusbilderUebersichtView
    {
        public AusbilderUebersichtView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public event AusbilderLoeschen AusbilderLoeschen;
        public event ZuAusbilder ZuAusbilder;
        public event ZurStartseite ZurStartseite;
        public event AusbilderHinzufuegen AusbilderHinzufuegen;

        public ObservableCollection<Models.Ausbilder> AngezeigteObjekte { get; set; }

        public event SeitenAnsicht ZeigeViewFertig;

        public void AktualisiereObjekt(Models.Ausbilder Objekt)
        {
            throw new NotImplementedException();
        }

        public void LoeseSpeichernAus(Models.Ausbilder Objekt)
        {
            throw new NotImplementedException();
        }


        public void ZeigeAlleObjekte(ObservableCollection<Models.Ausbilder> ausbilders)
        {
            AngezeigteObjekte = ausbilders;
        }

        public void ZeigeAusgewaehlteObjekte()
        {
            throw new NotImplementedException();
        }


        public void ZeigeView()
        {
            ZeigeViewFertig(this);
        }


        //Nachfragen: Soll jeder Ausbilder einfach so andere Ausbilder löschen können?
        private void AusbilderLoeschenButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlterAusbilder = ((Button)sender).Tag as Models.Ausbilder;

            MessageBoxResult result = MessageBox.Show($"Sie sind dabei, {AusgewaehlterAusbilder.Vorname} {AusgewaehlterAusbilder.Name} aus dem System zu Löschen. Sind Sie sich sicher, dass Sie dies tun möchten?","Sicher?",MessageBoxButton.YesNo);

            switch(result)
            {
                case MessageBoxResult.Yes:
                    AusbilderLoeschen(AusgewaehlterAusbilder.Id);
                    break;
                case MessageBoxResult.No:
                default:
                    break;
            }
        }

        private void GeheZuProfilButton(object sender, RoutedEventArgs e)
        {
            var AusgewaehlterAusbilder = ((Button)sender).Tag as Models.Ausbilder;
            ZuAusbilder(AusgewaehlterAusbilder);
        }

        private void AusbilderHinzufuegenButton(object sender, RoutedEventArgs e)
        {
            AusbilderHinzufuegen();
        }

        private void ZurStartseiteButton(object sender, RoutedEventArgs e)
        {
            ZurStartseite();
        }
    }
}
