namespace ZetaLongPaths
{

    public sealed class ZlpSplittedPath
    {
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

        public string FullPath => Info.FullName;

        public ZlpFileOrDirectoryInfo Info { get; }

        public string Drive => ZlpPathHelper.GetDrive(Info.FullName);

        public string Share => ZlpPathHelper.GetShare(Info.FullName);

        public string DriveOrShare => ZlpPathHelper.GetDriveOrShare(Info.FullName);

        public string Directory => ZlpPathHelper.GetDirectory(Info.FullName);

        public string NameWithoutExtension => ZlpPathHelper.GetNameWithoutExtension(Info.FullName);

        public string NameWithExtension => ZlpPathHelper.GetNameWithExtension(Info.FullName);

        public string Extension => ZlpPathHelper.GetExtension(Info.FullName);

        public string DriveOrShareAndDirectory => ZlpPathHelper.Combine(DriveOrShare, Directory);

        public string DriveOrShareAndDirectoryAndNameWithoutExtension =>
            ZlpPathHelper.Combine(ZlpPathHelper.Combine(DriveOrShare, Directory), NameWithoutExtension);

        public string DirectoryAndNameWithExtension => ZlpPathHelper.Combine(Directory, NameWithExtension);
    }
}
