using ComicReader.Net.Common.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    public class DbContextCharacterTests : DbContextTestBase
    {
        [Test]
        public async Task AddCharacterTest()
        {
            var dbHelper = DbHelperCollection.Take();

            dbHelper.Db.Books.Add(new Book() { Name = "My Book", Path = @"c:\myFile.txt" });
            dbHelper.Db.Characters.Add(new Character() { BookId = 0, Name = "George Washington" });

            await dbHelper.Db.SaveChangesAsync();

            Cleanup(dbHelper);
        }

        [Test]
        public async Task AddSameCharacterTwoBooksTest()
        {
            var dbHelper = DbHelperCollection.Take();

            var books = new Book[] { new Book() { Name = "My Book", Path = @"c:\myFile.txt" },
                                     new Book() { Name = "My Book Again", Path = @"c:\myFile2.txt"}};

            var characters = new Character[] { new Character() { Book = books[0], Name = "George Washington" } ,
                             new Character() { Book = books[1], Name = "George Washington" }};

            dbHelper.Db.Books.AddRange(books);
            dbHelper.Db.Characters.AddRange(characters);

            await dbHelper.Db.SaveChangesAsync();

            Cleanup(dbHelper);
        }

        [Test]
        public void InvalidAddCharacterMissingPathTest()
        {
            var dbHelper = DbHelperCollection.Take();

            dbHelper.Db.Books.Add(new Book() { Name = "John Smith" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await dbHelper.Db.SaveChangesAsync();
            });

            Cleanup(dbHelper);
        }

        [Test]
        public void AddCharacterWithDuplicateNameTest()
        {
            var dbHelper = DbHelperCollection.Take();

            var books = new List<Book>
            {
                new Book{ Path = @"c:\test.txt"},
                new Book{Path = @"c:\test2.txt"}
            };

            var characters = new List<Character>
            {
                new Character{ Book = books[0], Name = @"test name1" },
                new Character{ Book= books[0], Name = @"test name1" }
            };

            dbHelper.Db.Books.AddRange(books);
            dbHelper.Db.Characters.AddRange(characters);

            Assert.ThrowsAsync<DbUpdateException>(async () => await dbHelper.Db.SaveChangesAsync());

            Cleanup(dbHelper);
        }
    }
}