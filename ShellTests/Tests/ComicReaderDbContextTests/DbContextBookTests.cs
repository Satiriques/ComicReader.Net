using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicReader.Net.Common.Models;
using System.Data.Entity.Validation;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    public class DbContextBookTests : DbContextTestBase
    {
        [Test]
        public async Task AddBookTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book() { Path = @"c:\testFile.txt" };
            db.Books.Add(book);
            await db.SaveChangesAsync();

            var books = db.Books.ToList();
            Assert.IsNotNull(books);
            CollectionAssert.IsNotEmpty(books);
            Assert.AreEqual(book.Path, books[0].Path);

            Cleanup(dbHelper);
        }

        [Test]
        public void AddInvalidBookTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;

            var book = new Book();
            db.Books.Add(book);
            Assert.Throws<DbEntityValidationException>(() => db.SaveChangesAsync());

            Cleanup(dbHelper);
        }
    }
}