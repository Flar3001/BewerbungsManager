using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _2_UML.Interfaces
{
    interface IMainWindow : IView
    {
        void ChangeFrameContent(Page page);
    }
}
