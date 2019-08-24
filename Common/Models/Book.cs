using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public string Path { get; set; }

        [ForeignKey("TagId")]
        public List<Tag> Tags { get; set; }

        [ForeignKey("CharacterId")]
        public List<Character> Characters { get; set; }

        [ForeignKey("ThumbnailId")]
        public List<Thumbnail> Thumbnails { get; set; }

        //public List<ThumbnailCache> ThumbnailCaches { get; set; }
    }
}