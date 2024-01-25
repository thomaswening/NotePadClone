using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.Services
{
    internal class WindowService : IWindowService
    {
        public void OpenNewWindow() 
        {
            var window = new MainWindow();
            window.Show();
        }
    }
}
