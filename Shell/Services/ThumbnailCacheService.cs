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
        private readonly IImageService _imageService;
        private readonly string _cacheFolder;

        public ThumbnailCacheService(IZipService zipService,
                                     IImageService imageService)
        {
            _zipService = zipService;
            _imageService = imageService;
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
                Console.WriteLine($"[{Process.GetCurrentProcess().Id}] current folder: {currentFolder}");
                Directory.CreateDirectory(currentFolder);
                _zipService.ExtractBook(book, currentFolder);
                var files = Directory.GetFiles(currentFolder);

                var cacheFilePath = Path.Combine(_cacheFolder, book.Id + ".cache");
                if (File.Exists(cacheFilePath))
                {
                    if (overwrite)
                    {
                        Console.WriteLine($"[{Process.GetCurrentProcess().Id}] deleting file: {cacheFilePath}");
                        File.Delete(cacheFilePath);
                        Console.WriteLine($"[{Process.GetCurrentProcess().Id}] resizing file: {files[0]} and moving to {cacheFilePath}");
                        _imageService.ResizeImage(files[0], cacheFilePath, 256, 256);
                        //File.Move(files[0], cacheFilePath);
                    }
                }
                else
                {
                    Console.WriteLine($"[{Process.GetCurrentProcess().Id}] resizing file: {files[0]} and moving to {cacheFilePath}");
                    _imageService.ResizeImage(files[0], cacheFilePath, 256, 256);
                    //File.Move(files[0], cacheFilePath);
                }

                Console.WriteLine($"[{Process.GetCurrentProcess().Id}] deleting folder: {currentFolder}");
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