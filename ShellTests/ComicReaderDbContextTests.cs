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
        public async Task AddSameCharacterTwoBooksTest()
        {
            var books = new Book[] { new Book() { Name = "My Book", Path = @"c:\myFile.txt" },
                                     new Book() { Name = "My Book Again", Path = @"c:\myFile2.txt"}};

            var characters = new Character[] { new Character() { Book = books[0], Name = "George Washington" } ,
                             new Character() { Book = books[1], Name = "George Washington" }};

            _db.Books.AddRange(books);
            _db.Characters.AddRange(characters);

            await _db.SaveChangesAsync();
        }

        //[Test]
        public void InvalidAddCharacterMissingNameTest()
        {
            _db.Books.Add(new Book() { Path = @"c:\myFile.txt" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await _db.SaveChangesAsync();
            });
        }

        [Test]
        public void InvalidAddCharacterMissingPathTest()
        {
            _db.Books.Add(new Book() { Name = "John Smith" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await _db.SaveChangesAsync();
            });
        }
    }
}