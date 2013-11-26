using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using wpXml2Jekyll.Properties;

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
                FreeConsole();
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
                if (Settings.Default.UseXmlParsing)
                {
                    var posts = new PostImporter().ReadWpPosts(wordpressXmlFile);
                    new PostWriter().WritePostToMarkdown(posts, outputFolder);
                }
                else
                {
                    var posts = new PostImporter().ReadPosts(wordpressXmlFile, s => { });
                    new PostWriter().WritePostToMarkdown(posts, outputFolder);
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int FreeConsole();
    }
}
