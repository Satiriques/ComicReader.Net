using ComicReader.Net.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComicReader.Net.Common.Interfaces
{
    public interface IDataService
    {
        Task<List<Book>> GetAllBooksAsync();

        Task AddBooksAsync(IEnumerable<string> files);
    }
}