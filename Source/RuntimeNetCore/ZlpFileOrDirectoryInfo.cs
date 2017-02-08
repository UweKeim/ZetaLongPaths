namespace ZetaLongPaths
{
    using System;

    /// <summary>
    /// Wraps a <see cref="ZlpFileInfo"/> and a <see cref="ZlpDirectoryInfo"/> in one class.
    /// </summary>
    public sealed class ZlpFileOrDirectoryInfo
    {
        public enum PreferedType
        {
            Unspecified,
            File,
            Directory
        }

        private readonly string _fullPath;
        private readonly string _originalPath;

        private PreferedType _preferedType;

        public ZlpFileOrDirectoryInfo()
        {
            _preferedType = PreferedType.Unspecified;
        }

        public ZlpFileOrDirectoryInfo(
            string fullPath)
        {
            _preferedType = PreferedType.Unspecified;
            _fullPath = fullPath;
            _originalPath = fullPath;
        }

        public ZlpFileOrDirectoryInfo(
            string fullPath,
            bool detectTypeFromFileSystem)
        {
            _fullPath = fullPath;
            _originalPath = fullPath;

            if (detectTypeFromFileSystem)
            {
                _preferedType =
                    IsFile
                        ? PreferedType.File
                        : IsDirectory
                            ? PreferedType.Directory
                            : PreferedType.Unspecified;
            }
            else
            {
                _preferedType = PreferedType.Unspecified;
            }
        }

        public ZlpFileOrDirectoryInfo(
            string fullPath,
            PreferedType preferedType)
        {
            _preferedType = preferedType;
            _fullPath = fullPath;
            _originalPath = fullPath;
        }

        public ZlpFileOrDirectoryInfo(
            ZlpFileOrDirectoryInfo info)
        {
            _preferedType = info._preferedType;
            _fullPath = info.FullName;
            _originalPath = info._originalPath;
        }

        public ZlpFileOrDirectoryInfo(
            // ReSharper disable SuggestBaseTypeForParameter
            ZlpFileInfo info)
        // ReSharper restore SuggestBaseTypeForParameter
        {
            _preferedType = PreferedType.File;
            _fullPath = info.FullName;
            _originalPath = info.ToString();
        }

        public ZlpFileOrDirectoryInfo(
            // ReSharper disable SuggestBaseTypeForParameter
            ZlpDirectoryInfo info)
        // ReSharper restore SuggestBaseTypeForParameter
        {
            _preferedType = PreferedType.Directory;
            _fullPath = info.FullName;
            _originalPath = info.ToString();
        }

        public ZlpFileOrDirectoryInfo Clone()
        {
            return new ZlpFileOrDirectoryInfo(this);
        }

        public bool IsEmpty => string.IsNullOrEmpty(_fullPath);

        public ZlpFileInfo File => new ZlpFileInfo(_fullPath);

        public ZlpDirectoryInfo Directory => new ZlpDirectoryInfo(_fullPath);

        public ZlpDirectoryInfo EffectiveDirectory
        {
            get
            {
                switch (_preferedType)
                {
                    case PreferedType.File:
                        return File.Directory;

                    case PreferedType.Unspecified:
                        if (ZlpSafeFileOperations.SafeDirectoryExists(Directory))
                            return Directory;
                        else if (ZlpSafeFileOperations.SafeFileExists(File))
                            return File.Directory;
                        else
                            return Directory;

                    case PreferedType.Directory:
                        return Directory;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Get a value indicating whether the file or the directory exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                if (string.IsNullOrEmpty(_fullPath))
                {
                    return false;
                }
                else
                {
                    switch (_preferedType)
                    {
                        default:
                            return Directory.Exists || File.Exists;
                        case PreferedType.File:
                            return File.Exists || Directory.Exists;
                    }
                }
            }
        }

        public string FullName => _fullPath;

        public string OriginalPath => _originalPath;

        public ZlpSplittedPath ZlpSplittedPath => new ZlpSplittedPath(this);

        public string Name => IsFile ? File?.Name : Directory?.Name;

        /// <summary>
        /// Gets a value indicating whether this instance is file by querying the file system
        /// whether the file exists on disk.
        /// </summary>
        public bool IsFile => File.Exists;

        /// <summary>
        /// Gets a value indicating whether this instance is directory by quering the file system
        /// whether the directory exists on disk.
        /// </summary>
        public bool IsDirectory => Directory.Exists;

        public static int Compare(
            ZlpDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        public static int Compare(
            ZlpFileInfo one,
            ZlpFileInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpFileOrDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpFileInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'), StringComparison.OrdinalIgnoreCase);
        }

        public static int Compare(
            string one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            string one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            string one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            ZlpDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            ZlpFileInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public static int Compare(
            string one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        public int Compare(
            string b)
        {
            return Compare(this, new ZlpFileOrDirectoryInfo(b));
        }

        public int Compare(
            ZlpFileInfo b)
        {
            return Compare(b.FullName);
        }

        public int Compare(
            ZlpDirectoryInfo b)
        {
            return Compare(b.FullName);
        }

        public int Compare(
            ZlpFileOrDirectoryInfo b)
        {
            return Compare(b.FullName);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileInfo one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        public ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo info)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        info.FullName));
        }

        public ZlpFileOrDirectoryInfo Combine(
            string path)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        path));
        }

        public ZlpFileOrDirectoryInfo Combine(
            ZlpFileInfo info)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        // According to Reflector, "ToString()" returns the 
                        // "OriginalPath". This is what we need here.
                        info.ToString()));
        }

        public ZlpFileOrDirectoryInfo Combine(
            ZlpDirectoryInfo info)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        // According to Reflector, "ToString()" returns the 
                        // "OriginalPath". This is what we need here.
                        info.ToString()));
        }

        /// <summary>
        /// Schaut ins Dateisystem, wenn der Typ "unspecified" ist und versucht den
        /// korreten Typ festzustellen.
        /// </summary>
        public void LookupType()
        {
            if (_preferedType == PreferedType.Unspecified)
            {
                _preferedType =
                    IsFile
                        ? PreferedType.File
                        : IsDirectory
                            ? PreferedType.Directory
                            : PreferedType.Unspecified;
            }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(_originalPath)
                ? _fullPath
                : _originalPath;
        }
    }
}
