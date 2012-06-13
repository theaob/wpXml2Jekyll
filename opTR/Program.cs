using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace opTR
{
    class Program
    {
        static String text;
        static String markdown;
        static String post; 

        static String[] specials = { "ş", "Ş", "ğ", "Ğ", "ü", "Ü", "İ", "ı", "ç", "Ç", "ö", "Ö" };
        static String[] numericals = { "&#351;", "&#350;", "&#287;", "&#286;", "&uuml;", "&Uuml;", "&#304;", "&#305;", "&ccedil;", "&Ccedil;", "&#246;", "&#214;" };

        static void Main(string[] args)
        {
            Console.WriteLine("Türkçe karakterleri ascii karşılıklarına çevir");

            try
            {
                TextReader tr = new StreamReader(args[0], Encoding.UTF8);
                text = tr.ReadToEnd();
                tr.Close();
                markdown = text.Substring(0, text.IndexOf("---", 4));
                post = text.Substring(text.LastIndexOf("---"));
                replace();
                text = markdown + post;
                TextWriter tw = new StreamWriter(args[0], false, Encoding.UTF8);
                tw.Write(text);
                tw.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return;
            }
            finally
            {
                Console.WriteLine("İşlem tamamlandı. Bir tuşa basınız.");
            }
            Console.ReadKey();
        }



        private static void replace()
        {
            for (int i = 0; i < specials.Length; i++)
            {
                markdown = markdown.Replace(specials[i], numericals[i]);
            }
        }
    }
}
