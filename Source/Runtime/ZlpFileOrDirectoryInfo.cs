namespace ZetaLongPaths
{
    using JetBrains.Annotations;
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

        private PreferedType _preferedType;

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo()
        {
            _preferedType = PreferedType.Unspecified;
        }

        public ZlpFileOrDirectoryInfo(
            string fullPath)
        {
            _preferedType = PreferedType.Unspecified;
            FullName = fullPath;
            OriginalPath = fullPath;
        }

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo(
            string fullPath,
            bool detectTypeFromFileSystem)
        {
            FullName = fullPath;
            OriginalPath = fullPath;

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
            FullName = fullPath;
            OriginalPath = fullPath;
        }

        public ZlpFileOrDirectoryInfo(
            ZlpFileOrDirectoryInfo info)
        {
            _preferedType = info._preferedType;
            FullName = info.FullName;
            OriginalPath = info.OriginalPath;
        }

        public ZlpFileOrDirectoryInfo(
                // ReSharper disable SuggestBaseTypeForParameter
                ZlpFileInfo info)
        // ReSharper restore SuggestBaseTypeForParameter
        {
            _preferedType = PreferedType.File;
            FullName = info.FullName;
            OriginalPath = info.ToString();
        }

        public ZlpFileOrDirectoryInfo(
                // ReSharper disable SuggestBaseTypeForParameter
                ZlpDirectoryInfo info)
        // ReSharper restore SuggestBaseTypeForParameter
        {
            _preferedType = PreferedType.Directory;
            FullName = info.FullName;
            OriginalPath = info.ToString();
        }

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo Clone()
        {
            return new ZlpFileOrDirectoryInfo(this);
        }

        public bool IsEmpty => string.IsNullOrEmpty(FullName);

        public ZlpFileInfo File => new ZlpFileInfo(FullName);

        public ZlpDirectoryInfo Directory => new ZlpDirectoryInfo(FullName);

        [UsedImplicitly]
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
        [UsedImplicitly]
        public bool Exists
        {
            get
            {
                if (string.IsNullOrEmpty(FullName))
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

        public string FullName { get; }

        [UsedImplicitly]
        public string OriginalPath { get; }

        public ZlpSplittedPath ZlpSplittedPath => new ZlpSplittedPath(this);

        [UsedImplicitly]
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

        [UsedImplicitly]
        public static int Compare(
            ZlpDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileInfo one,
            ZlpFileInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpFileOrDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpFileInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return string.Compare(one.FullName.TrimEnd('\\'), two.FullName.TrimEnd('\\'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int Compare(
            string one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            string one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            string one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            ZlpFileOrDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public static int Compare(
            string one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Compare(two);
        }

        [UsedImplicitly]
        public int Compare(
            string b)
        {
            return Compare(this, new ZlpFileOrDirectoryInfo(b));
        }

        [UsedImplicitly]
        public int Compare(
            ZlpFileInfo b)
        {
            return Compare(b.FullName);
        }

        [UsedImplicitly]
        public int Compare(
            ZlpDirectoryInfo b)
        {
            return Compare(b.FullName);
        }

        [UsedImplicitly]
        public int Compare(
            ZlpFileOrDirectoryInfo b)
        {
            return Compare(b.FullName);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileInfo one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpFileInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            ZlpFileOrDirectoryInfo two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public static ZlpFileOrDirectoryInfo Combine(
            string one,
            string two)
        {
            return new ZlpFileOrDirectoryInfo(one).Combine(two);
        }

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo Combine(
            ZlpFileOrDirectoryInfo info)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        info.FullName));
        }

        [UsedImplicitly]
        public ZlpFileOrDirectoryInfo Combine(
            string path)
        {
            return
                new ZlpFileOrDirectoryInfo(
                    ZlpPathHelper.Combine(
                        EffectiveDirectory.FullName,
                        path));
        }

        [UsedImplicitly]
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

        [UsedImplicitly]
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
        [UsedImplicitly]
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
            return string.IsNullOrEmpty(OriginalPath)
                ? FullName
                : OriginalPath;
        }
    }
}