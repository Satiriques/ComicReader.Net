using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Common.Interfaces
{
    public interface IUserConfig
    {
        List<string> Libraries { get; set; }

        void Save();
    }
}