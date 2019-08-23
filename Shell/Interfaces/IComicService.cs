using ComicReader.Net.Common.Models;
using System.Collections.Generic;

namespace ComicReader.Net.Shell.Interfaces
{
    public interface IComicService
    {
        IEnumerable<Book> GetAll();

        void Initialize();
    }
}