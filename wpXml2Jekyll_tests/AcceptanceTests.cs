using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
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
        public void SimpleWordPressXmlFileConvertionHelloWorldPost()
        {
            var wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
            var outputfolderPath = @".\";
            Program.Main(new[] { wordpressXmlFilePath, outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", helloWorldPost);
            var outputFileFullPath = outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionLoremIpsumPost()
        {
            var wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
            var outputfolderPath = @".\";
            Program.Main(new[] { wordpressXmlFilePath, outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", loremIpsumPost);
            var outputFileFullPath = outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionSamplePagePost()
        {
            var wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
            var outputfolderPath = @".\";
            Program.Main(new[] { wordpressXmlFilePath, outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", samplePagePost);
            var outputFileFullPath = outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionTestPost()
        {
            var wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
            var outputfolderPath = @".\";
            Program.Main(new[] { wordpressXmlFilePath, outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", testPost);
            var outputFileFullPath = outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }
    }
}
