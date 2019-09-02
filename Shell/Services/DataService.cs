using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class DataService : IDataService
    {
        private readonly Func<ComicReaderDbContext> _dbContext;
        private readonly IFileService _fileService;
        private readonly IZipService _zipService;
        private readonly IParserService _parserService;
        private readonly string _cacheFolder;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataService(Func<ComicReaderDbContext> dbContext,
                           IFileService fileService,
                           IZipService zipService,
                           IParserService parserService
                           )
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _zipService = zipService;
            _parserService = parserService;
            _cacheFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                "ComicReader.Net",
                "cache");
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            log.Info("GetAllBooksAsync");
            using (var db = _dbContext())
            {
                return await db.Books.AsNoTracking().ToListAsync();
            }
        }

        public async Task<Thumbnail> GetThumbnailAsync(int bookId)
        {
            using (var db = _dbContext())
            {
                return await db.Thumbnails.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == bookId);
            }
        }

        public async Task<List<Thumbnail>> GetAllThumbnailsAsync()
        {
            using (var db = _dbContext())
            {
                return await db.Thumbnails.AsNoTracking().ToListAsync();
            }
        }

        public async Task<bool> ThumbnailExistsAsync(int bookId)
        {
            using (var db = _dbContext())
            {
                return await db.Thumbnails.AsNoTracking().AnyAsync(x => x.BookId == bookId);
            }
        }

        /// <summary>
        /// For each file, unzip that file and add the book to the database,
        /// while parsing the metadata if present and adding them to their
        /// table
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task AddBooksAsync(IEnumerable<string> files)
        {
            log.Debug($"[{Thread.CurrentThread.ManagedThreadId}] AddBooksAsync");
            var tmpPath = Path.Combine(Path.GetTempPath(), "ComicReader.Net_" + Process.GetCurrentProcess().Id.ToString());

            using (var db = _dbContext())
            {
                List<Book> booksToAdd = new List<Book>();
                List<Character> charactersToAdd = new List<Character>();
                List<Serie> seriesToAdd = new List<Serie>();
                List<Tag> tagsToAdd = new List<Tag>();

                foreach (var file in files)
                {
                    if (!await db.Books.AnyAsync(x => x.Path == file))
                    {
                        var book = new Book() { Path = file };
                        try
                        {
                            _zipService.ExtractBook(book, tmpPath);
                        }
                        catch (Exception e)
                        {
                            log.Error(e.Message);
                            continue;
                        }

                        if (Directory.GetFiles(tmpPath).Length > 0)
                        {
                            booksToAdd.Add(book);
                            if (File.Exists(Path.Combine(tmpPath, "ComicInfo.xml")))
                            {
                                AddMetadata(tmpPath, charactersToAdd, seriesToAdd, tagsToAdd, file, book);
                            }
                        }
                    }
                }

                db.Books.AddRange(booksToAdd);
                db.Characters.AddRange(charactersToAdd);
                db.Tags.AddRange(tagsToAdd);

                await db.SaveChangesAsync();
            }
        }

        private void AddMetadata(string tmpPath, List<Character> charactersToAdd,
            List<Serie> seriesToAdd, List<Tag> tagsToAdd, string file, Book book)
        {
            log.DebugFormat("Metadata found for file: {0}", Path.GetFileNameWithoutExtension(file));
            var metadata = _parserService.ParseComicRackMetaData(Path.Combine(tmpPath, "ComicInfo.xml"));
            if (metadata?.CharactersList?.Any() == true)
            {
                log.DebugFormat("Adding characters: {0}", string.Join(",", metadata.CharactersList));
                charactersToAdd.AddRange(metadata.CharactersList.Select(x =>
                new Character() { Book = book, Name = x }));
            }
            if (metadata?.SeriesList?.Any() == true)
            {
                log.DebugFormat("Adding series: {0}", string.Join(",", metadata.SeriesList));
                seriesToAdd.AddRange(metadata.SeriesList.Select(x =>
                new Serie() { Book = book, Name = x }));
            }
            if (metadata?.GenreList?.Any() == true)
            {
                log.DebugFormat("Adding tags: {0}", string.Join(",", metadata.GenreList));
                tagsToAdd.AddRange(metadata.GenreList.Select(x =>
                new Tag() { Book = book, Name = x }));
            }
        }

        public async Task UpdateCachesAsync()
        {
            Book[] books = null;
            using (var db = _dbContext())
            {
                books = await db.Books.AsNoTracking().ToArrayAsync();
            }

            if (!(books?.Any() == true))
            {
                return;
            }

            var thumbnails = _fileService.GetThumbnailsFromBookIds(books.Select(x => x.Id));

            if (thumbnails.Any())
            {
                using (var db = _dbContext())
                {
                    foreach (var thumbnailPath in thumbnails)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(thumbnailPath);
                        var bookId = int.Parse(fileName);
                        var entity = await db.Thumbnails.FirstOrDefaultAsync(x => x.BookId == bookId);
                        if (entity != null)
                        {
                            entity.Path = thumbnailPath;
                        }
                        else
                        {
                            log.Debug($"adding thumbnail with key {bookId}");
                            db.Thumbnails.Add(new Thumbnail() { Path = thumbnailPath, BookId = bookId });
                        }
                    }
                    try
                    {
                        await db.SaveChangesAsync();
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}