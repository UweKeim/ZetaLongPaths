namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using System;
    using System.Diagnostics;
    using System.IO;
    using FileAttributes = Native.FileAttributes;

    // ReSharper disable once UseNameofExpression
    [DebuggerDisplay(@"{FullName}")]
    public class ZlpDirectoryInfo : IZlpFileSystemInfo
    {
        public static ZlpDirectoryInfo GetTemp()
        {
            return new ZlpDirectoryInfo(ZlpPathHelper.GetTempDirectoryPath());
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo GetFolderPath(
            Environment.SpecialFolder specialFolder)
        {
            return new ZlpDirectoryInfo(Environment.GetFolderPath(specialFolder));
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo GetFolderPath(
            Environment.SpecialFolder specialFolder,
            Environment.SpecialFolderOption option)
        {
            return new ZlpDirectoryInfo(Environment.GetFolderPath(specialFolder, option));
        }

        public ZlpDirectoryInfo(string path)
        {
            FullName = path;
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo(DirectoryInfo path)
        {
            FullName = path?.FullName;
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo(ZlpDirectoryInfo path)
        {
            FullName = path?.FullName;
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo FromOther(ZlpDirectoryInfo path)
        {
            return new ZlpDirectoryInfo(path);
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo FromString(string path)
        {
            return new ZlpDirectoryInfo(path);
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo FromBuiltIn(DirectoryInfo path)
        {
            return new ZlpDirectoryInfo(path);
        }

        [UsedImplicitly]
        public DirectoryInfo ToBuiltIn()
        {
            return new DirectoryInfo(FullName);
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo ToOther()
        {
            return Clone();
        }

        [UsedImplicitly]
        public void Refresh()
        {
        }

        public string Extension => ZlpPathHelper.GetExtension(FullName);

        [UsedImplicitly]
        public ZlpDirectoryInfo Clone()
        {
            return new ZlpDirectoryInfo(FullName);
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo GetFullPath()
        {
            return new ZlpDirectoryInfo(ZlpPathHelper.GetFullPath(FullName));
        }

        public bool Exists => ZlpIOHelper.DirectoryExists(FullName);

        [UsedImplicitly] public bool IsEmpty => ZlpIOHelper.DirectoryIsEmpty(FullName);

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

        [UsedImplicitly]
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

        /// <summary>
        /// The destination folder may not exists yet, otherwise an error 183 will be thrown.
        /// </summary>
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

        [UsedImplicitly]
        public ZlpFileInfo[] GetFiles(string pattern)
        {
            return ZlpIOHelper.GetFiles(FullName, pattern);
        }

        [UsedImplicitly]
        public ZlpFileInfo[] GetFiles(string pattern, SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(FullName, pattern, searchOption);
        }

        [UsedImplicitly]
        public ZlpFileInfo[] GetFiles(SearchOption searchOption)
        {
            return ZlpIOHelper.GetFiles(FullName, searchOption);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos()
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName);
        }

        [UsedImplicitly]
        public IZlpFileSystemInfo[] GetFileSystemInfos(string pattern)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, pattern);
        }

        [UsedImplicitly]
        public IZlpFileSystemInfo[] GetFileSystemInfos(string pattern, SearchOption searchOption)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, pattern, searchOption);
        }

        public IZlpFileSystemInfo[] GetFileSystemInfos(SearchOption searchOption)
        {
            return ZlpIOHelper.GetFileSystemInfos(FullName, searchOption);
        }

        public ZlpDirectoryInfo[] GetDirectories()
        {
            return ZlpIOHelper.GetDirectories(FullName);
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo[] GetDirectories(string pattern)
        {
            return ZlpIOHelper.GetDirectories(FullName, pattern);
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo[] GetDirectories(SearchOption searchOption)
        {
            return ZlpIOHelper.GetDirectories(FullName, searchOption);
        }

        [UsedImplicitly]
        public ZlpDirectoryInfo[] GetDirectories(string pattern, SearchOption searchOption)
        {
            return ZlpIOHelper.GetDirectories(FullName, pattern, searchOption);
        }

        [UsedImplicitly]
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

        public ZlpFileDateInfos DateInfos
        {
            get => ZlpIOHelper.GetFileDateInfos(FullName);
            set => ZlpIOHelper.SetFileDateInfos(FullName, value);
        }

        public FileAttributes Attributes
        {
            get => ZlpIOHelper.GetFileAttributes(FullName);
            set => ZlpIOHelper.SetFileAttributes(FullName, value);
        }
    }
}