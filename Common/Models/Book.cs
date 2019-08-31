using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [MaxLength(260)]
        [Index(IsUnique = true)]
        public string Path { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Character> Characters { get; set; }

        //public List<ThumbnailCache> ThumbnailCaches { get; set; }
    }
}