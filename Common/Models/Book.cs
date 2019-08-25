using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicReader.Net.Common.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [MaxLength(260)]
        public string Path { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Character> Characters { get; set; }

        public List<Thumbnail> Thumbnails { get; set; }

        //public List<ThumbnailCache> ThumbnailCaches { get; set; }
    }
}