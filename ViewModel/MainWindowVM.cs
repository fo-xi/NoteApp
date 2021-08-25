using GalaSoft.MvvmLight;
using NoteApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ControlsVM;
using ViewModel.Service;

namespace ViewModel
{
    public class MainWindowVM : ViewModelBase
    {
        private Project _project;

        private NotesVM _notesVM;

        private Category? _oldFindText = null;

        public NotesVM NotesVM
        {
            get
            {
                return _notesVM;
            }
            set
            {
                _notesVM = value;
                RaisePropertyChanged(nameof(NotesVM));
            }
        }

        public MainWindowVM(IMessageBoxService messageBoxService,
            INoteWindowService noteWindowService)
        {
            _project = ProjectManager.ReadFromFile();
            NotesVM = new NotesVM(messageBoxService,
                noteWindowService, Project.SortingNotes(_project.Notes));

            NotesVM.PropertyChanged += OnTextChanged;
        }

        public void Save()
        {
            _project.Notes = NotesVM.Notes;
            ProjectManager.WriteToFile(_project);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            var listNotesVM = (NotesVM)sender;

            if (listNotesVM.SelectedCategory == _oldFindText)
            {
                return;
            }

            _oldFindText = listNotesVM.SelectedCategory;
            listNotesVM.FindedNotes = Project.SortingNotes
                (listNotesVM.SelectedCategory, NotesVM.Notes);
        }
    }
}
