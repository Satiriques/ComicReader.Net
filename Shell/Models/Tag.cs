using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.UI.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
