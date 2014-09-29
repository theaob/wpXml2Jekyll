using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NDesk.Options;

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
                var wordpressXmlFile = "";
                var outputFolder = "";
                var images = false;

                var optionSet = new OptionSet
                    {
                        {"i|input=", "WordPress export file", v => wordpressXmlFile = v},
                        {"o|output=", "Output folder", v => outputFolder = v},
                        {"images", "Extract images from posts", v => images = v != null},
                    };

                var extras = optionSet.Parse(args);
                if (string.IsNullOrEmpty(wordpressXmlFile) || string.IsNullOrEmpty(outputFolder))
                {
                    ShowHelp(optionSet);
                    return;
                }
                

                var posts = new PostImporter().ReadWpPosts(wordpressXmlFile);
                int count = new PostWriter(images).WritePostToMarkdown(posts, outputFolder);

                Console.WriteLine("Saved " + count + " posts");
            }
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: wpXml2Jekyll -input=<input_file> -output=<output_folder> [OPTIONS]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int FreeConsole();
    }
}
