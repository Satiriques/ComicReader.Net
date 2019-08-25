using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}