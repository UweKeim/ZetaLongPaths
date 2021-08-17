namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using Native;
    using System;

    public interface IZlpFileSystemInfo
    {
        [UsedImplicitly] bool Exists { get; }
        [UsedImplicitly] string OriginalPath { get; }
        [UsedImplicitly] string FullName { get; }
        [UsedImplicitly] string Extension { get; }
        [UsedImplicitly] string Name { get; }
        [UsedImplicitly] DateTime LastWriteTime { get; set; }
        [UsedImplicitly] DateTime LastAccessTime { get; set; }
        [UsedImplicitly] DateTime CreationTime { get; set; }
        [UsedImplicitly] FileAttributes Attributes { get; set; }

        [UsedImplicitly]
        void MoveToRecycleBin();

        [UsedImplicitly]
        string ToString();

        [UsedImplicitly]
        void Refresh();

        [UsedImplicitly]
        void Delete();

        [UsedImplicitly]
        void MoveTo(string destinationDirectoryPath);
    }
}