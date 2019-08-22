using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Linq;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class LibrariesSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IUserConfig _userConfig;
        private string selectedItem;

        public LibrariesSettingsViewModel(IWindowService windowService,
                                          IUserConfig config)
        {
            _windowService = windowService;
            _userConfig = config;

            Libraries = new ObservableCollection<string>(config.Libraries);

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

        public void SaveConfig()
        {
            _userConfig.Libraries = Libraries.ToList();

            _userConfig.Save();
        }

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