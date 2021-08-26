using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NoteApp;
using ViewModel.Service;

namespace ViewModel.WindowsVM
{
    public class NoteWindowVM
    {
        private Note _note;

        public Note Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
            }
        }

        public RelayCommand OKCommand { get; set; }

        public RelayCommand CancelCommand { get; set; }

        public NoteWindowVM(Note note, INoteWindowService noteWindowService)
        {
            Note = note;
            OKCommand = noteWindowService.OKCommand;
            CancelCommand = noteWindowService.CancelCommand;
        }
    }
}
