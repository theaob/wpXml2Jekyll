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
        private readonly string _helloWorldPost = "2013-11-02-hello-world";
        private readonly string _loremIpsumPost = "2013-11-02-lorem-ipsum";
        private readonly string _samplePagePost = "2013-11-02-sample-page";
        private readonly string _testPost = "2013-11-02-Test";
        private readonly string _wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
        private readonly string _outputfolderPath = @".\";

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionHelloWorldPost()
        {
            Program.Main(new[] { _wordpressXmlFilePath, _outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", _helloWorldPost);
            var outputFileFullPath = _outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionLoremIpsumPost()
        {
            Program.Main(new[] { _wordpressXmlFilePath, _outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", _loremIpsumPost);
            var outputFileFullPath = _outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionSamplePagePost()
        {
            Program.Main(new[] { _wordpressXmlFilePath, _outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", _samplePagePost);
            var outputFileFullPath = _outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionTestPost()
        {
            Program.Main(new[] { _wordpressXmlFilePath, _outputfolderPath });
            var outputFileName = string.Format(".\\{0}.md", _testPost);
            var outputFileFullPath = _outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }
    }
}
