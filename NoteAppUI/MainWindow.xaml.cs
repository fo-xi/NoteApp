using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Command;
using NoteAppUI.Service;
using NoteAppUI.Windows;
using ViewModel;

namespace NoteAppUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
	    MainWindowVM _mainWindow = new MainWindowVM(new MessageBoxService(), new NoteWindowService());

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainWindow;

            ShowAboutWindowMenuItem.Command = new RelayCommand(OpenAbout);
            Exit.Command = new RelayCommand(ExitMainWindow);

            Closing += ClosingMainWindow;
        }

        private void OpenAbout()
        {
	        AboutWindow aboutWindow = new AboutWindow();
	        aboutWindow.ShowDialog();
        }

        private void ExitMainWindow()
        {
	        _mainWindow.Save();
	        Close();
        }

        private void ClosingMainWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
	        _mainWindow.Save();
        }
    }
}
