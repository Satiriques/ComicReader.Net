namespace ComicReader.Net.Common.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
    }
}