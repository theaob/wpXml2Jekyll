using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace wpXml2Jekyll
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new UIForm());
            }
            else
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Usage: wpXml2Jekyll [wordpress export file] [output folder]");
                    Environment.Exit(1);
                }
                var wordpressXmlFile = args[0];
                var outputFolder = args[1];
                var application = new UIForm();
                var posts = application.ReadPosts(wordpressXmlFile, s => { });
                application.WritePostToMarkdown(posts, outputFolder);
            }
        }
    }
}
