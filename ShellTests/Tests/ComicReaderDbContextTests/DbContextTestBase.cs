using ComicReader.Net.Shell.Database;
using NUnit.Framework;
using ShellTests.Classes;
using System;
using System.Collections.Concurrent;
using System.Data.Entity;
using System.IO;

namespace ShellTests.Tests.ComicReaderDbContextTests
{
    public class DbContextTestBase
    {
        protected BlockingCollection<PathDb> DbHelperCollection = new BlockingCollection<PathDb>();

        [SetUp]
        public void Setup()
        {
            string dbFile = @$".\test_{Guid.NewGuid()}.sdf";
            DbHelperCollection.Add(new PathDb(dbFile, new ComicReaderDbContext($"Data Source={dbFile}",
                new DropCreateDatabaseAlways<ComicReaderDbContext>())));
        }

        public void Cleanup(PathDb dbHelper)
        {
            dbHelper.Db.Dispose();
            File.Delete(dbHelper.Path);
        }
    }
}