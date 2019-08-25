using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Common.Interfaces
{
    public interface IImageService
    {
        void ResizeImage(string imagePath, string newImagePath,
                             /* note changed names */
                             int canvasWidth, int canvasHeight);
    }
}