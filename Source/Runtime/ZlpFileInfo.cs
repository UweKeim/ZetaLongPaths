namespace ZetaLongPaths
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using Microsoft.Win32.SafeHandles;
    using Native;
    using FileAccess = Native.FileAccess;
    using FileAttributes = Native.FileAttributes;
    using FileShare = Native.FileShare;

    [DebuggerDisplay(@"{FullName}")]
    public class ZlpFileInfo
    {
        private readonly string _path;

        public static ZlpFileInfo GetTemp()
        {
            return new ZlpFileInfo(ZlpPathHelper.GetTempFilePath());
        }

        public ZlpFileInfo(string path)
        {
            _path = path;
        }

        public void Refresh()
        {
        }

        public void MoveToRecycleBin()
        {
            ZlpIOHelper.MoveFileToRecycleBin(_path);
        }

        public string OriginalPath
        {
            get { return _path; }
        }

        public override string ToString()
        {
            return _path;
        }

        public void MoveTo(string destinationFilePath)
        {
            ZlpIOHelper.MoveFile(_path, destinationFilePath);
        }

        public void MoveTo(ZlpFileInfo destinationFilePath)
        {
            ZlpIOHelper.MoveFile(_path, destinationFilePath.FullName);
        }

        /// <summary>
        /// Pass the file handle to the <see cref="System.IO.FileStream"/> constructor. 
        /// The <see cref="System.IO.FileStream"/> will close the handle.
        /// </summary>
        public SafeFileHandle CreateHandle(
            CreationDisposition creationDisposition,
            FileAccess fileAccess,
            FileShare fileShare)
        {
            return ZlpIOHelper.CreateFileHandle(_path, creationDisposition, fileAccess, fileShare);
        }

        public void CopyTo(
            string destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFile(_path, destinationFilePath, overwriteExisting);
        }

        public void CopyTo(
            ZlpFileInfo destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFile(_path, destinationFilePath._path, overwriteExisting);
        }

        public void CopyToExact(
            string destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFileExact(_path, destinationFilePath, overwriteExisting);
        }

        public void CopyToExact(
            ZlpFileInfo destinationFilePath,
            bool overwriteExisting)
        {
            ZlpIOHelper.CopyFileExact(_path, destinationFilePath._path, overwriteExisting);
        }

        public void Delete()
        {
            ZlpIOHelper.DeleteFile(_path);
        }

        public void DeleteFileAfterReboot()
        {
            ZlpIOHelper.DeleteFileAfterReboot(_path);
        }

        public string Owner
        {
            get { return ZlpIOHelper.GetFileOwner(_path); }
        }

        public bool Exists
        {
            get { return ZlpIOHelper.FileExists(_path); }
        }

        public byte[] ReadAllBytes()
        {
            return ZlpIOHelper.ReadAllBytes(_path);
        }

        public System.IO.FileStream OpenRead()
        {
            return new System.IO.FileStream(
                ZlpIOHelper.CreateFileHandle(
                    _path,
                    CreationDisposition.OpenAlways,
                    FileAccess.GenericRead,
                    FileShare.Read),
                System.IO.FileAccess.Read);
        }

        public System.IO.FileStream OpenWrite()
        {
            return new System.IO.FileStream(
                ZlpIOHelper.CreateFileHandle(
                    _path,
                    CreationDisposition.OpenAlways,
                    FileAccess.GenericRead | FileAccess.GenericWrite,
                    FileShare.Read | FileShare.Write),
                System.IO.FileAccess.ReadWrite);
        }

        public string ReadAllText()
        {
            return ZlpIOHelper.ReadAllText(_path);
        }

        public string ReadAllText(Encoding encoding)
        {
            return ZlpIOHelper.ReadAllText(_path, encoding);
        }

        public string[] ReadAllLines()
        {
            return ZlpIOHelper.ReadAllLines(_path);
        }

        public string[] ReadAllLines(Encoding encoding)
        {
            return ZlpIOHelper.ReadAllLines(_path, encoding);
        }

        public void WriteAllText(string text, Encoding encoding = null)
        {
            ZlpIOHelper.WriteAllText(_path, text, encoding);
        }

        public void AppendText(string text, Encoding encoding = null)
        {
            ZlpIOHelper.AppendText(_path, text, encoding);
        }

        public void WriteAllBytes(byte[] content)
        {
            ZlpIOHelper.WriteAllBytes(_path, content);
        }

        public DateTime LastWriteTime
        {
            get { return ZlpIOHelper.GetFileLastWriteTime(_path); }
            set { ZlpIOHelper.SetFileLastWriteTime(_path, value); }
        }

        public DateTime LastAccessTime
        {
            get { return ZlpIOHelper.GetFileLastAccessTime(_path); }
            set { ZlpIOHelper.SetFileLastAccessTime(_path, value); }
        }

        public DateTime CreationTime
        {
            get { return ZlpIOHelper.GetFileCreationTime(_path); }
            set { ZlpIOHelper.SetFileCreationTime(_path, value); }
        }

        public string FullName
        {
            get { return _path; }
        }

        public string Name
        {
            get { return ZlpPathHelper.GetFileNameFromFilePath(_path); }
        }

        public string NameWithoutExtension
        {
            get { return ZlpPathHelper.GetFileNameWithoutExtension(Name); }
        }

        public ZlpDirectoryInfo Directory
        {
            get { return new ZlpDirectoryInfo(DirectoryName); }
        }

        public string DirectoryName
        {
            get { return ZlpPathHelper.GetDirectoryPathNameFromFilePath(_path); }
        }

        public string Extension
        {
            get { return ZlpPathHelper.GetExtension(_path); }
        }

        public long Length
        {
            get { return ZlpIOHelper.GetFileLength(_path); }
        }

        public FileAttributes Attributes
        {
            get { return ZlpIOHelper.GetFileAttributes(_path); }
            set { ZlpIOHelper.SetFileAttributes(_path, value); }
        }
    }
}