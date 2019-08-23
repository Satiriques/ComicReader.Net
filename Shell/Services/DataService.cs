using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class DataService : IDataService
    {
        private readonly Func<ComicReaderDbContext> _dbContext;

        public DataService(Func<ComicReaderDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            using (var db = _dbContext())
            {
                return await db.Books.AsNoTracking().ToListAsync();
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
    }
}