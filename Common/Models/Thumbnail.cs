using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Thumbnail
    {
        public int ThumbnailId { get; set; }

        [Required]
        public string Path { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}