using ComicReader.Net.CenterGrid.Interfaces;
using Prism.Events;
using ComicReader.Net.Common.Events;
using System;
using System.Collections.ObjectModel;
using ComicReader.Net.Common.Models;
using ComicReader.Net.Common.Interfaces;
using System.Linq;
using ComicReader.Common;
using System.Collections.Generic;

namespace ComicReader.Net.CenterGrid.ViewModels
{
    public class CenterGridViewModel : ViewModelBase, ICenterGridViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IDataService _dataService;
        private readonly IThumbnailCacheService _thumbnailCacheService;
        private readonly IFileService _fileService;

        public CenterGridViewModel(IEventAggregator eventAggregator,
                                   IDataService dataService,
                                   IThumbnailCacheService thumbnailCacheService,
                                   IFileService fileService)
        {
            _eventAggregator = eventAggregator;
            _dataService = dataService;
            _thumbnailCacheService = thumbnailCacheService;
            _fileService = fileService;
            _eventAggregator.GetEvent<PostCachesUpdatedEvent>().Subscribe(UpdateThumbnails);
            UpdateThumbnails();
        }

        public ObservableCollection<BookViewModel> Books { get; set; }

        private string _searchText;

        public string SearchText
        {
            get => _searchText; set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    Books = new ObservableCollection<BookViewModel>(
                        _bookViewModels.Where(x => x.Name.Contains(_searchText)));
                    OnPropertyChanged(nameof(Books));
                    OnPropertyChanged();
                }
            }
        }

        private IEnumerable<BookViewModel> _bookViewModels;

        private async void UpdateThumbnails()
        {
            var books = await _dataService.GetAllBooksAsync();
            _bookViewModels = books.Select(x => new BookViewModel(x, _thumbnailCacheService, _fileService));

            Books = new ObservableCollection<BookViewModel>(_bookViewModels);
            OnPropertyChanged(nameof(Books));
        }
    }
}