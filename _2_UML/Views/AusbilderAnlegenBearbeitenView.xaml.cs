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
using _2_UML.Interfaces;
using _2_UML.Models;

namespace _2_UML.Views
{
    /// <summary>
    /// Interaktionslogik für AusbilderAnlegenBearbeitenView.xaml
    /// </summary>
    public partial class AusbilderAnlegenBearbeitenView : Page, IAusbilderAnlegenBearbeitenView
    {
        public AusbilderAnlegenBearbeitenView()
        {
            InitializeComponent();
        }

        public event SeitenAnsicht ZeigeViewFertig;

        public void ZeigeObjekte(List<Models.Ausbilder> Objekte)
        {
            throw new NotImplementedException();
        }

        public void ZeigeObjekte(List<Interfaces.Ausbilder> Objekte)
        {
            throw new NotImplementedException();
        }

        public void ZeigeView()
        {
            throw new NotImplementedException();
        }
    }
}
