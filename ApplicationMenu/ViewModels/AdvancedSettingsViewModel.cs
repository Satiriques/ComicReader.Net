using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class AdvancedSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        public string Title => "Advanced";
    }
}