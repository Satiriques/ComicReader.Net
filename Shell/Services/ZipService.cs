using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ComicReader.Net.Shell.Services
{
    public class ZipService : IZipService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void ExtractZipFile(string archivePath, string password, string outFolder)
        {
            log.Debug($"[{Process.GetCurrentProcess().Id}] unzipping archive: {archivePath} to folder: {outFolder}");

            using (Stream fsInput = File.OpenRead(archivePath))
            using (var zf = new ZipFile(fsInput))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    // AES encrypted entries are handled automatically
                    zf.Password = password;
                }

                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        // Ignore directories
                        continue;
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:
                    //entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here
                    // to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    // Manipulate the output filename here as desired.
                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    // 4K is optimum
                    var buffer = new byte[4096];

                    // Unzip file in buffered chunks. This is just as fast as unpacking
                    // to a buffer the full size of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (var zipStream = zf.GetInputStream(zipEntry))
                    using (Stream fsOutput = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, fsOutput, buffer);
                    }
                }
            }
        }

        public async Task ExtractBookByIdAsync(IEnumerable<Book> books)
        {
            await Task.Run(() =>
            {
                foreach (var book in books)
                {
                    var folder = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                        "ComicReader.Net",
                        book.Id.ToString());
                    Directory.CreateDirectory(folder);

                    ExtractZipFile(book.Path, "", folder);
                }
            }).ConfigureAwait(false);
        }

        public void ExtractBook(Book book, string folder)
        {
            ExtractZipFile(book.Path, "", folder);
        }
    }
}