using ComicReader.Net.Shell.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShellTests
{
    public class ParserServiceTests
    {
        [Test]
        public void ParseXmlTest()
        {
            var path = Path.Combine(Assembly.GetExecutingAssembly().Location, "..", "Data", "ComicInfoTest.xml");
            Debug.Assert(File.Exists(path));
            var service = new ParserService();
            var metadata = service.ParseComicRackMetaData(path);
            Assert.IsNotNull(metadata);
        }
    }
}