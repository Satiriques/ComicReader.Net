using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using System.Collections.ObjectModel;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class PreferencesViewModel : ViewModelBase
    {
        private ISettingsViewModel _selectedViewModel;

        public LibrariesSettingsViewModel LibrariesSettingsViewModel { get; }
        public AdvancedSettingsViewModel AdvancedSettingsViewModel { get; }

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
    }
}