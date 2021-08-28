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
	public class NotesVM : ViewModelBase
	{
		public ObservableCollection<Note> Notes;

		private Note _selectedNote;

		private Category? _selectedCategory;

		private ObservableCollection<Note> _findedNotes;

		public RelayCommand AddCommand { get; set; }

		public RelayCommand RemoveCommand { get; set; }

		public RelayCommand EditCommand { get; set; }

		private IMessageBoxService _messageBoxService;

		private INoteWindowService _noteWindowService;

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

		private void Add()
		{
			NoteWindowVM window = new NoteWindowVM(new Note(), _noteWindowService);

			_noteWindowService.Open(window);

			if (_noteWindowService.DialogResult)
			{
				Notes.Add(window.Note);
			}
		}

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

		public NotesVM(IMessageBoxService messageBoxService,
			INoteWindowService noteWindowService, ObservableCollection<Note> notes)
		{
			SelectedCategory = null;
			Notes = FindedNotes = notes;

			_messageBoxService = messageBoxService;
			_noteWindowService = noteWindowService;

			AddCommand = new RelayCommand(Add);
			RemoveCommand = new RelayCommand(Remove);
			EditCommand = new RelayCommand(Edit);
		}
	}
}
