using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using NUnit.Framework;
using wpXml2Jekyll;

namespace wpXml2JekyllTests
{
    [TestFixture]
    public class TestPostImporter
    {
        private readonly string _xmlPath = @"..\..\exporttest.wordpress.2013-11-17.xml";
        private readonly string _falseDirectoryPath = @".\findthis\ifyou\can.xml";
        private readonly string _falseFilePath = @".\findthisifyoucan.xml";
        private PostImporter postImporter;
        private XmlDocument realDocument;

        [SetUp]
        public void SetUpObjects()
        {
            postImporter = new PostImporter();
            realDocument = new XmlDocument();
            realDocument.Load(_xmlPath);
        }

        [Test]
        public void TestPostImporting()
        {

            XmlDocument importedDocument = postImporter.ReadWpPosts(_xmlPath);
            Assert.AreEqual(realDocument, importedDocument);

        }

        [Test]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void PostImportDirectoryNotFoundException()
        {
            XmlDocument falsePathDocument = postImporter.ReadWpPosts(_falseDirectoryPath);
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void PostImportFileNotFoundException()
        {
            XmlDocument falsePathDocument = postImporter.ReadWpPosts(_falseFilePath);
        }
    }
}
