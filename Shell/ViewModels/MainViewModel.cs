using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IFileMenuViewModel fileMenuViewModel)
        {
            FileMenuViewModel = fileMenuViewModel;
        }

        public IFileMenuViewModel FileMenuViewModel { get; }
    }
}
