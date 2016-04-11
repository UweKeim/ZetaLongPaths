namespace ZetaLongPaths.UnitTests
{
    using System;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

	[TestFixture]
	public class DirectoryInfoTest
	{
		#region Set up and tear down.
		// ------------------------------------------------------------------

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			//...
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			//...
		}

		[SetUp]
		public void SetUp()
		{
			//...
		}

		[TearDown]
		public void TearDown()
		{
			//...
		}

		// ------------------------------------------------------------------
		#endregion

		#region Tests.
		// ------------------------------------------------------------------

	    [Test]
	    public void TestMove()
	    {
	        var path = ZlpDirectoryInfo.GetTemp().CombineDirectory(Guid.NewGuid().ToString()).CheckCreate();
	        try
	        {
	            var p1 = path.CombineDirectory(@"a").CheckCreate();
	            var p2 = path.CombineDirectory(@"b");

	            var f1 = p1.CombineFile("1.txt");
                f1.WriteAllText("1");

	            Assert.DoesNotThrow(() => p1.MoveTo(p2));
	        }
	        finally
	        {
	            path.SafeDelete();
	        }
	    }

        [Test]
	    public void TestGetFileSystemInfos()
	    {
            var path = ZlpDirectoryInfo.GetTemp().CombineDirectory(Guid.NewGuid().ToString()).CheckCreate();
            try
            {
                var p1 = path.CombineDirectory(@"a").CheckCreate();
                path.CombineDirectory(@"b").CheckCreate();

                var f1 = p1.CombineFile("1.txt");
                f1.WriteAllText("1");
                
                Assert.IsTrue(path.GetFileSystemInfos().Length == 2);
                Assert.IsTrue(path.GetFileSystemInfos(SearchOption.AllDirectories).Length == 3);
                Assert.IsTrue(path.GetFileSystemInfos(SearchOption.AllDirectories).Where(f => f is ZlpFileInfo).ToList().Count == 1);
                Assert.IsTrue(path.GetFileSystemInfos(SearchOption.AllDirectories).Where(f => f is ZlpDirectoryInfo).ToList().Count == 2);

            }
            finally
            {
                path.SafeDelete();
            }
        }

	    [Test]
		public void TestGeneral()
		{
            // Ordner mit Punkt am Ende.
            string dir = $@"C:\Ablage\{Guid.NewGuid():N}.";
            Assert.IsFalse(new ZlpDirectoryInfo(dir).Exists);
            new ZlpDirectoryInfo(dir).CheckCreate();
            Assert.IsTrue(new ZlpDirectoryInfo(dir).Exists);
            new ZlpDirectoryInfo(dir).Delete(true);
            Assert.IsFalse(new ZlpDirectoryInfo(dir).Exists);


			//Assert.IsTrue(new ZlpDirectoryInfo(Path.GetTempPath()).CreationTime>DateTime.MinValue);
			//Assert.IsTrue(new ZlpDirectoryInfo(Path.GetTempPath()).Exists);
			//Assert.IsFalse(new ZlpDirectoryInfo(@"C:\Ablage\doesnotexistjdlkfjsdlkfj").Exists);
			//Assert.IsTrue(new ZlpDirectoryInfo(Path.GetTempPath()).Exists);
			//Assert.IsFalse(new ZlpDirectoryInfo(@"C:\Ablage\doesnotexistjdlkfjsdlkfj2").Exists);
			//Assert.IsFalse(new ZlpDirectoryInfo(@"\\zetac11\C$\Ablage").Exists);
			//Assert.IsFalse(new ZlpDirectoryInfo(@"\\zetac11\C$\Ablage\doesnotexistjdlkfjsdlkfj2").Exists);

			const string s1 = @"C:\Users\Chris\Documents\Development\ADC\InterStore.NET\Visual Studio 2008\6.4.2\Zeta Resource Editor";
			const string s2 = @"C:\Users\Chris\Documents\Development\ADC\InterStore.NET\Visual Studio 2008\6.4.2\Web\central\Controls\App_LocalResources\ItemSearch";

			var s3 = ZlpPathHelper.GetRelativePath(s1, s2);
			Assert.AreEqual(s3, @"..\Web\central\Controls\App_LocalResources\ItemSearch");

			var ext = ZlpPathHelper.GetExtension(s3);
			Assert.IsNullOrEmpty(ext);

			ext = ZlpPathHelper.GetExtension(@"C:\Ablage\Uwe.txt");
			Assert.AreEqual(ext, @".txt");

			const string path = @"C:\Ablage\Test";
			Assert.AreEqual(
				new DirectoryInfo(path).Name,
				new ZlpDirectoryInfo(path).Name);

			Assert.AreEqual(
				new DirectoryInfo(path).FullName,
				new ZlpDirectoryInfo(path).FullName);

			const string filePath = @"C:\Ablage\Test\file.txt";
			var fn1 = new FileInfo(filePath).Directory?.FullName;
			var fn2 = new ZlpFileInfo(filePath).Directory.FullName;

			var fn1A = new FileInfo(filePath).DirectoryName;
			var fn2A = new ZlpFileInfo(filePath).DirectoryName;

			Assert.AreEqual(fn1,fn2);
			Assert.AreEqual(fn1A,fn2A);

			var fn = new ZlpDirectoryInfo(@"\\zetac11\C$\Ablage\doesnotexistjdlkfjsdlkfj2").Parent.FullName;

			Assert.AreEqual(fn, @"\\zetac11\C$\Ablage");

			fn = new ZlpDirectoryInfo(@"\\zetac11\C$\Ablage\doesnotexistjdlkfjsdlkfj2\").Parent.FullName;

			Assert.AreEqual(fn, @"\\zetac11\C$\Ablage");
		}

		[Test]
		public void TestToString()
		{
			var a = new ZlpDirectoryInfo(@"C:\ablage\test.txt");
			var b = new DirectoryInfo(@"C:\ablage\test.txt");

			var x = a.ToString();
			var y = b.ToString();

			Assert.AreEqual(x, y);

			// --

			a = new ZlpDirectoryInfo(@"C:\ablage\");
			b = new DirectoryInfo(@"C:\ablage\");

			x = a.ToString();
			y = b.ToString();

			Assert.AreEqual(x, y);

			// --

			a = new ZlpDirectoryInfo(@"test.txt");
			b = new DirectoryInfo(@"test.txt");

			x = a.ToString();
			y = b.ToString();

			Assert.AreEqual(x, y);

			// --

			a = new ZlpDirectoryInfo(@"c:\ablage\..\ablage\test.txt");
			b = new DirectoryInfo(@"c:\ablage\..\ablage\test.txt");

			x = a.ToString();
			y = b.ToString();

			Assert.AreEqual(x, y);

			// --

			a = new ZlpDirectoryInfo(@"\ablage\test.txt");
			b = new DirectoryInfo(@"\ablage\test.txt");

			x = a.ToString();
			y = b.ToString();

			Assert.AreEqual(x, y);

			// --

			a = new ZlpDirectoryInfo(@"ablage\test.txt");
			b = new DirectoryInfo(@"ablage\test.txt");

			x = a.ToString();
			y = b.ToString();

			Assert.AreEqual(x, y);
		}

		// ------------------------------------------------------------------
		#endregion
	}
}