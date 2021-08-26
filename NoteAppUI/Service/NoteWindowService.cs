using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using NoteAppUI.Windows;
using ViewModel.Service;
using ViewModel.WindowsVM;

namespace NoteAppUI.Service
{
	public class NoteWindowService : INoteWindowService
	{
		private NoteWindow _noteWindowWindow;

		public bool DialogResult { get; set; }

		public void Open(NoteWindowVM note)
		{
			_noteWindowWindow = new NoteWindow();
			_noteWindowWindow.DataContext = note;
			_noteWindowWindow.ShowDialog();
		}

		public RelayCommand OKCommand { get; set; }

		public RelayCommand CancelCommand { get; set; }

		private void OK()
		{
			DialogResult = true;
			Close();
		}

		private void Cancel()
		{
			DialogResult = false;
			Close();
		}

		private void Close()
		{
			_noteWindowWindow.Close();
		}

		public NoteWindowService()
		{
			OKCommand = new RelayCommand(OK);
			CancelCommand = new RelayCommand(Cancel);
		}
	}
}
