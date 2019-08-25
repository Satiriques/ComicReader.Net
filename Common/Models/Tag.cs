using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}