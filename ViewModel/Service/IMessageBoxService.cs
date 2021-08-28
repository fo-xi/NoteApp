using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Service
{
    /// <summary>
    /// The interface responsible for displaying the MessageBox.
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Show MessageBox.
        /// </summary>
        /// <param name="text"></param>
        void Show(string text);
    }
}
