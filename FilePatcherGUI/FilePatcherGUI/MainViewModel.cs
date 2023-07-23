using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilePatcherGUI
{
    internal class MainViewModel : ObservableObject
    {
        // Commands
        public RelayCommand MoveWindowCommand { get; set; }
        public RelayCommand MinimiseWindowCommand { get; set; }
        public RelayCommand ShutDownWindowCommand { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });
            ShutDownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });
            MinimiseWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });
        }
    }
}
