using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using _2_UML.Interfaces;
using _2_UML.Views;

namespace _2_UML.Controller
{
    public static class MainWindowController
    {
        static IMainWindow MainWindow;

        public static void MainWindowControllerStart()
        {
            MainWindow = new MainWindow();
            MainWindow.ZeigeView();
        }

        public static void NavigateTo(Page page)
        {
            MainWindow.ChangeFrameContent(page);
        }
    }

    public class MainWindowController2
    {
        public MainWindowController2()
        {
            MainWindowController.MainWindowControllerStart();
        }
    }
}
