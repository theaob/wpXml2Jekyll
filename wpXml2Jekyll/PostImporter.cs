using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace wpXml2Jekyll
{
    class PostImporter
    {
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
    }
}