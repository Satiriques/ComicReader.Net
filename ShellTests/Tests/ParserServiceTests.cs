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

namespace ShellTests.Tests
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
            Assert.That(metadata.AgeRating, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Characters, Is.Not.Null.Or.Empty);
            Assert.That(metadata.CoverArtist, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Editor, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Genre, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Inker, Is.Not.Null.Or.Empty);
            Assert.That(metadata.LanguageISO, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Letterer, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Manga, Is.Not.Null.Or.Empty);
            Assert.That(metadata.PageCount, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Penciller, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Publisher, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Summary, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Title, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Translator, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Web, Is.Not.Null.Or.Empty);
            Assert.That(metadata.Writer, Is.Not.Null.Or.Empty);

            Assert.That(metadata.Series, Is.Not.Null.Or.Empty);
            CollectionAssert.IsNotEmpty(metadata.SeriesList);
        }
    }
}