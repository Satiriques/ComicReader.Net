using ComicReader.Net.Common.Models;
using System.Collections.Generic;

namespace ComicReader.Net.Shell.Interfaces
{
    public interface IThumbnailCacheService
    {
        IEnumerable<Book> GetAll();

        void Initialize();
    }
}