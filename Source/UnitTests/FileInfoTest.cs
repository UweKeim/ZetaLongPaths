namespace ZetaLongPaths.UnitTests
{
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class FileInfoTest
    {
        [Test]
        public void TestToString()
        {
            var a = new ZlpFileInfo(@"C:\ablage\test.txt");
            var b = new FileInfo(@"C:\ablage\test.txt");

            var x = a.ToString();
            var y = b.ToString();

            Assert.AreEqual(x, y);

            // --

            a = new ZlpFileInfo(@"C:\ablage\");
            b = new FileInfo(@"C:\ablage\");

            x = a.ToString();
            y = b.ToString();

            Assert.AreEqual(x, y);

            // --

            a = new ZlpFileInfo(@"test.txt");
            b = new FileInfo(@"test.txt");

            x = a.ToString();
            y = b.ToString();

            Assert.AreEqual(x, y);

            // --

            a = new ZlpFileInfo(@"c:\ablage\..\ablage\test.txt");
            b = new FileInfo(@"c:\ablage\..\ablage\test.txt");

            x = a.ToString();
            y = b.ToString();

            Assert.AreEqual(x, y);

            // --

            a = new ZlpFileInfo(@"\ablage\test.txt");
            b = new FileInfo(@"\ablage\test.txt");

            x = a.ToString();
            y = b.ToString();

            Assert.AreEqual(x, y);

            // --

            a = new ZlpFileInfo(@"ablage\test.txt");
            b = new FileInfo(@"ablage\test.txt");

            x = a.ToString();
            y = b.ToString();

            Assert.AreEqual(x, y);
        }
    }
}