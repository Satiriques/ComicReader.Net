using ComicReader.Net.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ComicReader.Net.Shell.Services
{
    public class ParserService : IParserService
    {
        public ComicInfo ParseComicRackMetaData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ComicInfo));
            StreamReader reader = new StreamReader(path);
            var config = (ComicInfo)serializer.Deserialize(reader);
            reader.Close();
            return config;
        }
    }
}