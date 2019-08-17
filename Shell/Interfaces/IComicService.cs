using System.Collections.Generic;
using ComicReader.Net.Shell.Models;

namespace ComicReader.Net.Shell.Interfaces
{
    public interface IComicService
    {
        IEnumerable<Book> GetAll();
        void Initialize();
    }
}