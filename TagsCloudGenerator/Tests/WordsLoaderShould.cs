using System.IO;
using System.Linq;
using NUnit.Framework;
using Telerik.JustMock.AutoMock.Ninject.Infrastructure;

namespace TagsCloudGenerator.Tests
{
    public class WordsLoaderShould
    {
        private const string TestFileName = "loader_test.txt";
        private Options _options;

        [SetUp]
        public void SetUp()
        {
            _options = new Options {PathToBlackList = TestFileName};
        }

        [Test]
        public void LoadBlackListFromTxt()
        {
            var words = new[] {"a", "a", "a", "a", "b", "b", "b", "c", "c", "c", "d"};
            File.WriteAllLines(TestFileName, words);
            var actualWords = WordsLoader.LoadBlackListFromTxt(_options.PathToBlackList);
            CollectionAssert.AreEqual(actualWords, words.Distinct());
        }

        [Test]
        public void LoadWordsFromTxt()
        {
            File.WriteAllLines(TestFileName, new[] {"a", "b", "c", "d"});
            var actualWords = WordsLoader.LoadWordsFromTxt(TestFileName);
            CollectionAssert.AreEqual(actualWords, new[] {"a", "b", "c", "d"});
        }
        
        [Test]
        public void ThrowException_IfFileNotFound()
        {
            var pathToWords = "doesn'texist.txt";
            Assert.Throws<FileNotFoundException>(() => { WordsLoader.LoadWordsFromTxt(pathToWords); });
        }
    }
}