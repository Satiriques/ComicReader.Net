using System.Collections.Generic;
using ComicReader.Net.UI.Model;

namespace ComicReader.Net.UI.Interfaces
{
    public interface IComicService
    {
        IEnumerable<Book> GetAll();
        void Initialize();
    }
}