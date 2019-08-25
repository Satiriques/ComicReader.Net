using ComicReader.Net.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicReader.Net.Common.Interfaces
{
    public interface IThumbnailCacheService
    {
        Task CacheBooksAsync(IEnumerable<Book> books, int size);
    }
}