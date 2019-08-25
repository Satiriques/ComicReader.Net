using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class ThumbnailCacheService : IThumbnailCacheService
    {
        private readonly IZipService _zipService;
        private readonly string _cacheFolder;

        public ThumbnailCacheService(IZipService zipService)
        {
            _zipService = zipService;
            _cacheFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                "ComicReader.Net",
                "cache");
            if (!Directory.Exists(_cacheFolder))
            {
                Directory.CreateDirectory(_cacheFolder);
            }
        }

        private async Task CacheBookAsync(Book book, string folder, bool overwrite)
        {
            await Task.Run(() =>
            {
                string currentFolder = Path.Combine(folder, book.Id.ToString());
                Directory.CreateDirectory(currentFolder);
                _zipService.ExtractBook(book, currentFolder);
                var files = Directory.GetFiles(currentFolder);

                var cacheFilePath = Path.Combine(_cacheFolder, book.Id + ".cache");
                if (File.Exists(cacheFilePath))
                {
                    if (overwrite)
                    {
                        File.Delete(cacheFilePath);
                        File.Move(files[0], cacheFilePath);
                    }
                }
                else
                {
                    File.Move(files[0], cacheFilePath);
                }
                Directory.Delete(currentFolder, true);
            }).ConfigureAwait(false);
        }

        public async Task CacheBooksAsync(IEnumerable<Book> books, int size)
        {
            var processId = Process.GetCurrentProcess().Id;
            var solutionName = "ComicReader.Net";
            var folderName = Path.Combine(Path.GetTempPath(), solutionName + "_" + processId);

            var tasks = new List<Task>();

            // TODO: Do something incase we can't delete the directory
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName, true);
            }
            Directory.CreateDirectory(folderName);

            await Task.WhenAll(books.Select(b => CacheBookAsync(b, folderName, true))).ConfigureAwait(false);
        }
    }
}