using GalaSoft.MvvmLight;
using NoteApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using ViewModel.ControlsVM;
using ViewModel.Service;

namespace ViewModel
{
    /// <summary>
    /// View Model for window MainWindow.
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        /// <summary>
        /// Stores a list of all notes created in the application.
        /// </summary>
        private Project _project;

        /// <summary>
        /// View Model notes.
        /// </summary>
        private NotesVM _notesVM;

        /// <summary>
        /// Old found text.
        /// </summary>
        private Category? _oldFindText = null;

        /// <summary>
        /// Returns and sets Add command.
        /// </summary>
        public RelayCommand AddCommand { get; set; }

        /// <summary>
        /// Returns and sets Edit command.
        /// </summary>
        public RelayCommand EditCommand { get; set; }

        /// <summary>
        /// Returns and sets Remove command.
        /// </summary>
        public RelayCommand RemoveCommand { get; set; }

        /// <summary>
        /// Returns and sets View Model notes.
        /// </summary>
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

        /// <summary>
        /// Creation of information about all notes.
        /// </summary>
        /// <param name="messageBoxService"></param>
        /// <param name="noteWindowService"></param>
        public MainWindowVM(IMessageBoxService messageBoxService,
            INoteWindowService noteWindowService)
        {
            _project = ProjectManager.ReadFromFile();
            NotesVM = new NotesVM(messageBoxService,
                noteWindowService, Project.SortingNotes(_project.Notes));

            NotesVM.PropertyChanged += OnTextChanged;

            AddCommand = NotesVM.AddCommand;
            EditCommand = NotesVM.EditCommand;
            RemoveCommand = NotesVM.AddCommand;
        }

        /// <summary>
        /// Saving list of all notes to a file.
        /// </summary>
        public void Save()
        {
            _project.Notes = NotesVM.Notes;
            ProjectManager.WriteToFile(_project);
        }

        /// <summary>
        /// Finding notes when changing the search bar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
