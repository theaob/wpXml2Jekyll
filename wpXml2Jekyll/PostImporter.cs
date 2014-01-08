using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace wpXml2Jekyll
{
    public class PostImporter
    {
        public XmlDocument ReadWpPosts(string fileName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            return xmlDocument;
        }
    }
}