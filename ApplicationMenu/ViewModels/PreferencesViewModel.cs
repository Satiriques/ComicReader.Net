using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Events;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class PreferencesViewModel : ViewModelBase
    {
        private ISettingsViewModel _selectedViewModel;
        private readonly IEventAggregator _eventAggregator;

        public PreferencesViewModel(LibrariesSettingsViewModel librariesSettingsViewModel,
                                    AdvancedSettingsViewModel advancedSettingsViewModel,
                                    IEventAggregator eventAggregator,
                                    IUserConfig userConfig)
        {
            LibrariesSettingsViewModel = librariesSettingsViewModel;
            AdvancedSettingsViewModel = advancedSettingsViewModel;
            _eventAggregator = eventAggregator;

            MenuViewModels = new ObservableCollection<ISettingsViewModel>
            {
                librariesSettingsViewModel,
                advancedSettingsViewModel
            };
            OnPropertyChanged(nameof(MenuViewModels));

            OKCommand = new DelegateCommand<ICloseable>(OnOKCommand);
            CancelCommand = new DelegateCommand<ICloseable>(OnCancelCommand);
            _userConfig = userConfig;
        }

        private void OnCancelCommand(ICloseable window)
        {
            window?.Close();
        }

        private void OnOKCommand(ICloseable window)
        {
            LibrariesSettingsViewModel.SaveConfig();
            window?.Close();
        }

        public AdvancedSettingsViewModel AdvancedSettingsViewModel { get; }

        public LibrariesSettingsViewModel LibrariesSettingsViewModel { get; }

        public ObservableCollection<ISettingsViewModel> MenuViewModels { get; set; }

        public DelegateCommand<ICloseable> OKCommand { get; }
        public DelegateCommand<ICloseable> CancelCommand { get; }

        private IUserConfig _userConfig;

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