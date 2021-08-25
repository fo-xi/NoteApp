using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Service;

namespace NoteAppUI.Service
{
	public class MessageBoxService : IMessageBoxService
	{
		public void Show(string text)
		{
			MessageBox.Show(text);
		}
	}
}
