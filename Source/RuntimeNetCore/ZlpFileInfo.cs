namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using Microsoft.Win32.SafeHandles;
    using Native;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using FileAccess = Native.FileAccess;
    using FileAttributes = Native.FileAttributes;
    using FileShare = Native.FileShare;

    // ReSharper disable once UseNameofExpression
    [DebuggerDisplay(@"{FullName}")]
    public class ZlpFileInfo :
        IZlpFileSystemInfo
    {
        [UsedImplicitly]
        public static ZlpFileInfo GetTemp() => new ZlpFileInfo(ZlpPathHelper.GetTempFilePath());

        public ZlpFileInfo(string path)
        {
            FullName = path;
        }

        [UsedImplicitly]
        public ZlpFileInfo(FileInfo path)
        {
            FullName = path?.FullName;
        }

        [UsedImplicitly]
        public ZlpFileInfo(ZlpFileInfo path)
        {
            FullName = path?.FullName;
        }

        [UsedImplicitly]
        public static ZlpFileInfo FromOther(ZlpFileInfo path)
        {
            return new ZlpFileInfo(path);
        }

        [UsedImplicitly]
        public static ZlpFileInfo FromString(string path)
        {
            return new ZlpFileInfo(path);
        }

        [UsedImplicitly]
        public static ZlpFileInfo FromBuiltIn(FileInfo path)
        {
            return new ZlpFileInfo(path);
        }

        [UsedImplicitly]
        public FileInfo ToBuiltIn()
        {
            return new FileInfo(FullName);
        }

        [UsedImplicitly]
        public ZlpFileInfo ToOther()
        {
            return Clone();
        }

        [UsedImplicitly]
        public void Refresh()
        {
        }

        [UsedImplicitly]
        public ZlpFileInfo Clone()
        {
            return new ZlpFileInfo(FullName);
        }

        [UsedImplicitly]
        public ZlpFileInfo GetFullPath()
        {
            return new ZlpFileInfo(ZlpPathHelper.GetFullPath(FullName));
        }

        [UsedImplicitly]
        public bool IsReadOnly
        {
            get => (Attributes & FileAttributes.Readonly) != 0;
            set
            {
                if (value)
                    Attributes |= FileAttributes.Readonly;
                else
                    Attributes &= ~FileAttributes.Readonly;
            }
        }

        public void MoveToRecycleBin() => ZlpIOHelper.MoveFileToRecycleBin(FullName);

        public string OriginalPath => FullName;

        public override string ToString() => FullName;

        public void MoveTo(
            string destinationFilePath) => ZlpIOHelper.MoveFile(FullName, destinationFilePath);

        [UsedImplicitly]
        public void MoveTo(
            ZlpFileInfo destinationFilePath)
            => ZlpIOHelper.MoveFile(FullName, destinationFilePath.FullName);

        public void MoveTo(
            string destinationFilePath,
            bool overwriteExisting) => ZlpIOHelper.MoveFile(FullName, destinationFilePath, overwriteExisting);

        [UsedImplicitly]
        public void MoveTo(
            ZlpFileInfo destinationFilePath,
            bool overwriteExisting)
            => ZlpIOHelper.MoveFile(FullName, destinationFilePath.FullName, overwriteExisting);

        /// <summary>
        /// Pass the file handle to the <see cref="System.IO.FileStream"/> constructor. 
        /// The <see cref="System.IO.FileStream"/> will close the handle.
        /// </summary>
        [UsedImplicitly]
        public SafeFileHandle CreateHandle(
            CreationDisposition creationDisposition,
            FileAccess fileAccess,
            FileShare fileShare)
        {
            return ZlpIOHelper.CreateFileHandle(FullName, creationDisposition, fileAccess, fileShare);
        }

        [UsedImplicitly]
        public void CopyTo(
            string destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFile(FullName, destinationFilePath, overwriteExisting);
        }

        [UsedImplicitly]
        public void CopyTo(
            ZlpFileInfo destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFile(FullName, destinationFilePath.FullName, overwriteExisting);
        }

        [UsedImplicitly]
        public void CopyToExact(
            string destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFileExact(FullName, destinationFilePath, overwriteExisting);
        }

        [UsedImplicitly]
        public void CopyToExact(
            ZlpFileInfo destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFileExact(FullName, destinationFilePath.FullName, overwriteExisting);
        }

        [UsedImplicitly]
        public void Delete() => ZlpIOHelper.DeleteFile(FullName);

        [UsedImplicitly]
        public void DeleteFileAfterReboot() => ZlpIOHelper.DeleteFileAfterReboot(FullName);

        [UsedImplicitly]
        public void Touch() => ZlpIOHelper.Touch(FullName);

        [UsedImplicitly]
        public string Owner => ZlpIOHelper.GetFileOwner(FullName);

        public bool Exists => ZlpIOHelper.FileExists(FullName);

        [UsedImplicitly]
        public byte[] ReadAllBytes()
        {
            return ZlpIOHelper.ReadAllBytes(FullName);
        }

        [UsedImplicitly]
        public FileStream OpenRead()
        {
            return new FileStream(
                ZlpIOHelper.CreateFileHandle(
                    FullName,
                    CreationDisposition.OpenAlways,
                    FileAccess.GenericRead,
                    FileShare.Read),
                System.IO.FileAccess.Read);
        }

        [UsedImplicitly]
        public FileStream OpenWrite()
        {
            return new FileStream(
                ZlpIOHelper.CreateFileHandle(
                    FullName,
                    CreationDisposition.OpenAlways,
                    FileAccess.GenericRead | FileAccess.GenericWrite,
                    FileShare.Read | FileShare.Write),
                System.IO.FileAccess.ReadWrite);
        }

        [UsedImplicitly]
        public FileStream OpenCreate()
        {
            return new FileStream(
                ZlpIOHelper.CreateFileHandle(
                    FullName,
                    CreationDisposition.CreateAlways,
                    FileAccess.GenericRead | FileAccess.GenericWrite,
                    FileShare.Read | FileShare.Write),
                System.IO.FileAccess.ReadWrite);
        }

        [UsedImplicitly]
        public string ReadAllText() => ZlpIOHelper.ReadAllText(FullName);

        [UsedImplicitly]
        public string ReadAllText(Encoding encoding) => ZlpIOHelper.ReadAllText(FullName, encoding);

        [UsedImplicitly]
        public string[] ReadAllLines() => ZlpIOHelper.ReadAllLines(FullName);

        [UsedImplicitly]
        public string[] ReadAllLines(Encoding encoding) => ZlpIOHelper.ReadAllLines(FullName, encoding);

        [UsedImplicitly]
        public void WriteAllText(string text, Encoding encoding = null)
            => ZlpIOHelper.WriteAllText(FullName, text, encoding);

        [UsedImplicitly]
        public void WriteAllLines(string[] lines, Encoding encoding = null)
            => ZlpIOHelper.WriteAllLines(FullName, lines, encoding);

        [UsedImplicitly]
        public void AppendText(string text, Encoding encoding = null)
            => ZlpIOHelper.AppendText(FullName, text, encoding);

        [UsedImplicitly]
        public void WriteAllBytes(byte[] content) => ZlpIOHelper.WriteAllBytes(FullName, content);

        public DateTime LastWriteTime
        {
            get => ZlpIOHelper.GetFileLastWriteTime(FullName);
            set => ZlpIOHelper.SetFileLastWriteTime(FullName, value);
        }

        public DateTime LastAccessTime
        {
            get => ZlpIOHelper.GetFileLastAccessTime(FullName);
            set => ZlpIOHelper.SetFileLastAccessTime(FullName, value);
        }

        public DateTime CreationTime
        {
            get => ZlpIOHelper.GetFileCreationTime(FullName);
            set => ZlpIOHelper.SetFileCreationTime(FullName, value);
        }

        public string FullName { get; }

        public string Name => ZlpPathHelper.GetFileNameFromFilePath(FullName);

        /// <summary>
        /// Returns the same MD5 hash as the PHP function call http://php.net/manual/de/function.hash-file.php 
        /// with 'md5' as the first parameter.
        /// </summary>
        public string MD5Hash => ZlpIOHelper.CalculateMD5Hash(FullName);

        [UsedImplicitly]
        public string NameWithoutExtension => ZlpPathHelper.GetFileNameWithoutExtension(Name);

        public ZlpDirectoryInfo Directory => new ZlpDirectoryInfo(DirectoryName);

        public string DirectoryName => ZlpPathHelper.GetDirectoryPathNameFromFilePath(FullName);

        public string Extension => ZlpPathHelper.GetExtension(FullName);

        public long Length => ZlpIOHelper.GetFileLength(FullName);

        public FileAttributes Attributes
        {
            get => ZlpIOHelper.GetFileAttributes(FullName);
            set => ZlpIOHelper.SetFileAttributes(FullName, value);
        }
    }
}