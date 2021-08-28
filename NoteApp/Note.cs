using GalaSoft.MvvmLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
	public class Note : NotifyDataErrorInfoBase, ICloneable
    {
        /// <summary>
        /// Minimum string length.
        /// </summary>
        private const int MinLength = 1;

        /// <summary>
        /// Maximum line length.
        /// </summary>
        private const int MaxLength = 50;

        /// <summary>
        /// Note title.
        /// </summary>
        private string _title;

        /// <summary>
        /// Note category.
        /// </summary>
        private Category? _noteCategory;

        /// <summary>
        /// Note text.
        /// </summary>
        private string _text;

        /// <summary>
        /// When the note was last modified.
        /// </summary>
		private DateTime _lastModifiedTime;

        /// <summary>
        /// Title line state.
        /// </summary>
        private StateOfView _titleState = StateOfView.Initial;

	    public override bool HasErrors
        {
	        get
	        {
		        return base.HasErrors || string.IsNullOrEmpty(Title);
		    }
        }

        /// <summary>
        /// Returns and sets the title of the note.
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_titleState == StateOfView.Updated)
                {
                    base.Validate(value, MinLength, MaxLength, nameof(Title));
                }

                if (string.IsNullOrEmpty(value))
                {
	                _title = "Без названия";
                }
                else
                {
	                _title = value;
                }

                LastModifiedTime = DateTime.Now;
                RaisePropertyChanged(nameof(Title));
                RaisePropertyChanged(nameof(HasErrors));

                _titleState = StateOfView.Updated;
            }
        }

        /// <summary>
        /// Returns and sets the category of the note.
        /// </summary>
        public Category? NoteCategory
        {
            get
            {
                return _noteCategory;
            }
            set
            {
                _noteCategory = value;
                LastModifiedTime = DateTime.Now;
                RaisePropertyChanged(nameof(NoteCategory));
            }
        }

        /// <summary>
        /// Returns and sets the text of the note.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                LastModifiedTime = DateTime.Now;
                RaisePropertyChanged(nameof(Text));
            }
        }

        /// <summary>
        /// Returns and sets the time when the note was created.
        /// </summary>
        public DateTime TimeCreation { get; }

        /// <summary>
        /// Returns and sets the time the note was last modified.
        /// </summary>
        public DateTime LastModifiedTime
        {
            get
            {
                return _lastModifiedTime;
            }
            private set
            {
                _lastModifiedTime = value;
                RaisePropertyChanged(nameof(LastModifiedTime));
            }
        }

        /// <summary>
        /// Create a note.
        /// </summary>
        /// <param name="title">Note title.</param>
        /// <param name="noteCategory">Note category.</param>
        /// <param name="text">Note text.</param>
        /// <param name="timeCreation">Time of note creation.</param>
        /// <param name="lastModifiedTime">When the note was last modified.</param>
        public Note(string title, Category? noteCategory, 
            string text, DateTime lastModifiedTime)
        {
	        Title = title;
            NoteCategory = noteCategory;
            Text = text;
            TimeCreation = DateTime.Now;
            LastModifiedTime = lastModifiedTime;
        }

        /// <summary>
        /// Creates a note.
        /// </summary>
        public Note() : this(string.Empty, null, string.Empty,
            DateTime.Now)
        {

        }

        /// <summary>
        /// Makes a copy of the object <see cref="Note"/>
        /// </summary>
        /// <returns></returns>
		public object Clone()
		{
            return new Note(
                Title = this.Title,
                NoteCategory = this.NoteCategory,
                Text = this.Text,
                LastModifiedTime = this.LastModifiedTime);
		}
	}
}
