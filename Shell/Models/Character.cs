using System.ComponentModel.DataAnnotations;

namespace ComicReader.Net.Shell.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
