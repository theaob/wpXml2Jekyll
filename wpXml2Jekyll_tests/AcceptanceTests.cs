using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.SimpleLogger.Writers;
using NUnit.Framework;
using wpXml2Jekyll;

namespace wpXml2Jekyll_tests
{
    [TestFixture]
    class AcceptanceTests
    {
        private string helloWorldPost = "2013-11-02-hello-world";
        private string loremIpsumPost = "2013-11-02-lorem-ipsum";
        private string samplePagePost = "2013-11-02-sample-page";
        private string testPost = "2013-11-02-Test";

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertion()
        {
            var wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
            var outputfolderPath = @".\";
            Program.Main(new[] { wordpressXmlFilePath, outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", helloWorldPost);
            var outputFileFullPath = outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }
    }
}
