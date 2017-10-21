namespace ZetaLongPaths
{
    using System;
    using System.Linq;
    using System.Text;
    using JetBrains.Annotations;
#if NETCORE
    using RuntimeNetCore;
#else
    using Properties;

#endif

    /// <summary>
    /// "Nice to have" extensions.
    /// </summary>
    public static class ZlpExtensions
    {
        [UsedImplicitly]
        public static string MakeRelativeTo(
            this ZlpDirectoryInfo pathToMakeRelative,
            ZlpDirectoryInfo pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo.FullName, pathToMakeRelative.FullName);
        }

        [UsedImplicitly]
        public static string MakeRelativeTo(
            this ZlpDirectoryInfo pathToMakeRelative,
            string pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo, pathToMakeRelative.FullName);
        }

        [UsedImplicitly]
        public static string MakeRelativeTo(
            this ZlpFileInfo pathToMakeRelative,
            ZlpDirectoryInfo pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo.FullName, pathToMakeRelative.FullName);
        }

        [UsedImplicitly]
        public static string MakeRelativeTo(
            this ZlpFileInfo pathToMakeRelative,
            string pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo, pathToMakeRelative.FullName);
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CombineDirectory(this ZlpDirectoryInfo one, ZlpDirectoryInfo two)
        {
            if (one == null) return two;
            else if (two == null) return one;

            else return new ZlpDirectoryInfo(ZlpPathHelper.Combine(one.FullName, two.FullName));
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CombineDirectory(this ZlpDirectoryInfo one,
            ZlpDirectoryInfo two,
            ZlpDirectoryInfo three,
            params ZlpDirectoryInfo[] fours)
        {
            var result = CombineDirectory(one, two);
            result = CombineDirectory(result, three);

            result = fours.Aggregate(result, CombineDirectory);
            return result;
        }

        public static ZlpDirectoryInfo CombineDirectory(this ZlpDirectoryInfo one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return one;
            else if (one == null) return new ZlpDirectoryInfo(two);

            else return new ZlpDirectoryInfo(ZlpPathHelper.Combine(one.FullName, two));
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CombineDirectory(this ZlpDirectoryInfo one,
            string two,
            string three,
            params string[] fours)
        {
            var result = CombineDirectory(one, two);
            result = CombineDirectory(result, three);

            result = fours.Aggregate(result, CombineDirectory);
            return result;
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CombineDirectory( /*this*/ string one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return new ZlpDirectoryInfo(one);
            else if (one == null) return new ZlpDirectoryInfo(two);

            else return new ZlpDirectoryInfo(ZlpPathHelper.Combine(one, two));
        }

        [UsedImplicitly]
        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one, ZlpFileInfo two)
        {
            if (one == null) return two;
            else if (two == null) return null;

            else return new ZlpFileInfo(ZlpPathHelper.Combine(one.FullName, two.FullName));
        }

        [UsedImplicitly]
        public static ZlpFileInfo CombineFile(
            this ZlpDirectoryInfo one,
            ZlpFileInfo two,
            ZlpFileInfo three,
            params ZlpFileInfo[] fours)
        {
            var result = CombineFile(one, two);
            result = CombineFile(result?.FullName, three?.FullName);

            return fours.Aggregate(result,
                (current, four) =>
                    CombineFile(current?.FullName, four?.FullName));
        }

        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return null;
            else if (one == null) return new ZlpFileInfo(two);

            else return new ZlpFileInfo(ZlpPathHelper.Combine(one.FullName, two));
        }

        [UsedImplicitly]
        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one, string two, string three,
            params string[] fours)
        {
            var result = CombineFile(one, two);
            result = CombineFile(result?.FullName, three);

            return fours.Aggregate(result,
                (current, four) =>
                    CombineFile(current?.FullName, four));
        }

        [UsedImplicitly]
        public static ZlpFileInfo CombineFile( /*this*/ string one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return null;
            else if (one == null) return new ZlpFileInfo(two);

            else return new ZlpFileInfo(ZlpPathHelper.Combine(one, two));
        }

        /// <summary>
        /// Creates a copy of the calling instance with a changed extension.
        /// This calling instance remains unmodified.
        /// </summary>
        [UsedImplicitly]
        public static ZlpFileInfo ChangeExtension(
            this ZlpFileInfo o,
            string extension)
        {
            return new ZlpFileInfo(ZlpPathHelper.ChangeExtension(o.FullName, extension));
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CreateSubdirectory(this ZlpDirectoryInfo o, string name)
        {
            var path = ZlpPathHelper.Combine(o.FullName, name);
            ZlpIOHelper.CreateDirectory(path);
            return new ZlpDirectoryInfo(path);
        }

        [UsedImplicitly]
        public static bool EqualsNoCase(this ZlpDirectoryInfo o, ZlpDirectoryInfo p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.FullName.TrimEnd('\\', '/'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool EqualsNoCase(this ZlpDirectoryInfo o, string p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.TrimEnd('\\', '/'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool EqualsNoCase(this ZlpFileInfo o, ZlpFileInfo p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.FullName.TrimEnd('\\', '/'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool EqualsNoCase(this ZlpFileInfo o, string p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.TrimEnd('\\', '/'),
                StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static string ReplaceNoCase(this string s1, string s2, string s3)
        {
            if (s1 == null && s2 == null) return null;
            else if (s1 == null || s2 == null) return null;

            else return Replace(s1, s2, s3 ?? string.Empty, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static string Replace(
            this string str,
            string oldValue,
            string newValue,
            StringComparison comparison)
        {
            // http://stackoverflow.com/questions/244531/is-there-an-alternative-to-string-replace-that-is-case-insensitive

            var sb = new StringBuilder();

            var previousIndex = 0;
            var index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        [UsedImplicitly]
        public static int IndexOfNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return 0;
            else if (s1 == null || s2 == null) return -1;
            else return s1.IndexOf(s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int LastIndexOfNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return 0;
            else if (s1 == null || s2 == null) return -1;
            else return s1.LastIndexOf(s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool EqualsNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return true;
            else if (s1 == null || s2 == null) return false;
            else return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool StartsWithNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return true;
            else if (s1 == null || s2 == null) return false;
            else return s1.StartsWith(s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool ContainsNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return true;
            else if (s1 == null || s2 == null) return false;
            else return s1.IndexOf(s2, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        [UsedImplicitly]
        public static bool ContainsNoCase(this string s1, string s2, int startIndex)
        {
            if (s1 == null && s2 == null) return true;
            else if (s1 == null || s2 == null) return false;
            else return s1.IndexOf(s2, startIndex, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        [UsedImplicitly]
        public static bool EndsWithNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return true;
            else if (s1 == null || s2 == null) return false;
            else return s1.EndsWith(s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static int CompareNoCase(this string s1, string s2)
        {
            if (s1 == null && s2 == null) return 0;
            else if (s1 == null) return 1;
            else if (s2 == null) return -1;
            else return string.Compare(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        [UsedImplicitly]
        public static bool EndsWithAnyNoCase(this string s1, params string[] s2)
        {
            if (s1 == null || s2 == null) return false;
            return s2.Any(s22 => !string.IsNullOrEmpty(s22) && s1.EndsWithNoCase(s22));
        }

        [UsedImplicitly]
        public static ZlpFileInfo CheckExists(this ZlpFileInfo file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            if (!file.Exists)
            {
                throw new Exception(string.Format(Resources.FileNotFound, file));
            }

            return file;
        }

        [UsedImplicitly]
        public static ZlpDirectoryInfo CheckExists(this ZlpDirectoryInfo folder)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            if (!folder.Exists)
            {
                throw new Exception(string.Format(Resources.FolderNotFound, folder));
            }

            return folder;
        }

        public static ZlpDirectoryInfo CheckCreate(this ZlpDirectoryInfo folder)
        {
            if (folder == null) throw new ArgumentNullException(nameof(folder));

            if (!folder.Exists) folder.Create();

            return folder;
        }

        [UsedImplicitly]
        public static bool IsSame(this ZlpDirectoryInfo folder1, ZlpDirectoryInfo folder2)
        {
            return IsSame(folder1, folder2?.FullName);
        }

        [UsedImplicitly]
        public static bool IsSame(this ZlpDirectoryInfo folder1, string folder2)
        {
            return ZlpPathHelper.AreSameFolders(folder1.FullName, folder2);
        }

        [UsedImplicitly]
        public static bool StartsWith(this ZlpDirectoryInfo folder1, ZlpDirectoryInfo folder2)
        {
            return StartsWith(folder1, folder2?.FullName);
        }

        [UsedImplicitly]
        public static bool StartsWith(this ZlpDirectoryInfo folder1, string folder2)
        {
            var f1 = folder1?.FullName;

            return !string.IsNullOrEmpty(f1) && !string.IsNullOrEmpty(folder2) &&
                   f1.TrimEnd('\\').ToLowerInvariant().StartsWith(folder2.TrimEnd('\\').ToLowerInvariant());
        }
    }
}