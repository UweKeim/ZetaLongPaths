namespace ZetaLongPaths
{
    public static class ZlpSafeFileExtensions
    {
        public static void SafeDelete(this ZlpDirectoryInfo folderPath)
        {
            ZlpSafeFileOperations.SafeDeleteDirectory(folderPath);
        }

        public static void SafeDeleteContents(this ZlpDirectoryInfo folderPath)
        {
            ZlpSafeFileOperations.SafeDeleteDirectoryContents(folderPath);
        }

        public static void SafeDelete(this ZlpFileInfo filePath)
        {
            ZlpSafeFileOperations.SafeDeleteFile(filePath);
        }

        public static bool SafeExists(this ZlpDirectoryInfo folderPath)
        {
            return ZlpSafeFileOperations.SafeDirectoryExists(folderPath);
        }

        public static bool SafeExists(this ZlpFileInfo filePath)
        {
            return ZlpSafeFileOperations.SafeFileExists(filePath);
        }

        public static void SafeMove(this ZlpFileInfo sourcePath, string dstFilePath)
        {
            ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
        }

        public static void SafeMove(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath)
        {
            ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
        }

        public static void SafeCopy(this ZlpFileInfo sourcePath, string dstFilePath, bool overwrite = true)
        {
            ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
        }

        public static void SafeCopy(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath, bool overwrite = true)
        {
            ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
        }
    }
}