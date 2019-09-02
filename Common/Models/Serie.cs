using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicReader.Net.Common.Models
{
    public class Serie
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SerieId { get; set; }

        [Index("SerieUnique", 1, IsUnique = true)]
        public string Name { get; set; }

        [Index("SerieUnique", 2, IsUnique = true)]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}