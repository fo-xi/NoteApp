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
    public interface INoteWindowService
    {
        bool DialogResult { get; set; }

        void Open(NoteVM note);

        RelayCommand OKCommand { get; set; }

        RelayCommand CancelCommand { get; set; }
    }
}
