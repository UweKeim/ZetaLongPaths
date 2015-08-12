namespace ZetaLongPaths
{
    using System;
    using System.Linq;
    using Properties;

    /// <summary>
    /// "Nice to have" extensions.
    /// </summary>
    public static class ZlpExtensions
    {
        public static string MakeRelativeTo(
            this ZlpDirectoryInfo pathToMakeRelative,
            ZlpDirectoryInfo pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo.FullName, pathToMakeRelative.FullName);
        }

        public static string MakeRelativeTo(
            this ZlpDirectoryInfo pathToMakeRelative,
            string pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo, pathToMakeRelative.FullName);
        }

        public static string MakeRelativeTo(
            this ZlpFileInfo pathToMakeRelative,
            ZlpDirectoryInfo pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo.FullName, pathToMakeRelative.FullName);
        }

        public static string MakeRelativeTo(
            this ZlpFileInfo pathToMakeRelative,
            string pathToWhichToMakeRelativeTo)
        {
            return ZlpPathHelper.GetRelativePath(pathToWhichToMakeRelativeTo, pathToMakeRelative.FullName);
        }

        public static ZlpDirectoryInfo CombineDirectory(this ZlpDirectoryInfo one, ZlpDirectoryInfo two)
        {
            if (one == null) return two;
            else if (two == null) return one;

            else return new ZlpDirectoryInfo(ZlpPathHelper.Combine(one.FullName, two.FullName));
        }

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

        public static ZlpDirectoryInfo CombineDirectory( /*this*/ string one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return new ZlpDirectoryInfo(one);
            else if (one == null) return new ZlpDirectoryInfo(two);

            else return new ZlpDirectoryInfo(ZlpPathHelper.Combine(one, two));
        }

        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one, ZlpFileInfo two)
        {
            if (one == null) return two;
            else if (two == null) return null;

            else return new ZlpFileInfo(ZlpPathHelper.Combine(one.FullName, two.FullName));
        }

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

        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one, string two, string three, params string[] fours)
        {
            var result = CombineFile(one, two);
            result = CombineFile(result?.FullName, three);

            return fours.Aggregate(result,
                (current, four) =>
                    CombineFile(current?.FullName, four));
        }

        public static ZlpFileInfo CombineFile( /*this*/ string one, string two)
        {
            if (one == null && two == null) return null;
            else if (two == null) return null;
            else if (one == null) return new ZlpFileInfo(two);

            else return new ZlpFileInfo(ZlpPathHelper.Combine(one, two));
        }

        public static ZlpFileInfo ChangeExtension(
            this ZlpFileInfo o,
            string extension)
        {
            return new ZlpFileInfo(ZlpPathHelper.ChangeExtension(o.FullName, extension));
        }

        public static ZlpDirectoryInfo CreateSubdirectory(this ZlpDirectoryInfo o, string name)
        {
            var path = ZlpPathHelper.Combine(o.FullName, name);
            ZlpIOHelper.CreateDirectory(path);
            return new ZlpDirectoryInfo(path);
        }

        public static bool EqualsNoCase(this ZlpDirectoryInfo o, ZlpDirectoryInfo p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.FullName.TrimEnd('\\', '/'),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EqualsNoCase(this ZlpDirectoryInfo o, string p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.TrimEnd('\\', '/'),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EqualsNoCase(this ZlpFileInfo o, ZlpFileInfo p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.FullName.TrimEnd('\\', '/'),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool EqualsNoCase(this ZlpFileInfo o, string p)
        {
            if (o == null && p == null) return true;
            else if (o == null || p == null) return false;

            return string.Equals(
                o.FullName.TrimEnd('\\', '/'),
                p.TrimEnd('\\', '/'),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public static ZlpFileInfo CheckExists(this ZlpFileInfo file)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));

            if (!file.Exists)
            {
                throw new Exception(string.Format(Resources.FileNotFound, file));
            }

            return file;
        }

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

        public static bool IsSame(this ZlpDirectoryInfo folder1, ZlpDirectoryInfo folder2)
        {
            return IsSame(folder1, folder2?.FullName);
        }

        public static bool IsSame(this ZlpDirectoryInfo folder1, string folder2)
        {
            return ZlpPathHelper.AreSameFolders(folder1.FullName, folder2);
        }

        public static bool StartsWith(this ZlpDirectoryInfo folder1, ZlpDirectoryInfo folder2)
        {
            return StartsWith(folder1, folder2?.FullName);
        }

        public static bool StartsWith(this ZlpDirectoryInfo folder1, string folder2)
        {
            var f1 = folder1?.FullName;

            return !string.IsNullOrEmpty(f1) && !string.IsNullOrEmpty(folder2) &&
                   f1.TrimEnd('\\').ToLowerInvariant().StartsWith(folder2.TrimEnd('\\').ToLowerInvariant());
        }
    }
}