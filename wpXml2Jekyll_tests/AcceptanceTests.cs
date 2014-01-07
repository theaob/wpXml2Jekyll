using System.IO;
using System.Reflection;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using wpXml2Jekyll;

namespace wpXml2Jekyll_tests
{
    [TestFixture]
    class AcceptanceTests
    {
        // The WordPress xml file (exporttest.wordpress.2013-11-17.xml) contains
        // 4 posts/pages (hello-world, lorem-ipsum, sample-page and test). These 
        // are used in a acceptance/full system test.
        private readonly string _helloWorldPost = "2013-11-02-hello-world";
        private readonly string _loremIpsumPost = "2013-11-02-lorem-ipsum";
        private readonly string _samplePagePost = "2013-11-02-sample-page";
        private readonly string _testPost = "2013-11-02-test";
        private readonly string _wordpressXmlFilePath = @"..\..\exporttest.wordpress.2013-11-17.xml";
        private readonly string _outputfolderPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private void GetValue(string wordPressPost)
        {
            Program.Main(new[] { _wordpressXmlFilePath, _outputfolderPath });
            var outputFileName = string.Format("{0}.md", wordPressPost);
            var outputFileFullPath = _outputfolderPath + Path.DirectorySeparatorChar + outputFileName;
            Approvals.VerifyFile(outputFileFullPath);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionHelloWorldPost()
        {
            GetValue(_helloWorldPost);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionLoremIpsumPost()
        {
            GetValue(_loremIpsumPost);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionSamplePagePost()
        {
            GetValue(_samplePagePost);
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void SimpleWordPressXmlFileConvertionTestPost()
        {
            GetValue(_testPost);
        }
    }
}
