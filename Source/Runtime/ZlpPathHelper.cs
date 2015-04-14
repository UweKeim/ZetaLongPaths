namespace ZetaLongPaths
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using Native;

    public static class ZlpPathHelper
    {
        public static string GetPathRoot(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                path = ZlpIOHelper.ForceRemoveLongPathPrefix(path);

                return GetDriveOrShare(path);
            }
        }

        public static string ChangeExtension(string path, string extension)
        {
            if (path != null)
            {
                string text = path;
                int num = path.Length;

                while (--num >= 0)
                {
                    char c = path[num];
                    if (c == '.')
                    {
                        text = path.Substring(0, num);
                        break;
                    }
                    if (c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar || c == Path.VolumeSeparatorChar)
                    {
                        break;
                    }
                }
                if (extension != null && path.Length != 0)
                {
                    if (extension.Length == 0 || extension[0] != '.')
                    {
                        text += ".";
                    }
                    text += extension;
                }
                return text;
            }
            return null;
        }

        public static string GetFileNameFromFilePath(string filePath)
        {
            if (filePath == null) return null;

            var ls = filePath.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar, Path.VolumeSeparatorChar });
            return ls < 0 ? filePath : filePath.Substring(ls + 1);
        }

        public static string GetFileNameWithoutExtension(string filePath)
        {
            if (filePath == null) return null;

            var fn = GetFileNameFromFilePath(filePath);
            var ls = fn.LastIndexOf('.');

            return ls < 0 ? string.Empty : fn.Substring(0, ls);
        }

        public static string GetDirectoryNameOnlyFromFilePath(string filePath)
        {
            if (filePath == null) return null;

            var ls = filePath.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar/*, Path.VolumeSeparatorChar*/ });

            return ls < 0 ? filePath : filePath.Substring(ls + 1);
        }

        /// <summary>
        /// Returns the full path.
        /// </summary>
        public static string GetDirectoryPathNameFromFilePath(string filePath)
        {
            if (filePath == null) return null;
            //filePath = filePath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            var ls = filePath.LastIndexOfAny(new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar/*, Path.VolumeSeparatorChar*/ });

            if (ls < 0)
            {
                return string.Empty;
            }
            else
            {
                var result = ConvertForwardSlashsToBackSlashs(filePath.Substring(0, ls));

                if (result.EndsWith(@":"))
                {
                    return result + @"\";
                }
                else
                {
                    return result;
                }
            }
        }

        internal static void CheckInvalidPathChars(string path)
        {
            for (int i = 0; i < path.Length; i++)
            {
                var num = (int)path[i];
                if (num == 34 || num == 60 || num == 62 || num == 124 || num < 32)
                {
                    throw new ArgumentException("Invalid paths characters.");
                }
            }
        }

        internal static bool IsDirectorySeparator(char c)
        {
            return c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
        }

        internal static int GetRootLength(string path)
        {
            CheckInvalidPathChars(path);
            int i = 0;
            int length = path.Length;
            if (length >= 1 && IsDirectorySeparator(path[0]))
            {
                i = 1;
                if (length >= 2 && IsDirectorySeparator(path[1]))
                {
                    i = 2;
                    int num = 2;
                    while (i < length)
                    {
                        if ((path[i] == Path.DirectorySeparatorChar || path[i] == Path.AltDirectorySeparatorChar) && --num <= 0)
                        {
                            break;
                        }
                        i++;
                    }
                }
            }
            else
            {
                if (length >= 2 && path[1] == Path.VolumeSeparatorChar)
                {
                    i = 2;
                    if (length >= 3 && IsDirectorySeparator(path[2]))
                    {
                        i++;
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// Makes 'path' an absolute path, based on 'basePath'.
        /// If the given path is already an absolute path, the path
        /// is returned unmodified.
        /// </summary>
        /// <param name="pathToMakeAbsolute">The path to make absolute.</param>
        /// <param name="basePathToWhichToMakeAbsoluteTo">The base path to use when making an
        /// absolute path.</param>
        /// <returns>Returns the absolute path.</returns>
        public static string GetAbsolutePath(
            string pathToMakeAbsolute,
            string basePathToWhichToMakeAbsoluteTo)
        {
            return IsAbsolutePath(pathToMakeAbsolute)
                ? pathToMakeAbsolute
                : GetFullPath(
                    Combine(
                        basePathToWhichToMakeAbsoluteTo,
                        pathToMakeAbsolute));
        }

        /// <summary>
        /// Makes a path relative to another.
        /// (i.e. what to type in a "cd" command to get from
        /// the PATH1 folder to PATH2). works like e.g. developer studio,
        /// when you add a file to a project: there, only the relative
        /// path of the file to the project is stored, too.
        /// e.g.:
        /// path1  = "c:\folder1\folder2\folder4\"
        /// path2  = "c:\folder1\folder2\folder3\file1.txt"
        /// result = "..\folder3\file1.txt"
        /// </summary>
        /// <param name="pathToWhichToMakeRelativeTo">The path to which to make relative to.</param>
        /// <param name="pathToMakeRelative">The path to make relative.</param>
        /// <returns>
        /// Returns the relative path, IF POSSIBLE.
        /// If not possible (i.e. no same parts in PATH2 and the PATH1),
        /// returns the complete PATH2.
        /// </returns>
        public static string GetRelativePath(
            string pathToWhichToMakeRelativeTo,
            string pathToMakeRelative)
        {
            if (string.IsNullOrEmpty(pathToWhichToMakeRelativeTo) ||
                string.IsNullOrEmpty(pathToMakeRelative))
            {
                return pathToMakeRelative;
            }
            else
            {
                var o = pathToWhichToMakeRelativeTo.ToLowerInvariant().Replace('/', '\\').TrimEnd('\\');
                var t = pathToMakeRelative.ToLowerInvariant().Replace('/', '\\');

                // --
                // Handle special cases for Driveletters and UNC shares.

                var td = GetDriveOrShare(t);
                var od = GetDriveOrShare(o);

                td = td.Trim();
                td = td.Trim('\\', '/');

                od = od.Trim();
                od = od.Trim('\\', '/');

                // Different drive or share, i.e. nothing common, skip.
                if (td != od)
                {
                    return pathToMakeRelative;
                }
                else
                {
                    var ol = o.Length;
                    var tl = t.Length;

                    // compare each one, until different.
                    var pos = 0;
                    while (pos < ol && pos < tl && o[pos] == t[pos])
                    {
                        pos++;
                    }
                    if (pos < ol)
                    {
                        pos--;
                    }

                    // after comparison, make normal (i.e. NOT lowercase) again.
                    t = pathToMakeRelative;

                    // --

                    // noting in common.
                    if (pos <= 0)
                    {
                        return t;
                    }
                    else
                    {
                        // If not matching at a slash-boundary, navigate back until slash.
                        if (!(pos == ol || o[pos] == '\\' || o[pos] == '/'))
                        {
                            while (pos > 0 && (o[pos] != '\\' && o[pos] != '/'))
                            {
                                pos--;
                            }
                        }

                        // noting in common.
                        if (pos <= 0)
                        {
                            return t;
                        }
                        else
                        {
                            // --
                            // grab and split the reminders.

                            var oRemaining = o.Substring(pos);
                            oRemaining = oRemaining.Trim('\\', '/');

                            // Count how many folders are following in 'path1'.
                            // Count by splitting.
                            var oRemainingParts = oRemaining.Split('\\');

                            var tRemaining = t.Substring(pos);
                            tRemaining = tRemaining.Trim('\\', '/');

                            // --

                            var result = new StringBuilder();

                            // Path from path1 to common root.
                            foreach (var oRemainingPart in oRemainingParts)
                            {
                                if (!string.IsNullOrEmpty(oRemainingPart))
                                {
                                    result.Append(@"..\");
                                }
                            }

                            // And up to 'path2'.
                            result.Append(tRemaining);

                            // --

                            return result.ToString();
                        }
                    }
                }
            }
        }

        public static string GetFullPath(string path)
        {
            path = ZlpIOHelper.CheckAddLongPathPrefix(path);

            // --
            // Determine length.

            var sb = new StringBuilder();

            var realLength = PInvokeHelper.GetFullPathName(path, 0, sb, IntPtr.Zero);

            // --

            sb.Length = realLength;
            realLength = PInvokeHelper.GetFullPathName(path, sb.Length, sb, IntPtr.Zero);

            if (realLength <= 0)
            {
                var lastWin32Error = Marshal.GetLastWin32Error();
                throw new Win32Exception(
                    lastWin32Error,
                    string.Format(
                        "Error {0} getting full path for '{1}': {2}",
                        lastWin32Error,
                        path,
                        ZlpIOHelper.CheckAddDotEnd(new Win32Exception(lastWin32Error).Message)));
            }
            else
            {
                return sb.ToString();
            }
        }

        public static string GetExtension(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                var splitted = path.Split(
                    Path.DirectorySeparatorChar,
                    Path.AltDirectorySeparatorChar,
                    Path.VolumeSeparatorChar);

                if (splitted.Length > 0)
                {
                    var p = splitted[splitted.Length - 1];

                    var pos = p.LastIndexOf('.');
                    if (pos < 0)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return p.Substring(pos);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static ZlpDirectoryInfo CombineDirectory(
            string path1,
            string path2)
        {
            return new ZlpDirectoryInfo(Combine(path1, path2));
        }

        public static ZlpFileInfo CombineFile(
            string path1,
            string path2)
        {
            return new ZlpFileInfo(Combine(path1, path2));
        }

        public static string Combine(
            string path1,
            string path2)
        {
            if (string.IsNullOrEmpty(path1))
            {
                return path2;
            }
            else if (string.IsNullOrEmpty(path2))
            {
                return path1;
            }
            else
            {
                path1 = path1.TrimEnd('\\', '/').Replace('/', '\\');
                path2 = path2.TrimStart('\\', '/').Replace('/', '\\');

                return path1 + @"\" + path2;
            }
        }

        public static char DirectorySeparatorChar
        {
            get { return Path.DirectorySeparatorChar; }
        }

        public static char AltDirectorySeparatorChar
        {
            get { return Path.AltDirectorySeparatorChar; }
        }

        public static string GetTempDirectoryPath()
        {
            // Simply redirect.
            return Path.GetTempPath();
        }

        public static string GetTempFilePath()
        {
            // Simply redirect.
            return Path.GetTempFileName();
        }

        public static ZlpDirectoryInfo CombineDirectory(
            string path1,
            string path2,
            string path3,
            params string[] paths)
        {
            return new ZlpDirectoryInfo(Combine(path1, path2, path3, paths));
        }

        public static ZlpFileInfo CombineFile(
            string path1,
            string path2,
            string path3,
            params string[] paths)
        {
            return new ZlpFileInfo(Combine(path1, path2, path3, paths));
        }

        public static string Combine(
            string path1,
            string path2,
            string path3,
            params string[] paths)
        {
            var resultPath = Combine(path1, path2);
            resultPath = Combine(resultPath, path3);

            if (paths != null)
            {
                resultPath = paths.Aggregate(resultPath, Combine);
            }

            return resultPath;
        }

        public static string ConvertBackSlashsToForwardSlashs(
            string text)
        {
            return string.IsNullOrEmpty(text) ? text : text.Replace('\\', '/');
        }

        public static string ConvertForwardSlashsToBackSlashs(
            string text)
        {
            return string.IsNullOrEmpty(text) ? text : text.Replace('/', '\\');
        }

        public static string GetDrive(
            string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                path = ConvertForwardSlashsToBackSlashs(path);

                var colonPos = path.IndexOf(':');
                var slashPos = path.IndexOf('\\');

                if (colonPos <= 0)
                {
                    return string.Empty;
                }
                else
                {
                    if (slashPos < 0 || slashPos > colonPos)
                    {
                        return path.Substring(0, colonPos + 1);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the share in a given string.
        /// </summary>
        /// <returns>
        /// Returns the share or an empty string if not found.
        /// </returns>
        /// <remarks>
        /// Example: "\\Server\C\Team\Text\Test.Txt" would return "\\Server\C".
        /// -
        /// Please note_: Searches until the last backslash (including).
        /// If none is present, the share will not be detected. The schema of
        /// a share looks like: "\\Server\Share\Dir1\Dir2\Dir3". The backslash
        /// after "Share" MUST be present to be detected successfully as a share.
        /// </remarks>
        public static string GetShare(
            string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                var str = path;

                // Nach Doppel-Slash suchen.
                // Kann z.B. "\\server\share\" sein,
                // aber auch "http:\\www.xyz.com\".
                const string dblslsh = @"\\";
                var n = str.IndexOf(dblslsh, StringComparison.Ordinal);
                if (n < 0)
                {
                    return string.Empty;
                }
                else
                {
                    // Übernehme links von Doppel-Slash alles in Rückgabe
                    // (inkl. Doppel-Slash selbst).
                    var ret = str.Substring(0, n + dblslsh.Length);
                    str = str.Remove(0, n + dblslsh.Length);

                    // Jetzt nach Slash nach Server-Name suchen.
                    // Dieser Slash darf nicht unmittelbar nach den 2 Anfangsslash stehen.
                    n = str.IndexOf('\\');
                    if (n <= 0)
                    {
                        return string.Empty;
                    }
                    else
                    {
                        // Wiederum übernehmen in Rückgabestring.
                        ret += str.Substring(0, n + 1);
                        str = str.Remove(0, n + 1);

                        // Jetzt nach Slash nach Share-Name suchen.
                        // Dieser Slash darf ebenfalls nicht unmittelbar 
                        // nach dem jetzigen Slash stehen.
                        n = str.IndexOf('\\');
                        if (n < 0)
                        {
                            n = str.Length;
                        }
                        else if (n == 0)
                        {
                            return string.Empty;
                        }

                        // Wiederum übernehmen in Rückgabestring, 
                        // aber ohne letzten Slash.
                        ret += str.Substring(0, n);

                        // The last item must not be a slash.
                        return ret[ret.Length - 1] == '\\' ? string.Empty : ret;
                    }
                }
            }
        }

        public static string GetDriveOrShare(
            string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetDrive(path)))
                {
                    return GetDrive(path);
                }
                else if (!string.IsNullOrEmpty(GetShare(path)))
                {
                    return GetShare(path);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets the drive or share and directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetDriveOrShareAndDirectory(
            string path)
        {
            return string.IsNullOrEmpty(path)
                ? path
                : ZlpPathHelper.GetDirectoryPathNameFromFilePath(path);
        }

        /// <summary>
        /// Retrieves the path part in a given string (without the drive or share).
        /// </summary>
        /// <remarks>
        /// Example: "C:\Team\Text\Test.Txt" would return "\Test\Text\".
        /// -
        /// Please note_: Searches until the last backslash (including).
        /// If not present, the path is not treated as a directory.
        /// (E.g.. "C:\Test\MyDir" would return "\Test" only as the directory).
        /// </remarks>
        public static string GetDirectory(
            string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }
            else
            {
                var driveOrShare = GetDriveOrShare(path);

                var dir = ZlpPathHelper.GetDirectoryPathNameFromFilePath(path);

                // ReSharper disable InvocationIsSkipped
                Debug.Assert(
                    string.IsNullOrEmpty(driveOrShare) ||
                    dir.StartsWith(driveOrShare),
                    string.Format(
                        @"Variable 'dir' ('{0}') must start with drive or share '{1}'.",
                        dir,
                        driveOrShare));
                // ReSharper restore InvocationIsSkipped

                if (!string.IsNullOrEmpty(driveOrShare) &&
                    dir.StartsWith(driveOrShare))
                {
                    return dir.Substring(driveOrShare.Length);
                }
                else
                {
                    return dir;
                }
            }
        }

        /// <summary>
        /// Retrieves the file name without the extension in a given string.
        /// </summary>
        /// <remarks>
        /// Examples:
        /// "C:\Team\Text\Test.Txt" would return "Test".
        /// "C:\Team\Text\Test" would also return "Test".
        /// </remarks>
        public static string GetNameWithoutExtension(
            string path)
        {
            return string.IsNullOrEmpty(path)
                ? path
                : ZlpPathHelper.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// Retrieves the file name with the extension in a given string.
        /// </summary>
        /// <remarks>
        /// Examples:
        /// "C:\Team\Text\Test.Txt" would return "Test.Txt".
        /// "C:\Team\Text\Test" would return "Test".
        /// </remarks>
        public static string GetNameWithExtension(
            ZlpFileInfo path)
        {
            return GetNameWithExtension(path.FullName);
        }

        /// <summary>
        /// Retrieves the file name with the extension in a given string.
        /// </summary>
        /// <remarks>
        /// Examples:
        /// "C:\Team\Text\Test.Txt" would return "Test.Txt".
        /// "C:\Team\Text\Test" would return "Test".
        /// </remarks>
        public static string GetNameWithExtension(
            string path)
        {
            return string.IsNullOrEmpty(path) ? path : ZlpPathHelper.GetFileNameFromFilePath(path);
        }

        public static ZlpSplittedPath SplitPath(
            string path)
        {
            return new ZlpFileOrDirectoryInfo(path).ZlpSplittedPath;
        }

        public static ZlpSplittedPath SplitPath(
            ZlpFileInfo path)
        {
            return new ZlpFileOrDirectoryInfo(path).ZlpSplittedPath;
        }

        public static ZlpSplittedPath SplitPath(
            ZlpDirectoryInfo path)
        {
            return new ZlpFileOrDirectoryInfo(path).ZlpSplittedPath;
        }

        public static ZlpSplittedPath SplitPath(
            ZlpFileOrDirectoryInfo path)
        {
            return path.ZlpSplittedPath;
        }

        public static bool IsDriveLetterPath(
            string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }
            else
            {
                return filePath.IndexOf(':') == 1;
            }
        }

        public static bool IsUncPath(
            string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return false;
            }
            else
            {
                var s = ZlpPathHelper.ConvertForwardSlashsToBackSlashs(filePath);

                if (s.StartsWith(@"\\"))
                {
                    if (s.StartsWith(@"\\?\UNC\"))
                    {
                        return !string.IsNullOrEmpty(ZlpPathHelper.GetShare(filePath));
                    }
                    else if (s.StartsWith(@"\\?\"))
                    {
                        return false;
                    }
                    else
                    {
                        return !string.IsNullOrEmpty(ZlpPathHelper.GetShare(filePath));
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static string SetBackSlashEnd(
            string path,
            bool setSlash)
        {
            return setSlashEnd(path, setSlash, '\\');
        }

        public static string SetForwardSlashEnd(
            string path,
            bool setSlash)
        {
            return setSlashEnd(path, setSlash, '/');
        }

        public static string SetBackSlashBegin(
            string path,
            bool setSlash)
        {
            return setSlashBegin(path, setSlash, '\\');
        }

        public static string SetForwardSlashBegin(
            string path,
            bool setSlash)
        {
            return setSlashBegin(path, setSlash, '/');
        }

        public static string GetParentPath(
            string text)
        {
            return string.IsNullOrEmpty(text)
                ? text
                : ZlpPathHelper.GetFullPath(ZlpPathHelper.Combine(text, @".."));
        }

        /// <summary>
        /// Check whether a given path contains an absolute or relative path.
        /// No disk-access is performed, only the syntax of the given string
        /// is checked.
        /// </summary>
        public static bool IsAbsolutePath(
            string path)
        {
            path = path.Replace('/', '\\');

            if (path.Length < 2)
            {
                return false;
            }
            else if (path.Substring(0, 2) == @"\\")
            {
                // UNC.
                return IsUncPath(path);
            }
            else if (path.Substring(1, 1) == @":")
            {
                // "C:"
                return IsDriveLetterPath(path);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// A "less intelligent" Combine (in contrast to to Path.Combine).
        /// For paths with forward slash.
        /// </summary>
        public static string CombineVirtual(
            string path1,
            string path2)
        {
            if (string.IsNullOrEmpty(path1))
            {
                return path2;
            }
            else if (string.IsNullOrEmpty(path2))
            {
                return path1;
            }
            else
            {
                // Avoid removing too much "/", so that "file://" still
                // stays "file://" and does not become "file:/".
                // (The same applies for other protocols.

                path1 = path1.Replace('\\', '/');
                if (path1[path1.Length - 1] != '/')
                {
                    path1 += @"/";
                }

                path2 = path2.Replace('\\', '/');

                // Do allow "file://" + "/C:/..." to really form "file:///C:/...",
                // with three slashes.
                if (path2.Length >= 3)
                {
                    if (path2[0] == '/' && path2[2] == ':' && char.IsLetter(path2[1]))
                    {
                        // Is OK to have a leading slash.
                    }
                    else
                    {
                        path2 = path2.TrimStart('/', '\\');
                    }
                }
                else
                {
                    path2 = path2.TrimStart('/', '\\');
                }

                return path1 + path2;
            }
        }

        /// <summary>
        /// A "less intelligent" Combine (in contrast to to Path.Combine).
        /// For paths with forward slash.
        /// </summary>
        public static string CombineVirtual(
            string path1,
            string path2,
            string path3,
            params string[] paths)
        {
            var resultPath = CombineVirtual(path1, path2);
            resultPath = CombineVirtual(resultPath, path3);

            if (paths != null)
            {
                resultPath = paths.Aggregate(resultPath, CombineVirtual);
            }

            return resultPath;
        }

        private static string setSlashBegin(
            string path,
            bool setSlash,
            char directorySeparatorChar)
        {
            if (setSlash)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return directorySeparatorChar.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    if (path[0] == directorySeparatorChar)
                    {
                        return path;
                    }
                    else
                    {
                        return directorySeparatorChar + path;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(path))
                {
                    return path;
                }
                else
                {
                    return path[0] == directorySeparatorChar ? path.Substring(1) : path;
                }
            }
        }

        private static string setSlashEnd(
            string path,
            bool setSlash,
            char directorySeparatorChar)
        {
            if (setSlash)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return directorySeparatorChar.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    if (path[path.Length - 1] == directorySeparatorChar)
                    {
                        return path;
                    }
                    else
                    {
                        return path + directorySeparatorChar;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(path))
                {
                    return path;
                }
                else
                {
                    return path[path.Length - 1] == directorySeparatorChar ? path.Substring(0, path.Length - 1) : path;
                }
            }
        }
    }
}