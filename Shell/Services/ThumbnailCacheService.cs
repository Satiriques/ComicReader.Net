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
        }

        private async Task CacheBookAsync(Book book, string folder)
        {
            await Task.Run(() =>
            {
                string currentFolder = Path.Combine(folder, book.Id.ToString());
                _zipService.ExtractBook(book, folder);
                var files = Directory.GetFiles(folder);
                File.Move(files[0], Path.Combine(_cacheFolder, book.Id + ".cache"));
                Directory.Delete(currentFolder);
            }).ConfigureAwait(false);
        }

        public async Task CacheBooksAsync(IEnumerable<Book> books)
        {
            var processId = Process.GetCurrentProcess().Id;
            var solutionName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
            var folderName = solutionName + "_" + processId;

            var tasks = new List<Task>();

            // TODO: Do something incase we can't delete the directory
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName, true);
            }
            Directory.CreateDirectory(folderName);

            await Task.WhenAll(books.Select(b => CacheBookAsync(b, folderName))).ConfigureAwait(false);
        }
    }
}