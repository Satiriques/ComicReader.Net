using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComicReader.Net.Shell.Data
{
    public class ThumbnailCacheService : IThumbnailCacheService
    {
        private readonly string _currentDirectory;
        private readonly string _cacheDirectory;
        private readonly string _tmpDirectory;

        public ThumbnailCacheService()
        {
            _currentDirectory = Directory.GetCurrentDirectory();
            _cacheDirectory = Path.Combine(_currentDirectory, "cache");
            _tmpDirectory = Path.Combine(Environment.GetEnvironmentVariable("TMP"), "comicreader");
        }

        public IEnumerable<Book> GetAll()
        {
            Book comic = new Book();
            return null;
        }

        public void Initialize()
        {
            if (!Directory.Exists(_cacheDirectory))
            {
                Directory.CreateDirectory(_cacheDirectory);
            }

            CacheCovers();
        }

        private void CacheCovers()
        {
            foreach (var file in Directory.GetFiles("Files"))
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var cacheFile = Path.Combine(_cacheDirectory, fileName + ".cache");
                if (!File.Exists(cacheFile))
                {
                    ZipService.Zip.ExtractFile(file, _tmpDirectory);

                    var images = Directory.GetFiles(_tmpDirectory).OrderBy(x => x);
                    var image = images.FirstOrDefault(); ;
                    if (image != null)
                    {
                        try
                        {
                            File.Move(image, cacheFile);
                        }
                        catch
                        {
                            Console.WriteLine("skipped " + file);
                        }
                    }

                    foreach (var img in images)
                    {
                        File.Delete(img);
                    }
                }
            }
        }
    }
}