using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Database;
using ComicReader.Net.Shell.ViewModels;
using System;
using System.Linq;
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

        public MainWindow(MainViewModel mainViewModel, IDataService dataService, Func<ComicReaderDbContext> func)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            _dataService = dataService;
        }
    }
}