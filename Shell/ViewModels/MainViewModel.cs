using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.CenterGrid.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IFileMenuViewModel fileMenuViewModel,
                             ICenterGridViewModel centerGridViewModel)
        {
            FileMenuViewModel = fileMenuViewModel;
            CenterGridViewModel = centerGridViewModel;
        }

        public IFileMenuViewModel FileMenuViewModel { get; }
        public ICenterGridViewModel CenterGridViewModel { get; }
    }
}