using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace wpXml2Jekyll
{
    public class PostWriter
    {
        public void WritePostToMarkdown(XmlDocument xmlDocumentToWrite, string outputFolder)
        {
            var items = xmlDocumentToWrite.SelectNodes("//item");

            var namespaceManager = new XmlNamespaceManager(xmlDocumentToWrite.NameTable);
            namespaceManager.AddNamespace("wp", "http://wordpress.org/export/1.2/");
            namespaceManager.AddNamespace("content", "http://purl.org/rss/1.0/modules/content/");
            {
                foreach (XmlNode item in items)
                {
                    var p = new UIForm.Post(item.SelectSingleNode("title").InnerText, item.SelectSingleNode("wp:post_date", namespaceManager).InnerText, item.SelectSingleNode("content:encoded", namespaceManager).InnerText,
                         item.SelectSingleNode("wp:post_name", namespaceManager).InnerText);
                    
                    using (
                        TextWriter tw =
                            new StreamWriter(outputFolder + Path.DirectorySeparatorChar +
                                             p.date.ToString("yyyy-MM-dd-") + p.url + ".md"))
                    {
                        tw.WriteLine("---");
                        tw.WriteLine("layout: post");
                        tw.WriteLine("title: " + p.title);
                        tw.WriteLine("date: " + p.date.ToString("yyyy-MM-dd HH:mm"));
                        tw.WriteLine("comments: true");
                        tw.WriteLine("categories: []");
                        tw.WriteLine("---");
                        tw.WriteLine(p.content);
                    }
                }
            }

        }

        public void WritePostToMarkdown(LinkedList<string> linesToWrite, string outputFolder)
        {
            LinkedList<UIForm.Post> posts = new LinkedList<UIForm.Post>();

            for (int i = 0; i < linesToWrite.Count; i += 4)
            {
                try
                {
                    UIForm.Post newPost = new UIForm.Post();

                    newPost.title = linesToWrite.ElementAt(i);
                    newPost.content = linesToWrite.ElementAt(i + 1);
                    newPost.date = DateTime.Parse(linesToWrite.ElementAt(i + 2));
                    newPost.url = linesToWrite.ElementAt(i + 3);

                    posts.AddLast(newPost);
                }
                catch(FormatException exp)
                {
                    //Draft post without any content are handled here
                }
            }


            foreach (UIForm.Post p in posts)
            {
                using (
                    TextWriter tw =
                        new StreamWriter(outputFolder + Path.DirectorySeparatorChar +
                                         p.date.ToString("yyyy-MM-dd-") + p.url + ".md"))
                {
                    tw.WriteLine("---");
                    tw.WriteLine("layout: post");
                    tw.WriteLine("title: " + p.title);
                    tw.WriteLine("date: " + p.date.ToString("yyyy-MM-dd HH:mm"));
                    tw.WriteLine("comments: true");
                    tw.WriteLine("categories: []");
                    tw.WriteLine("---");
                    tw.WriteLine(p.content);
                }
            }
        }
    }
}