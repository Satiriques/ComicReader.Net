using ComicReader.Common;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Events;
using ComicReader.Net.Common.Interfaces;
using Prism.Commands;
using Prism.Events;
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
        private readonly IEventAggregator _eventAggregator;

        public string Title => "Gallery";

        public DelegateCommand GenerateCacheCommand { get; }

        public GridSettingsViewModel(IThumbnailCacheService thumbnailCacheService,
                                     IDataService dataService,
                                     IEventAggregator eventAggregator)
        {
            GenerateCacheCommand = new DelegateCommand(GenerateCachesExecute);
            _thumbnailCacheService = thumbnailCacheService;
            _dataService = dataService;
            _eventAggregator = eventAggregator;
        }

        private async void GenerateCachesExecute()
        {
            var books = await _dataService.GetAllBooksAsync();
            await _thumbnailCacheService.CacheBooksAsync(books);
            await _dataService.UpdateCachesAsync();
            _eventAggregator.GetEvent<PostCachesUpdatedEvent>().Publish();
        }
    }
}