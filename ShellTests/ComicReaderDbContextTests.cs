using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Database;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShellTests
{
    public class ComicReaderDbContextTests
    {
        private ComicReaderDbContext _db;

        [SetUp]
        public void SetDataService()
        {
            string dbFile = @$".\test_{Thread.CurrentThread.ManagedThreadId}.sdf";
            _db = new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>());
        }

        [Test]
        public async Task AddCharacterTest()
        {
            _db.Books.Add(new Book() { Name = "My Book", Path = @"c:\myFile.txt" });
            _db.Characters.Add(new Character() { BookId = 0, Name = "George Washington" });

            await _db.SaveChangesAsync();
        }

        [Test]
        public async Task InvalidAddCharacterMissingNameTest()
        {
            _db.Books.Add(new Book() { Path = @"c:\myFile.txt" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await _db.SaveChangesAsync();
            });
        }

        [Test]
        public async Task InvalidAddCharacterMissingPathTest()
        {
            _db.Books.Add(new Book() { Name = @"John Smith" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await _db.SaveChangesAsync();
            });
        }
    }
}