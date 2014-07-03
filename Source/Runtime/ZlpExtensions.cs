namespace ZetaLongPaths
{
    using System.Linq;

    /// <summary>
    /// "Nice to have" extensions.
    /// </summary>
    public static class ZlpExtensions
    {
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

        public static ZlpFileInfo CombineFile(this ZlpDirectoryInfo one,
            ZlpFileInfo two,
            ZlpFileInfo three,
            params ZlpFileInfo[] fours)
        {
            var result = CombineFile(one, two);
            result = CombineFile(result == null ? null : result.FullName, three == null ? null : three.FullName);

            return fours.Aggregate(result,
                (current, four) =>
                    CombineFile(current == null ? null : current.FullName, four == null ? null : four.FullName));
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
            result = CombineFile(result == null ? null : result.FullName, three);

            return fours.Aggregate(result,
                (current, four) =>
                    CombineFile(current == null ? null : current.FullName, four));
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
    }
}