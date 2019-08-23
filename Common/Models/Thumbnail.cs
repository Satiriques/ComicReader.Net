namespace ComicReader.Net.Common.Models
{
    public class Thumbnail
    {
        public int ThumbnailId { get; set; }
        public string Path { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}