using ComicReader.Net.Common.Models;

namespace ComicReader.Net.Shell.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
        }

        public string Name { get; }
        public int Id { get; }
    }
}