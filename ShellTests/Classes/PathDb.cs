using ComicReader.Net.Shell.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellTests.Classes
{
    public class PathDb
    {
        public string Path { get; }
        public ComicReaderDbContext Db { get; }

        public PathDb(string path, ComicReaderDbContext db)
        {
            Path = path;
            Db = db;
        }
    }
}