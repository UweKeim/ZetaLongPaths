namespace ZetaLongPaths
{

    public sealed class ZlpSplittedPath
    {
        private readonly ZlpFileOrDirectoryInfo _info;

        public ZlpSplittedPath(
            string path)
        {
            _info = new ZlpFileOrDirectoryInfo(path);
        }

        public ZlpSplittedPath(
            ZlpFileOrDirectoryInfo path)
        {
            _info = new ZlpFileOrDirectoryInfo(path);
        }

        public string FullPath
        {
            get
            {
                return _info.FullName;
            }
        }

        public ZlpFileOrDirectoryInfo Info
        {
            get
            {
                return _info;
            }
        }

        public string Drive
        {
            get
            {
                return ZlpPathHelper.GetDrive(_info.FullName);
            }
        }

        public string Share
        {
            get
            {
                return ZlpPathHelper.GetShare(_info.FullName);
            }
        }

        public string DriveOrShare
        {
            get
            {
                return ZlpPathHelper.GetDriveOrShare(_info.FullName);
            }
        }

        public string Directory
        {
            get
            {
                return ZlpPathHelper.GetDirectory(_info.FullName);
            }
        }

        public string NameWithoutExtension
        {
            get
            {
                return ZlpPathHelper.GetNameWithoutExtension(_info.FullName);
            }
        }

        public string NameWithExtension
        {
            get
            {
                return ZlpPathHelper.GetNameWithExtension(_info.FullName);
            }
        }

        public string Extension
        {
            get
            {
                return ZlpPathHelper.GetExtension(_info.FullName);
            }
        }

        public string DriveOrShareAndDirectory
        {
            get
            {
                return ZlpPathHelper.Combine(DriveOrShare, Directory);
            }
        }

        public string DriveOrShareAndDirectoryAndNameWithoutExtension
        {
            get
            {
                return
                    ZlpPathHelper.Combine(
                        ZlpPathHelper.Combine(DriveOrShare, Directory),
                        NameWithoutExtension);
            }
        }

        public string DirectoryAndNameWithExtension
        {
            get
            {
                return ZlpPathHelper.Combine(Directory, NameWithExtension);
            }
        }
    }
}