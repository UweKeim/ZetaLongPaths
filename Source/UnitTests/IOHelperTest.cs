namespace ZetaLongPaths.UnitTests
{
    using System;
    using System.IO;

    using NUnit.Framework;

    using ZetaLongPaths.Native;

    using FileAccess = ZetaLongPaths.Native.FileAccess;
    using FileShare = ZetaLongPaths.Native.FileShare;

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
            var tempFolder = Environment.ExpandEnvironmentVariables("%temp%");
            Assert.True(ZlpIOHelper.DirectoryExists(tempFolder));

            var tempPath = ZlpPathHelper.Combine(tempFolder, "ZlpTest");

            try
            {
                ZlpIOHelper.CreateDirectory(tempPath);
                Assert.IsTrue(ZlpIOHelper.DirectoryExists(tempPath));

                var filePath = ZlpPathHelper.Combine(tempPath, "text.zlp");
                var fileHandle = ZlpIOHelper.CreateFileHandle(
                    filePath,
                    CreationDisposition.CreateAlways,
                    FileAccess.GenericWrite | FileAccess.GenericRead,
                    FileShare.None);
                var textStream = new StreamWriter(new FileStream(fileHandle, System.IO.FileAccess.Write));
                textStream.WriteLine("Zeta Long Paths Extended testing...");
                textStream.Flush();
                textStream.Close();
                fileHandle.Close();

                Assert.IsTrue(ZlpIOHelper.FileExists(filePath));


                var m = ZlpIOHelper.GetFileLength(filePath);
                Assert.IsTrue(m > 0);
                Assert.IsTrue(m == new FileInfo(filePath).Length);


                Assert.IsTrue(ZlpIOHelper.FileExists(@"c:\Windows\notepad.exe"));
                Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\dslfsdjklfhsd\kjsaklfjd.exe"));
                Assert.IsFalse(ZlpIOHelper.FileExists(@"c:\ablage"));

                Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\Windows\notepad.exe"));
                Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows"));
                Assert.IsTrue(ZlpIOHelper.DirectoryExists(@"c:\Windows\"));
                Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh"));
                Assert.IsFalse(ZlpIOHelper.DirectoryExists(@"c:\fkjhskfsdhfjkhsdjkfhsdkjfh\"));

                // --

                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastWriteTime(filePath, new DateTime(1986, 1, 1)));
                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastAccessTime(filePath, new DateTime(1987, 1, 1)));
                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileCreationTime(filePath, new DateTime(1988, 1, 1)));

                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastWriteTime(tempPath, new DateTime(1986, 1, 1)));
                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileLastAccessTime(tempPath, new DateTime(1987, 1, 1)));
                Assert.DoesNotThrow(() => ZlpIOHelper.SetFileCreationTime(tempPath, new DateTime(1988, 1, 1)));

                var anotherFile = ZlpPathHelper.Combine(tempPath, "test2.zpl");
                ZlpIOHelper.WriteAllText(anotherFile, @"äöü.");
                Assert.IsTrue(ZlpIOHelper.FileExists(anotherFile));

                var time = ZlpIOHelper.GetFileLastWriteTime(filePath);
                Assert.Greater(time, DateTime.MinValue);

                var owner = ZlpIOHelper.GetFileOwner(@"c:\Windows\notepad.exe");
                Assert.IsNotNullOrEmpty(owner);

                var l = ZlpIOHelper.GetFileLength(anotherFile);
                Assert.IsTrue(l > 0);
            }
            finally
            {
                ZlpIOHelper.DeleteDirectory(tempPath, true);
            }

            return;
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