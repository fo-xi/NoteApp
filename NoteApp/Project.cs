using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
	/// <summary>
	/// Project class.
	/// </summary>
	public class Project : ViewModelBase
	{
		/// <summary>
		/// List of all notes created in the app.
		/// </summary>
		private ObservableCollection<Note> _notes;

		/// <summary>
		/// Current note.
		/// </summary>
		private Note _currentNote;

		/// <summary>
		/// Returns and sets the list of all notes created in the app.
		/// </summary>
		public ObservableCollection<Note> Notes
		{
			get
			{
				return _notes;
			}
			set
			{
				_notes = value;
				RaisePropertyChanged(nameof(Notes));
			}
		}

		public Project()
		{
			Notes = new ObservableCollection<Note>();
		}

		/// <summary>
		/// Sort the list by date modified.
		/// </summary>
		/// <param name="notes">All notes in the app.</param>
		/// <returns></returns>
		public static ObservableCollection<Note> SortingNotes(ObservableCollection<Note> notes)
		{
			return new ObservableCollection<Note>(notes.OrderBy(note => note.LastModifiedTime));
		}

		/// <summary>
		/// Sort the list by modification date belonging only to the specified category.
		/// </summary>
		/// <param name="noteCategory">Note category.</param>
		/// <param name="notes">All notes in the app.</param>
		/// <returns></returns>
		public static ObservableCollection<Note> SortingNotes(Category? noteCategory, 
			ObservableCollection<Note> notes)
		{
			if (noteCategory == Category.All)
			{
				return SortingNotes(notes);
			}

			bool result = notes.Any(note => note.NoteCategory == noteCategory);

			if (result)
			{
				return new ObservableCollection<Note>(notes.Where(note => note.NoteCategory == noteCategory)
						.OrderBy(note => note.LastModifiedTime));
			}
			else
			{
				return notes = new ObservableCollection<Note>();
			}
		}
	}
}
