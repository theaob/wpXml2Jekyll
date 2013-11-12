using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace wpXml2Jekyll
{
    public partial class UIForm : Form
    {
        LinkedList<String> lines = new LinkedList<string>();

        public UIForm()
        {
            InitializeComponent();
        }

        public struct Post
        {
            public String title;
            public DateTime date;
            public String content;
            public String url;

            public Post(String postTitle, String postDate, String postContent, String postURL)
            {
                title = postTitle;
                date = DateTime.Parse(postDate);
                content = postContent;
                url = postURL;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lines.Clear();
            listBox1.Items.Clear();

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wordpress Output (.xml)|*.xml";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName.Length < 2)
            {
                return;
            }

            lines = ReadPosts(openFileDialog1.FileName, add2List);
        }

        public LinkedList<string> ReadPosts(string fileName, Action<string> reporter)
        {
            using (TextReader tr = new StreamReader(fileName, Encoding.UTF8))
            {
                var exportFileContent = tr.ReadToEnd();

                return ParseExportFileContent(exportFileContent, reporter);
            }
        }


        private LinkedList<string> ParseExportFileContent(string exportFileContent, Action<string> reporter)
        {
            using (StringReader stringReader = new StringReader(exportFileContent))
            {
                var parsedLines = new LinkedList<string>();
                string line;
                bool title = false;

                while ((line = stringReader.ReadLine()) != null)
                {
                    if (line.Contains("<image>"))
                    {
                        while (true)
                        {
                            line += "\r\n" + stringReader.ReadLine();
                            if (line.Contains("</image>"))
                            {
                                break;
                            }
                        }
                    }
                    else if (line.Contains("<title>"))
                    {

                        if (title)
                        {
                            line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                            parsedLines.AddLast(line);
                            reporter(line);
                        }
                        else
                        {
                            title = true;
                        }
                    }
                    else if (line.Contains("<wp:post_date>"))
                    {
                        line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                        int year = int.Parse(line.Substring(0, 4));
                        int month = int.Parse(line.Substring(5, 2));
                        int day = int.Parse(line.Substring(8, 2));
                        DateTime date = new DateTime(year, month, day);

                        parsedLines.AddLast(line.ToString());
                        reporter(date.ToShortDateString());
                    }
                    else if (line.Contains("<content:encoded>") && line.Contains("</content:encoded>"))
                    {
                        line = line.Substring(28);
                        line = line.Substring(0, line.Length - 21);
                        parsedLines.AddLast(line);
                        reporter(line);
                    }
                    else if (line.Contains("<content:encoded>"))
                    {
                        while (true)
                        {
                            line += "\r\n" + stringReader.ReadLine();
                            if (line.Contains("</content:encoded>"))
                            {
                                break;
                            }
                        }
                        line = line.Substring(28);
                        line = line.Substring(0, line.Length - 21);
                        parsedLines.AddLast(line);
                        reporter(line);
                    }
                    else if (line.Contains("<wp:post_name>"))
                    {
                        line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                        parsedLines.AddLast(line);
                        reporter(line);
                    }
                    else if (line.Contains("<wp:status>draft</wp:status>"))
                    {
                        line = "draft";
                        parsedLines.AddLast(line);
                        reporter(line);
                    }
                }
                return parsedLines;
            }
        }


        private void add2List(String addThis)
        {
            listBox1.Items.Add(addThis);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (folderBrowserDialog1.SelectedPath.Length < 2)
            {
                return;
            }

            WritePostToMarkdown(lines, folderBrowserDialog1.SelectedPath);
        }

        public void WritePostToMarkdown(LinkedList<string> linesToWrite, string outputFolder)
        {
            LinkedList<Post> posts = new LinkedList<Post>();

            for (int i = 0; i < linesToWrite.Count; i += 4)
            {
                try
                {
                    Post newPost = new Post();

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


            foreach (Post p in posts)
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