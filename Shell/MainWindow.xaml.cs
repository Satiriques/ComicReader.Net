using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace ComicReader.Net.Shell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataService _dataService;

        public MainWindow(MainViewModel mainViewModel, IDataService dataService)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            _dataService = dataService;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var books = await _dataService.GetAllBooksAsync();
        }
    }
}