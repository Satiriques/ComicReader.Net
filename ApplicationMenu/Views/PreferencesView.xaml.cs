using ComicReader.Net.ApplicationMenu.ViewModels;
using ComicReader.Net.Common.Interfaces;
using System.Windows;

namespace ComicReader.Net.ApplicationMenu.Views
{
    /// <summary>
    /// Interaction logic for PreferencesView.xaml
    /// </summary>
    public partial class PreferencesView : Window, ICloseable
    {
        public PreferencesView(PreferencesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}