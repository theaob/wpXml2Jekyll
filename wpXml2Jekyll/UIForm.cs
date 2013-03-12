using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace wpXml2Jekyll
{
    public partial class UIForm : Form
    {
        LinkedList<String> lines = new LinkedList<string>();
        LinkedList<Post> posts = new LinkedList<Post>();

        bool title = false;

        public UIForm()
        {
            InitializeComponent();
        }

        public struct Post
        {
            public String postTitle;
            public DateTime date;
            public String post;
            public String postURL;

            public Post(String title, String then, String content, String postur)
            {
                postTitle = title;
                date = DateTime.Parse(then);
                post = content;
                postURL = postur;
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

            TextReader tr = new StreamReader(openFileDialog1.FileName,Encoding.UTF8);
            String line;
            while ((line = tr.ReadLine())!=null)
            {
                if (line.Contains("<title>"))
                {
                    if (title)
                    {
                        line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                        lines.AddLast(line);
                        add2List(line);
                    }
                    else
                    {
                        title = true;
                    }
                    
                }else if(line.Contains("<wp:post_date>"))
                {
                    line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                    int year = int.Parse(line.Substring(0, 4));
                    int month = int.Parse(line.Substring(5, 2));
                    int day = int.Parse(line.Substring(8, 2));
                    DateTime date = new DateTime(year,month,day);

                    lines.AddLast(line.ToString());
                    add2List(date.ToShortDateString());
                }
                else if (line.Contains("<content:encoded>") && line.Contains("</content:encoded>"))
                {
                    
                    line = line.Substring(28);
                    line = line.Substring(0, line.Length - 21);
                    lines.AddLast(line);
                    add2List(line);
                }
                else if (line.Contains("<content:encoded>"))
                {
                    while (true)
                    {
                        line += "\r\n" + tr.ReadLine();
                        if(line.Contains("</content:encoded>"))
                        {
                            break;
                        }
                        
                    }
                    line = line.Substring(28);
                    line = line.Substring(0,line.Length-21);
                    lines.AddLast(line);
                    add2List(line);
                }else if(line.Contains("<wp:post_name>"))
                {
                    line = line.Substring(line.IndexOf('>') + 1, line.LastIndexOf('<') - line.IndexOf('>') - 1);
                    lines.AddLast(line);
                    add2List(line);
                }


            }

            tr.Close();
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

            posts.Clear();

            for (int i = 0; i < lines.Count; i += 4)
            {
                posts.AddLast(new Post(lines.ElementAt(i), lines.ElementAt(i + 2), lines.ElementAt(i + 1), lines.ElementAt(i + 3)));
            }

            TextWriter tw;

            foreach (Post p in posts)
            {
                tw = new StreamWriter(folderBrowserDialog1.SelectedPath + "\\" + p.date.ToString("yyyy-MM-dd-") + p.postURL + ".md");
                tw.WriteLine("---");
                tw.WriteLine("layout: post");
                tw.WriteLine("title: " + p.postTitle);
                tw.WriteLine("date: " + p.date.ToString("yyyy-MM-dd HH:mm"));
                tw.WriteLine("comments: true");
                tw.WriteLine("categories: []");
                tw.WriteLine("---");
                tw.WriteLine(p.post);
                tw.Close();
            }
        }
    }
}