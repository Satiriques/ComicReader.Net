using System.Collections.Generic;

namespace ComicReader.Net.Common.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Character> Characters { get; set; }
        public List<Thumbnail> Thumbnails { get; set; }
        //public List<ThumbnailCache> ThumbnailCaches { get; set; }
    }
}