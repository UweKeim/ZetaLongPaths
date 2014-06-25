namespace ZetaLongPaths.UnitTests
{
	using System.IO;
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
		public void TestGeneral()
		{
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
			var fn1 = new FileInfo(filePath).Directory.FullName;
			var fn2 = new ZlpFileInfo(filePath).Directory.FullName;

			var fn1a = new FileInfo(filePath).DirectoryName;
			var fn2a = new ZlpFileInfo(filePath).DirectoryName;

			Assert.AreEqual(fn1,fn2);
			Assert.AreEqual(fn1a,fn2a);

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