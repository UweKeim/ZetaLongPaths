namespace ZetaLongPaths
{
    public static class ZlpSafeFileExtensions
    {
        public static ZlpDirectoryInfo SafeDelete(this ZlpDirectoryInfo folderPath)
        {
            ZlpSafeFileOperations.SafeDeleteDirectory(folderPath);
            return folderPath;
        }

        public static ZlpDirectoryInfo SafeDeleteContents(this ZlpDirectoryInfo folderPath)
        {
            ZlpSafeFileOperations.SafeDeleteDirectoryContents(folderPath);
            return folderPath;
        }

        public static ZlpFileInfo SafeDelete(this ZlpFileInfo filePath)
        {
            ZlpSafeFileOperations.SafeDeleteFile(filePath);
            return filePath;
        }

        public static bool SafeExists(this ZlpDirectoryInfo folderPath)
        {
            return ZlpSafeFileOperations.SafeDirectoryExists(folderPath);
        }

        public static bool SafeExists(this ZlpFileInfo filePath)
        {
            return ZlpSafeFileOperations.SafeFileExists(filePath);
        }

        public static void SafeCheckCreate(this ZlpDirectoryInfo folderPath)
        {
            ZlpSafeFileOperations.SafeCheckCreateDirectory(folderPath);
        }

        public static ZlpFileInfo SafeMove(this ZlpFileInfo sourcePath, string dstFilePath)
        {
            ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
            return sourcePath;
        }

        public static ZlpFileInfo SafeMove(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath)
        {
            ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
            return sourcePath;
        }

        public static ZlpFileInfo SafeCopy(this ZlpFileInfo sourcePath, string dstFilePath, bool overwrite = true)
        {
            ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
            return sourcePath;
        }

        public static ZlpFileInfo SafeCopy(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath, bool overwrite = true)
        {
            ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
            return sourcePath;
        }
    }
}