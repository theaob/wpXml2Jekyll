using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace wpXml2Jekyll
{
    public partial class UIForm : Form
    {
        private XmlDocument xmlDocument;
        private readonly PostWriter _postWriter;

        public UIForm()
        {
            InitializeComponent();
            _postWriter = new PostWriter();
        }

        

        public PostWriter PostWriter
        {
            get { return _postWriter; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wordpress Output (.xml)|*.xml";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName.Length < 2)
            {
                return;
            }

            xmlDocument = new PostImporter().ReadWpPosts(openFileDialog1.FileName);
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

            PostWriter.WritePostToMarkdown(xmlDocument, folderBrowserDialog1.SelectedPath);
        }
    }
}