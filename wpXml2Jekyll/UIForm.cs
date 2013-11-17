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
        private readonly PostWriter _postWriter;

        public UIForm()
        {
            InitializeComponent();
            _postWriter = new PostWriter();
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

        public PostWriter PostWriter
        {
            get { return _postWriter; }
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

            lines = new PostImporter().ReadPosts(openFileDialog1.FileName, add2List);
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

            PostWriter.WritePostToMarkdown(lines, folderBrowserDialog1.SelectedPath);
        }
    }
}