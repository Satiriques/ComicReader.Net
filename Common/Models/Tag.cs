namespace ComicReader.Net.Common.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}