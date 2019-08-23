﻿using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class LibrariesSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly IWindowService _windowService;
        private readonly IUserConfig _userConfig;
        private readonly IDataService _dataService;
        private string selectedItem;

        public LibrariesSettingsViewModel(IWindowService windowService,
                                          IUserConfig config,
                                          IDataService dataService)
        {
            _windowService = windowService;
            _dataService = dataService;
            _userConfig = config;

            Libraries = new ObservableCollection<string>(config.Libraries);

            AddCommand = new DelegateCommand(AddCommandExecute);
            RemoveCommand = new DelegateCommand(RemoveCommandExecute, RemoveCommandCanExecute);
            SyncCommand = new DelegateCommand(SyncCommandExecute, SyncCommandCanExecute);
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand SyncCommand { get; }
        public ObservableCollection<string> Libraries { get; }

        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                RemoveCommand.RaiseCanExecuteChanged();
                SyncCommand.RaiseCanExecuteChanged();
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

        private async void SyncCommandExecute()
        {
            var files = Directory.GetFiles(SelectedItem, "*", SearchOption.AllDirectories);
            await _dataService.AddBooksAsync(files);
        }

        private bool RemoveCommandCanExecute()
            => SelectedItem != null;

        private bool SyncCommandCanExecute()
            => SelectedItem != null;
    }
}