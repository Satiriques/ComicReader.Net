using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComicReader.Net.Shell.Services
{
    public class ThumbnailCacheService : IThumbnailCacheService
    {
        private readonly IZipService _zipService;

        public ThumbnailCacheService(IZipService zipService)
        {
            _zipService = zipService;
        }

        public void CacheBooksAsync()
        {
        }
    }
}