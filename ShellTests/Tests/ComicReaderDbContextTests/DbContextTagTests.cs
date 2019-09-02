using ComicReader.Net.Common.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    internal class DbContextTagTests : DbContextTestBase
    {
        [Test]
        public async Task AddTagTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book() { Path = @"c:\test.txt" };
            var book2 = new Book() { Path = @"c:\test2.txt" };

            List<Tag> Tags = new List<Tag>()
            {
                new Tag() { Name = "My Tag", Book = book },
                new Tag() { Name = "My Tag2", Book = book },
                new Tag() { Name = "My Tag", Book = book2 }
            };

            db.Books.Add(book);
            db.Books.Add(book2);
            db.Tags.AddRange(Tags);

            await dbHelper.Db.SaveChangesAsync();

            Cleanup(dbHelper);
        }

        [Test]
        public void AddTagDuplicateTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book() { Path = @"c:\test.txt" };

            List<Tag> Tags = new List<Tag>()
            {
                new Tag() { Name = "My Tag", Book = book },
                new Tag() { Name = "My Tag", Book = book }
            };

            db.Books.Add(book);
            db.Tags.AddRange(Tags);

            Assert.ThrowsAsync<DbUpdateException>(async () => await dbHelper.Db.SaveChangesAsync());

            Cleanup(dbHelper);
        }

        [Test]
        public void AddTagWithoutBookTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var Tag = new Tag() { Name = "My Tag" };

            db.Tags.Add(Tag);

            Assert.ThrowsAsync<DbUpdateException>(async () => await dbHelper.Db.SaveChangesAsync());

            Cleanup(dbHelper);
        }
    }
}