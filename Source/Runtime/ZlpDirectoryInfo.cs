namespace ZetaLongPaths
{
    using System;
    using System.Diagnostics;
    using Native;

    [DebuggerDisplay(@"{FullName}")]
    public class ZlpDirectoryInfo
    {
        private readonly string _path;

        public static ZlpDirectoryInfo GetTemp()
        {
            return new ZlpDirectoryInfo(ZlpPathHelper.GetTempDirectoryPath());
        }

        public static ZlpDirectoryInfo GetFolderPath(
            Environment.SpecialFolder specialFolder)
        {
            return new ZlpDirectoryInfo(Environment.GetFolderPath(specialFolder));
        }

#if !NET20
        public static ZlpDirectoryInfo GetFolderPath(
            Environment.SpecialFolder specialFolder,
            Environment.SpecialFolderOption option)
        {
            return new ZlpDirectoryInfo(Environment.GetFolderPath(specialFolder, option));
        }
#endif

        public ZlpDirectoryInfo(string path)
        {
            _path = path;
        }

        public void Refresh()
        {
        }

        public bool Exists
        {
            get { return ZlpIOHelper.DirectoryExists(_path); }
        }

        public void MoveToRecycleBin()
        {
            ZlpIOHelper.MoveDirectoryToRecycleBin(_path);
        }

        public string OriginalPath
        {
            get { return _path; }
        }

        public override string ToString()
        {
            return _path;
        }

        public void Delete(bool recursive)
        {
            ZlpIOHelper.DeleteDirectory(_path, recursive);
        }

        public void Create()
        {
            ZlpIOHelper.CreateDirectory(_path);
        }

        public void MoveTo(string destinationDirectoryPath)
        {
            ZlpIOHelper.MoveDirectory(_path, destinationDirectoryPath);
        }

        public void MoveTo(ZlpDirectoryInfo destinationDirectoryPath)
        {
            ZlpIOHelper.MoveDirectory(_path, destinationDirectoryPath.FullName);
        }

        public string FullName
        {
            get { return _path; }
        }

        public string Name
        {
            get { return ZlpPathHelper.GetDirectoryNameOnlyFromFilePath(_path); }
        }

        public ZlpFileInfo[] GetFiles()
        {
            return ZlpIOHelper.GetFiles(_path);
        }

        public ZlpFileInfo[] GetFiles(string pattern)
        {
            return ZlpIOHelper.GetFiles(_path, pattern);
        }

        public ZlpFileInfo[] GetFiles(string pattern, System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(_path, pattern, searchOption);
        }

        public ZlpFileInfo[] GetFiles(System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(_path, searchOption);
        }

        public ZlpDirectoryInfo[] GetDirectories()
        {
            return ZlpIOHelper.GetDirectories(_path);
        }

        public ZlpDirectoryInfo[] GetDirectories(string pattern)
        {
            return ZlpIOHelper.GetDirectories(_path, pattern);
        }

        public ZlpDirectoryInfo CreateSubdirectory(string name)
        {
            var path = ZlpPathHelper.Combine(_path, name);
            ZlpIOHelper.CreateDirectory(path);
            return new ZlpDirectoryInfo(path);
        }

        public ZlpDirectoryInfo Parent
        {
            get { return new ZlpDirectoryInfo(ZlpPathHelper.GetDirectoryPathNameFromFilePath(_path)); }
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

        public FileAttributes Attributes
        {
            get { return ZlpIOHelper.GetFileAttributes(_path); }
            set { ZlpIOHelper.SetFileAttributes(_path, value); }
        }
    }
}