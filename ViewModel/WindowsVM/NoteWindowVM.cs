using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NoteApp;
using ViewModel.Service;

namespace ViewModel.WindowsVM
{
    /// <summary>
    /// View Model for window Note.
    /// </summary>
    public class NoteWindowVM : NotifyDataErrorInfoBase
    {
        /// <summary>
        /// Created or editable note.
        /// </summary>
        private Note _note;

        /// <summary>
        /// Returns and sets сreated or editable note.
        /// </summary>
        public Note Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                _note.PropertyChanged += NoteChanged;
            }
        }

        /// <summary>
        /// Returns access to the button.
        /// </summary>
        public bool IsCanClicked
        {
	        get
	        {
		        return !Note.HasErrors;
	        }
        }

        /// <summary>
        /// Returns and sets OK command.
        /// </summary>
        public RelayCommand OKCommand { get; set; }

        /// <summary>
        /// Returns and sets Cancel command.
        /// </summary>
        public RelayCommand CancelCommand { get; set; }

        /// <summary>
        /// Create a window for creating or editing a note.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="noteWindowService"></param>
        public NoteWindowVM(Note note, INoteWindowService noteWindowService)
        {
            Note = note;
            OKCommand = noteWindowService.OKCommand;
            CancelCommand = noteWindowService.CancelCommand;
        }

        /// <summary>
        /// Responsible for updating the access to the button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoteChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(IsCanClicked));
        }
    }
}
