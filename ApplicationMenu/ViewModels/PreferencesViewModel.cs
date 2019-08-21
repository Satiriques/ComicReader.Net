using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class PreferencesViewModel : ViewModelBase
    {
        private ISettingsViewModel _selectedViewModel;

        public PreferencesViewModel(LibrariesSettingsViewModel librariesSettingsViewModel,
                                    AdvancedSettingsViewModel advancedSettingsViewModel)
        {
            LibrariesSettingsViewModel = librariesSettingsViewModel;
            AdvancedSettingsViewModel = advancedSettingsViewModel;

            MenuViewModels = new ObservableCollection<ISettingsViewModel>
            {
                librariesSettingsViewModel,
                advancedSettingsViewModel
            };
            OnPropertyChanged(nameof(MenuViewModels));
        }

        public AdvancedSettingsViewModel AdvancedSettingsViewModel { get; }

        public LibrariesSettingsViewModel LibrariesSettingsViewModel { get; }

        public ObservableCollection<ISettingsViewModel> MenuViewModels { get; set; }

        public ISettingsViewModel SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                if (_selectedViewModel != value)
                {
                    _selectedViewModel = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}