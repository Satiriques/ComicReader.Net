using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System;
using System.Windows.Input;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class FileMenuViewModel : ViewModelBase, IFileMenuViewModel
    {
        private readonly IWindowService _windowService;

        public FileMenuViewModel(IWindowService windowService)
        {
            PreferencesCommand = new DelegateCommand(OnPreferencesCommandExecute);
            _windowService = windowService;
        }

        private void OnPreferencesCommandExecute()
        {
            _windowService.ShowPreferences();
        }

        public ICommand PreferencesCommand { get; }
    }
}