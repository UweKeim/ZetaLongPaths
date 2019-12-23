namespace ZetaLongPaths
{
    using JetBrains.Annotations;

    public sealed class ZlpSplittedPath
    {
        [UsedImplicitly]
        public ZlpSplittedPath(
            string path)
        {
            Info = new ZlpFileOrDirectoryInfo(path);
        }

        public ZlpSplittedPath(
            ZlpFileOrDirectoryInfo path)
        {
            Info = new ZlpFileOrDirectoryInfo(path);
        }

        [UsedImplicitly]
        public string FullPath => Info.FullName;

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo Info { get; }

        [UsedImplicitly]
        public string Drive => ZlpPathHelper.GetDrive(Info.FullName);

        [UsedImplicitly]
        public string Share => ZlpPathHelper.GetShare(Info.FullName);

        [UsedImplicitly]
        public string DriveOrShare => ZlpPathHelper.GetDriveOrShare(Info.FullName);

        [UsedImplicitly]
        public string Directory => ZlpPathHelper.GetDirectory(Info.FullName);

        [UsedImplicitly]
        public string NameWithoutExtension => ZlpPathHelper.GetNameWithoutExtension(Info.FullName);

        [UsedImplicitly]
        public string NameWithExtension => ZlpPathHelper.GetNameWithExtension(Info.FullName);

        [UsedImplicitly]
        public string Extension => ZlpPathHelper.GetExtension(Info.FullName);

        [UsedImplicitly]
        public string DriveOrShareAndDirectory => ZlpPathHelper.Combine(DriveOrShare, Directory);

        [UsedImplicitly]
        public string DriveOrShareAndDirectoryAndNameWithoutExtension =>
            ZlpPathHelper.Combine(ZlpPathHelper.Combine(DriveOrShare, Directory), NameWithoutExtension);

        [UsedImplicitly]
        public string DirectoryAndNameWithExtension => ZlpPathHelper.Combine(Directory, NameWithExtension);
    }
}