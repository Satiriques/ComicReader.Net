using ComicReader.Net.Common.Models;
using System.Data.Entity;

namespace ComicReader.Net.Shell.Database
{
    public class ComicReaderDbContext : DbContext
    {
        public ComicReaderDbContext() : base("ComicReaderDb")
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }
    }
}