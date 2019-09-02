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

        [Test]
        public async Task AddCharacterTest()
        {
            string dbFile = @$".\test.sdf";
            using (var db = new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>()))
            {
                db.Books.Add(new Book() { Name = "My Book", Path = @"c:\myFile.txt" });
                db.Characters.Add(new Character() { BookId = 0, Name = "George Washington" });

                await db.SaveChangesAsync();
            }
        }

        [Test]
        public async Task AddSameCharacterTwoBooksTest()
        {
            var books = new Book[] { new Book() { Name = "My Book", Path = @"c:\myFile.txt" },
                                     new Book() { Name = "My Book Again", Path = @"c:\myFile2.txt"}};

            var characters = new Character[] { new Character() { Book = books[0], Name = "George Washington" } ,
                             new Character() { Book = books[1], Name = "George Washington" }};

            string dbFile = @$".\test.sdf";
            using (var db = new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>()))
            {
                db.Books.AddRange(books);
                db.Characters.AddRange(characters);

                await db.SaveChangesAsync();
            }
        }

        [Test]
        public void InvalidAddCharacterMissingPathTest()
        {
            string dbFile = @$".\test.sdf";
            using (var db = new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>()))
            {
                db.Books.Add(new Book() { Name = "John Smith" });

                Assert.ThrowsAsync<DbEntityValidationException>(async () =>
                {
                    await db.SaveChangesAsync();
                });
            }
        }
    }
}