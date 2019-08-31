using ComicReader.Net.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class FileService : IFileService
    {
        private readonly string _cacheFolder;

        public FileService()
        {
            _cacheFolder = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.ApplicationData),
               "ComicReader.Net",
               "cache");
        }

        public IEnumerable<string> GetAllThumbnails()
        {
            return Directory.GetFiles(_cacheFolder);
        }

        public IEnumerable<string> GetThumbnailsFromBookIds(IEnumerable<int> ids)
        {
            return GetAllThumbnails().Where(x => ids.Select(y => y.ToString()).Contains(Path.GetFileNameWithoutExtension(x)));
        }

        public void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                Process process = new Process();
                process.StartInfo.FileName = path;
                process.Start();
            }
        }
    }
}