using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ComicReader.Net.Shell.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Character> Characters { get; set; }
        public List<Thumbnail> Thumbnails { get; set; }
        public List<ThumbnailCache> ThumbnailCaches { get; set; }
    }
}
