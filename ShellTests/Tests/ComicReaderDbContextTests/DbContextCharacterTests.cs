﻿using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.Database;
using NCrunch.Framework;
using NUnit.Framework;
using ShellTests.Classes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    public class DbContextCharacterTests
    {
        private BlockingCollection<PathDb> _dbHelperCollection = new BlockingCollection<PathDb>();

        [SetUp]
        public void Setup()
        {
            string dbFile = @$".\test_{Guid.NewGuid()}.sdf";
            _dbHelperCollection.Add(new PathDb(dbFile, new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>())));
        }

        public void Cleanup(PathDb dbHelper)
        {
            dbHelper.Db.Dispose();
            File.Delete(dbHelper.Path);
        }

        [Test]
        public async Task AddCharacterTest()
        {
            var dbHelper = _dbHelperCollection.Take();

            dbHelper.Db.Books.Add(new Book() { Name = "My Book", Path = @"c:\myFile.txt" });
            dbHelper.Db.Characters.Add(new Character() { BookId = 0, Name = "George Washington" });

            await dbHelper.Db.SaveChangesAsync();

            Cleanup(dbHelper);
        }

        [Test]
        public async Task AddSameCharacterTwoBooksTest()
        {
            var dbHelper = _dbHelperCollection.Take();

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
            var dbHelper = _dbHelperCollection.Take();

            dbHelper.Db.Books.Add(new Book() { Name = "John Smith" });

            Assert.ThrowsAsync<DbEntityValidationException>(async () =>
            {
                await dbHelper.Db.SaveChangesAsync();
            });

            Cleanup(dbHelper);
        }
    }
}