using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Drawing;

namespace _2_UML.Interfaces
{
    //Muss außerhalb des Interfaces als Public erstellt werden. Ist eine Art Zeiger auf eine Methode 
    //(und muss dieselben Parameter beinhalten wie die Methode, auf die der Delegate später zeigen wird.)
    public delegate void LoginHandler(string name, string passwort);

    public delegate void SeitenAnsicht(Page page);

    public interface ILoginView : IView
    {
        event LoginHandler UeberpruefeLogin;      

        void MeldungBearbeiten(string meldung, string farbe);
    }
}