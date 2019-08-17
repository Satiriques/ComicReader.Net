using ComicReader.Net.UI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Comic.Net.ViewModels
{
    public class BookViewModel
    {
        public BookViewModel(Book book)
        {
            Id = book.Id;
            Name = book.Name;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
    }
}
