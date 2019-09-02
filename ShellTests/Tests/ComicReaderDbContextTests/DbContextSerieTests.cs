using ComicReader.Net.Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    public class DbContextSerieTests : DbContextTestBase
    {
        [Test]
        public async Task AddSerieTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book() { Path = @"c:\test.txt" };
            var book2 = new Book() { Path = @"c:\test2.txt" };

            List<Serie> series = new List<Serie>()
            {
                new Serie() { Name = "My serie", Book = book },
                new Serie() { Name = "My serie2", Book = book },
                new Serie() { Name = "My serie", Book = book2 }
            };

            db.Books.Add(book);
            db.Books.Add(book2);
            db.Series.AddRange(series);

            await dbHelper.Db.SaveChangesAsync();

            Cleanup(dbHelper);
        }

        [Test]
        public void AddSerieDuplicateTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book() { Path = @"c:\test.txt" };

            List<Serie> series = new List<Serie>()
            {
                new Serie() { Name = "My serie", Book = book },
                new Serie() { Name = "My serie", Book = book }
            };

            db.Books.Add(book);
            db.Series.AddRange(series);

            Assert.ThrowsAsync<DbUpdateException>(async () => await dbHelper.Db.SaveChangesAsync());

            Cleanup(dbHelper);
        }

        [Test]
        public void AddSerieWithoutBookTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var serie = new Serie() { Name = "My serie" };

            db.Series.Add(serie);

            Assert.ThrowsAsync<DbUpdateException>(async () => await dbHelper.Db.SaveChangesAsync());

            Cleanup(dbHelper);
        }
    }
}