namespace ZetaLongPaths
{
    using System;
    using Native;

    public interface IZlpFileSystemInfo
    {
        bool Exists { get; }
        string OriginalPath { get; }
        string FullName { get; }
        string Name { get; }
        DateTime LastWriteTime { get; set; }
        DateTime LastAccessTime { get; set; }
        DateTime CreationTime { get; set; }
        FileAttributes Attributes { get; set; }

        void MoveToRecycleBin();
        string ToString();
        void MoveTo(string destinationDirectoryPath);
    }
}