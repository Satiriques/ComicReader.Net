using ComicReader.Common;
using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ComicReader.Net.CenterGrid.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private readonly IThumbnailCacheService _thumbnailCacheService;
        private readonly IFileService _fileService;
        private readonly int _id;
        private readonly string _path;
        private BitmapSource _thumbnail;

        public BookViewModel(Book book,
                             IThumbnailCacheService thumbnailCacheService,
                             IFileService fileService)
        {
            _id = book.Id;
            _path = book.Path;
            Name = Path.GetFileNameWithoutExtension(book.Path);
            _thumbnailCacheService = thumbnailCacheService;
            _fileService = fileService;
        }

        public string Name { get; }

        public void OpenBook()
        {
            _fileService.OpenFile(_path);
        }

        public BitmapSource Thumbnail
        {
            get
            {
                if (_thumbnail == null)
                {
                    Console.WriteLine("loading thumbnail " + _id.ToString());
                    byte[] image = _thumbnailCacheService.GetThumbnailAsync(_id).Result;

                    if (image == null)
                    {
                        return null;
                    }

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(image);
                    bitmapImage.CreateOptions = BitmapCreateOptions.None;
                    bitmapImage.CacheOption = BitmapCacheOption.Default;
                    bitmapImage.EndInit();

                    _thumbnail = bitmapImage;
                }
                return _thumbnail;
            }
        }
    }
}