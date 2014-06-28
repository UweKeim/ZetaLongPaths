namespace ZetaLongPaths.UnitTests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class IOHelperTest
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
        public void TestFolderVsFile()
        {
            Assert.IsTrue(ZlpIOHelper.FileExists(@"c:\Windows\notepad.exe"));
            Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\dslfsdjklfhsd\kjsaklfjd.exe"));
            Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\Windows"));
            Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\Windows\"));

            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\Windows\notepad.exe"));
            Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows"));
            Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows\"));
            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh"));
            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh\"));
        }

        [Test]
        public void TestGeneral()
        {
            //var m = ZlpIOHelper.GetFileLength(@"C:\Ablage\test.temp");
            //Assert.IsTrue(m > 0);

            const string f = @"C:\Ablage\IconExperience - O-Collections\readme_o.txt";
            var m = ZlpIOHelper.GetFileLength(f);
            Assert.IsTrue(m > 0);
            Assert.IsTrue(m==new System.IO.FileInfo(f).Length);


            Assert.IsTrue(ZlpIOHelper.FileExists(@"c:\Windows\notepad.exe"));
            Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\dslfsdjklfhsd\kjsaklfjd.exe"));
            Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\ablage"));

            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\Windows\notepad.exe"));
            Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows"));
            Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows\"));
            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh"));
            Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh\"));

            // --

            Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastWriteTime(@"c:\ablage\test.txt", new DateTime(1986, 1, 1)));
            Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastAccessTime(@"c:\ablage\test.txt", new DateTime(1987, 1, 1)));
            Assert.DoesNotThrow(() => ZlpIOHelper.SetFileCreationTime(@"c:\ablage\test.txt", new DateTime(1988, 1, 1)));

            ZlpIOHelper.WriteAllText(@"c:\ablage\file.txt", @"äöü.");
            Assert.IsTrue(ZlpIOHelper.FileExists(@"c:\ablage\file.txt"));

            var time = ZlpIOHelper.GetFileLastWriteTime(@"c:\ablage\fehlerbericht.txt");
            Assert.Greater(time, DateTime.MinValue);

            var owner = ZlpIOHelper.GetFileOwner(@"c:\Windows\notepad.exe");
            Assert.IsNotNullOrEmpty(owner);

            var l = ZlpIOHelper.GetFileLength(
                @"C:\Ablage\CEG\Bodycote-CEG-WebApps-Intranet.rar");
            Assert.IsTrue(l > 0);

            return;
            // --

            //Assert.IsTrue(
            //    ZlpIOHelper.DirectoryExists(@"C:\Users\ukeim\"));
            //Assert.IsTrue(
            //    ZlpIOHelper.DirectoryExists(@"C:\Users\ukeim"));
            //Assert.IsFalse(
            //    ZlpIOHelper.DirectoryExists(@"C:\Users\ukeim\Documents\Visual Studio 2008\Projects\Zeta Producer 9\Zeta Producer Main\Deploy\Origin\Enterprise\C-Allgaier\Web\QM-Handbuch-Freigabe\Bin"));

            //return;

            //Assert.DoesNotThrow(
            //    () => ZlpIOHelper.CreateDirectory(@"c:\ablage\1\2\3\vier\fünf\"));

            //Assert.DoesNotThrow(
            //    () => ZlpIOHelper.CopyFile(
            //        @"C:\Users\ukeim\Documents\Visual Studio 2008\Projects\Zeta Producer 9\Zeta Producer Main\Bin\Applications\de\MessageBoxExLib.resources.dll",
            //        @"C:\Users\ukeim\Documents\Visual Studio 2008\Projects\Zeta Producer 9\Zeta Producer Main\Deploy\Origin\Enterprise\C-Allgaier\Windows\Applications\de\MessageBoxExLib.resources.dll",
            //        true));
        }

        [Test]
        public void TestCodePlex()
        {
            // http://zetalongpaths.codeplex.com/discussions/396147

            const string directoryPath =
                @"c:\1234567890123456789012345678901234567890";
            const string filePath =
                @"c:\1234567890123456789012345678901234567890\1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345.jpg";

            Assert.DoesNotThrow(() => ZlpIOHelper.CreateDirectory(directoryPath));
            Assert.DoesNotThrow(() => ZlpIOHelper.WriteAllText(filePath, @"test"));
            Assert.DoesNotThrow(() => ZlpIOHelper.DeleteFile(filePath));
            Assert.DoesNotThrow(() => ZlpIOHelper.DeleteDirectory(directoryPath, true));
        }

        // ------------------------------------------------------------------
        #endregion
    }
}