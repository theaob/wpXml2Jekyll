using System;
using System.Windows.Forms;
using System.Xml;

namespace wpXml2Jekyll
{
    public partial class UIForm : Form
    {
        private XmlDocument _xmlDocument;

        public UIForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Wordpress Output (.xml)|*.xml";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName.Length < 2)
            {
                return;
            }

            var postImporter = new PostImporter();
            _xmlDocument = postImporter.ReadWpPosts(openFileDialog1.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            if (folderBrowserDialog1.SelectedPath.Length < 2)
            {
                return;
            }

            var postWriter = new PostWriter(checkBoxExtractImages.Checked);
            var postCount = postWriter.WritePostToMarkdown(_xmlDocument, folderBrowserDialog1.SelectedPath);

            MessageBox.Show("Saved " + postCount + " posts");
        }
    }
}