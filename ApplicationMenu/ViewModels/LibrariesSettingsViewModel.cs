using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class LibrariesSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly IWindowService _windowService;
        private string selectedItem;

        public LibrariesSettingsViewModel(IWindowService windowService)
        {
            _windowService = windowService;

            Libraries = new ObservableCollection<string>();

            AddCommand = new DelegateCommand(AddCommandExecute);
            RemoveCommand = new DelegateCommand(RemoveCommandExecute, RemoveCommandCanExecute);
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public ObservableCollection<string> Libraries { get; }

        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Title => "Librairies";

        private void AddCommandExecute()
        {
            var path = _windowService.ShowFolderDialog();
            if (path != null)
            {
                Libraries.Add(path);
            }
        }

        private void RemoveCommandExecute()
        {
            Libraries.Remove(SelectedItem);
        }

        private bool RemoveCommandCanExecute()
            => SelectedItem != null;
    }
}