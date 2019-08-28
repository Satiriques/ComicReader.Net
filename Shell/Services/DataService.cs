﻿using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class DataService : IDataService
    {
        private readonly Func<ComicReaderDbContext> _dbContext;
        private readonly IFileService _fileService;
        private readonly string _cacheFolder;

        public DataService(Func<ComicReaderDbContext> dbContext,
                           IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _cacheFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData),
                "ComicReader.Net",
                "cache");
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
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

        public async Task AddBooksAsync(IEnumerable<string> files)
        {
            using (var db = _dbContext())
            {
                foreach (var file in files)
                {
                    if (!await db.Books.AnyAsync(x => x.Path == file))
                    {
                        db.Books.Add(new Book() { Path = file });
                    }
                }
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateCachesAsync()
        {
            var thumbnails = _fileService.GetAllThumbnails().ToArray();

            if (thumbnails.Length > 0)
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
                            Console.WriteLine($"adding thumbnail with key {bookId}");
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