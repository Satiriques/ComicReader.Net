using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.ApplicationMenu.ViewModels
{
    public class GridSettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private readonly IThumbnailCacheService _thumbnailCacheService;
        private readonly IDataService _dataService;

        public string Title => "Gallery";

        public DelegateCommand GenerateCacheCommand { get; }

        public GridSettingsViewModel(IThumbnailCacheService thumbnailCacheService,
                                     IDataService dataService)
        {
            GenerateCacheCommand = new DelegateCommand(GenerateCacheExecute);
            _thumbnailCacheService = thumbnailCacheService;
            _dataService = dataService;
        }

        private async void GenerateCacheExecute()
        {
            var books = await _dataService.GetAllBooksAsync();
            await _thumbnailCacheService.CacheBooksAsync(books, 128);
        }
    }
}