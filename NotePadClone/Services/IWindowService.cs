using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotePadClone.Services
{
    /// <summary>
    /// Service for handling window operations.
    /// </summary>
    public interface IWindowService
    {
        /// <summary>
        /// Opens a new window.
        /// </summary>
        void OpenNewWindow();
    }
}
