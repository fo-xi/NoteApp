using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WindowsVM;

namespace ViewModel.Service
{
    /// <summary>
    /// Сlass responsible for showing window.
    /// </summary>
    public interface INoteWindowService
    {
        /// <summary>
        /// DialogResult.
        /// </summary>
        bool DialogResult { get; set; }

        /// <summary>
        /// Opens a window for adding and editing a note.
        /// </summary>
        /// <param name="note">Note</param>
        void Open(NoteWindowVM note);

        /// <summary>
        /// Returns and sets OK command.
        /// </summary>
        RelayCommand OKCommand { get; set; }

        /// <summary>
        /// Returns and sets Cancel command.
        /// </summary>
        RelayCommand CancelCommand { get; set; }
    }
}
