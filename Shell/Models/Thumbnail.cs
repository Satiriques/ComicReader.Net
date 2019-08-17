using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace ComicReader.Net.Shell.Models
{
    public class Thumbnail
    {
        public int ThumbnailId { get; set; }
        public string Path { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
