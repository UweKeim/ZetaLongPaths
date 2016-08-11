namespace ZetaLongPaths
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using FileAttributes = Native.FileAttributes;

    [DebuggerDisplay(@"{FullName}")]
    public class ZlpDirectoryInfo : IZlpFileSystemInfo
    {
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
            FullName = path;
        }

        public void Refresh()
        {
        }

        public bool Exists => ZlpIOHelper.DirectoryExists(FullName);

        public bool IsEmpty => ZlpIOHelper.DirectoryIsEmpty(FullName);

        public void MoveToRecycleBin()
        {
            ZlpIOHelper.MoveDirectoryToRecycleBin(FullName);
        }

        public string OriginalPath => FullName;

        public override string ToString()
        {
            return FullName;
        }

        public void Delete(bool recursive)
        {
            ZlpIOHelper.DeleteDirectory(FullName, recursive);
        }

        public void DeleteContents(bool recursive)
        {
            ZlpIOHelper.DeleteDirectoryContents(FullName, recursive);
        }

        public void Create()
        {
            ZlpIOHelper.CreateDirectory(FullName);
        }

        public void MoveTo(string destinationDirectoryPath)
        {
            ZlpIOHelper.MoveDirectory(FullName, destinationDirectoryPath);
        }

        public void MoveTo(ZlpDirectoryInfo destinationDirectoryPath)
        {
            ZlpIOHelper.MoveDirectory(FullName, destinationDirectoryPath.FullName);
        }

        public string FullName { get; }

        public string Name => ZlpPathHelper.GetDirectoryNameOnlyFromFilePath(FullName);

        public ZlpFileInfo[] GetFiles()
        {
            return ZlpIOHelper.GetFiles(FullName);
        }

        public ZlpFileInfo[] GetFiles(string pattern)
        {
            return ZlpIOHelper.GetFiles(FullName, pattern);
        }

        public ZlpFileInfo[] GetFiles(string pattern, System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(FullName, pattern, searchOption);
        }

        public ZlpFileInfo[] GetFiles(System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(FullName, searchOption);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos()
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos(string pattern)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, pattern);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos(string pattern, System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, pattern, searchOption);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos(System.IO.SearchOption searchOption)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, searchOption);
        }

        public ZlpDirectoryInfo[] GetDirectories()
        {
            return ZlpIOHelper.GetDirectories(FullName);
        }

        public ZlpDirectoryInfo[] GetDirectories(string pattern)
        {
            return ZlpIOHelper.GetDirectories(FullName, pattern);
        }

        public ZlpDirectoryInfo[] GetDirectories(SearchOption searchOption)
        {
            return ZlpIOHelper.GetDirectories(FullName, searchOption);
        }

        public ZlpDirectoryInfo[] GetDirectories(string pattern, SearchOption searchOption)
        {
            return ZlpIOHelper.GetDirectories(FullName, pattern, searchOption);
        }

        public ZlpDirectoryInfo CreateSubdirectory(string name)
        {
            var path = ZlpPathHelper.Combine(FullName, name);
            ZlpIOHelper.CreateDirectory(path);
            return new ZlpDirectoryInfo(path);
        }

        public ZlpDirectoryInfo Parent =>
            new ZlpDirectoryInfo(
                ZlpPathHelper.GetDirectoryPathNameFromFilePath(FullName.TrimEnd(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar)));

        public DateTime LastWriteTime
        {
            get { return ZlpIOHelper.GetFileLastWriteTime(FullName); }
            set { ZlpIOHelper.SetFileLastWriteTime(FullName, value); }
        }

        public DateTime LastAccessTime
        {
            get { return ZlpIOHelper.GetFileLastAccessTime(FullName); }
            set { ZlpIOHelper.SetFileLastAccessTime(FullName, value); }
        }

        public DateTime CreationTime
        {
            get { return ZlpIOHelper.GetFileCreationTime(FullName); }
            set { ZlpIOHelper.SetFileCreationTime(FullName, value); }
        }

        public FileAttributes Attributes
        {
            get { return ZlpIOHelper.GetFileAttributes(FullName); }
            set { ZlpIOHelper.SetFileAttributes(FullName, value); }
        }
    }
}
