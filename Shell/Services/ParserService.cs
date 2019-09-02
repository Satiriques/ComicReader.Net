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
            ComicInfo config = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ComicInfo));
                StreamReader reader = new StreamReader(path);
                config = (ComicInfo)serializer.Deserialize(reader);
                reader.Close();
                return config;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
            }
        }
    }
}