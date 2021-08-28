using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using NoteApp;
using GalaSoft.MvvmLight.Command;
using ViewModel.WindowsVM;
using ViewModel.Service;

namespace ViewModel.ControlsVM
{
	/// <summary>
	/// View model for control NotesVM.
	/// </summary>
	public class NotesVM : ViewModelBase
	{
		/// <summary>
		/// List of all notes.
		/// </summary>
		public ObservableCollection<Note> Notes;

		/// <summary>
		/// Selected note.
		/// </summary>
		private Note _selectedNote;

		/// <summary>
		/// Selected category.
		/// </summary>
		private Category? _selectedCategory;

		/// <summary>
		/// List of finded notes.
		/// </summary>
		private ObservableCollection<Note> _findedNotes;

		/// <summary>
		/// Returns and sets Add command.
		/// </summary>
		public RelayCommand AddCommand { get; set; }

		/// <summary>
		/// Returns and sets Remove command.
		/// </summary>
		public RelayCommand RemoveCommand { get; set; }

		/// <summary>
		/// Returns and sets Edit command.
		/// </summary>
		public RelayCommand EditCommand { get; set; }

		/// <summary>
		/// Responsible for calling the MessageBox.
		/// </summary>
		private IMessageBoxService _messageBoxService;

		/// <summary>
		/// Responsible for calling the NoteWindowService.
		/// </summary>
		private INoteWindowService _noteWindowService;

		/// <summary>
		/// Returns and sets selected note.
		/// </summary>
		public Note SelectedNote
		{
			get
			{
				return _selectedNote;
			}
			set
			{
				_selectedNote = value;
				RaisePropertyChanged(nameof(SelectedNote));
			}
		}

		/// <summary>
		/// Returns and sets selected category.
		/// </summary>
		public Category? SelectedCategory
		{
			get
			{
				return _selectedCategory;
			}
			set
			{

				_selectedCategory = value;
				RaisePropertyChanged(nameof(SelectedCategory));
			}
		}

		/// <summary>
		/// Returns and sets selected finded notes.
		/// </summary>
		public ObservableCollection<Note> FindedNotes
		{
			get
			{
				return _findedNotes;
			}
			set
			{
				_findedNotes = Project.SortingNotes(SelectedCategory, value);
				RaisePropertyChanged(nameof(FindedNotes));
			}
		}

		/// <summary>
		/// Add command note.
		/// </summary>
		private void Add()
		{
			NoteWindowVM window = new NoteWindowVM(new Note(), _noteWindowService);

			_noteWindowService.Open(window);

			if (_noteWindowService.DialogResult)
			{
				Notes.Add(window.Note);
			}

			FindedNotes = Notes;
		}

		/// <summary>
		/// Remove command note.
		/// </summary>
		private void Remove()
		{
			var selectedNote = SelectedNote;

			if (selectedNote == null)
			{
				_messageBoxService.Show("Select Note!");
				return;
			}

			Notes.Remove(selectedNote);
			FindedNotes = Notes;
		}

		/// <summary>
		/// Edit command note.
		/// </summary>
		private void Edit()
		{
			var selectedNote = SelectedNote;

			if (selectedNote == null)
			{
				_messageBoxService.Show("Select Note!");
				return;
			}

			NoteWindowVM window = new NoteWindowVM((Note)selectedNote.Clone(), _noteWindowService);

			_noteWindowService.Open(window);

			if (_noteWindowService.DialogResult)
			{
				var index = Notes.IndexOf(selectedNote);
				Notes[index] = window.Note;
			}

			FindedNotes = Notes; 
		}

		/// <summary>
		/// Creates a notes list.
		/// </summary>
		/// <param name="messageBoxService">MessageBox service.</param>
		/// <param name="noteWindowService">NoteWindow service.</param>
		/// <param name="notes">Notes.</param>
		public NotesVM(IMessageBoxService messageBoxService,
			INoteWindowService noteWindowService, ObservableCollection<Note> notes)
		{
			SelectedCategory = Category.All;
			Notes = FindedNotes = notes;

			_messageBoxService = messageBoxService;
			_noteWindowService = noteWindowService;

			AddCommand = new RelayCommand(Add);
			RemoveCommand = new RelayCommand(Remove);
			EditCommand = new RelayCommand(Edit);
		}
	}
}
