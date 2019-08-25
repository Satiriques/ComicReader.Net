using ComicReader.Net.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicReader.Net.Common.Interfaces
{
    public interface IZipService
    {
        Task ExtractBookByIdAsync(IEnumerable<Book> books);

        void ExtractBook(Book book, string folder);
    }
}