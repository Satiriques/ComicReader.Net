using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Database;
using ComicReader.Net.Shell.Services;
using Moq;
using NUnit.Framework;
using ShellTests.Tests.ComicReaderDbContextTests;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShellTests.Tests
{
    public class DataServiceTests : DbContextTestBase
    {
        //[Test]
        public async Task AddBookTest()
        {
            var dbHelper = DbHelperCollection.Take();
            var db = dbHelper.Db;
            var dataService = new DataService(new Func<ComicReaderDbContext>(() => db), new FileService(), new ZipService(), new ParserService());

            var path = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "Data", "TestBook.zip");

            Assert.IsTrue(File.Exists(path));
            await dataService.AddBooksAsync(new string[] { path });

            var books = await dataService.GetAllBooksAsync();

            Cleanup(dbHelper);
        }

        //[Test]
        public async Task GetAllBooksAsync()
        {
            //var books = await _dataService.GetAllBooksAsync();
            //CollectionAssert.IsEmpty(books);

            //await _dataService.AddBooksAsync(new string[] { @"c:\randomFile.txt", @"c:\randomFile2.txt" });

            //books = await _dataService.GetAllBooksAsync();
            //CollectionAssert.AllItemsAreNotNull(books);
            //Assert.AreEqual(2, books.Count);
        }

        [Test]
        public async Task AddCharacterAsync()
        {
        }
    }
}