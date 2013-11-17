using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace wpXml2Jekyll
{
    public class PostWriter
    {
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