using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Database;
using ComicReader.Net.Shell.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShellTests
{
    public class DataServiceTests
    {
        private DataService _dataService;

        [SetUp]
        public void SetDataService()
        {
            string dbFile = @$".\test_{Thread.CurrentThread.ManagedThreadId}.sdf";
            var func = new Func<ComicReaderDbContext>(() =>
            new ComicReaderDbContext($"Data Source={dbFile}", new DropCreateDatabaseAlways<ComicReaderDbContext>()));
            _dataService = new DataService(func, new Mock<IFileService>().Object);
        }

        [Test]
        public async Task AddBookTest()
        {
            await _dataService.AddBooksAsync(new string[] { @"c:\randomFile.txt" });
            Console.WriteLine("AddBookTest");
        }

        [Test]
        public async Task GetAllBooksAsync()
        {
            var books = await _dataService.GetAllBooksAsync();
            CollectionAssert.IsEmpty(books);

            await _dataService.AddBooksAsync(new string[] { @"c:\randomFile.txt", @"c:\randomFile2.txt" });

            books = await _dataService.GetAllBooksAsync();
            CollectionAssert.AllItemsAreNotNull(books);
            Assert.AreEqual(2, books.Count);
        }
    }
}