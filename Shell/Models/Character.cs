using System.ComponentModel.DataAnnotations;

namespace ComicReader.Net.UI.Model
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}
