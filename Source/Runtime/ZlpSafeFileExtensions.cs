namespace ZetaLongPaths;

public static class ZlpSafeFileExtensions
{
    [UsedImplicitly]
    public static ZlpDirectoryInfo SafeDelete(this ZlpDirectoryInfo folderPath)
    {
        ZlpSafeFileOperations.SafeDeleteDirectory(folderPath);
        return folderPath;
    }

    [UsedImplicitly]
    public static ZlpDirectoryInfo SafeDeleteContents(this ZlpDirectoryInfo folderPath)
    {
        ZlpSafeFileOperations.SafeDeleteDirectoryContents(folderPath);
        return folderPath;
    }

    [UsedImplicitly]
    public static ZlpFileInfo SafeDelete(this ZlpFileInfo filePath)
    {
        ZlpSafeFileOperations.SafeDeleteFile(filePath);
        return filePath;
    }

    [UsedImplicitly]
    public static bool SafeExists(this ZlpDirectoryInfo folderPath)
    {
        return ZlpSafeFileOperations.SafeDirectoryExists(folderPath);
    }

    [UsedImplicitly]
    public static bool SafeExists(this ZlpFileInfo filePath)
    {
        return ZlpSafeFileOperations.SafeFileExists(filePath);
    }

    [UsedImplicitly]
    public static ZlpDirectoryInfo SafeCheckCreate(this ZlpDirectoryInfo folderPath)
    {
        ZlpSafeFileOperations.SafeCheckCreateDirectory(folderPath);
        return folderPath;
    }

    [UsedImplicitly]
    public static ZlpFileInfo SafeMove(this ZlpFileInfo sourcePath, string dstFilePath)
    {
        ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
        return sourcePath;
    }

    [UsedImplicitly]
    public static ZlpFileInfo SafeMove(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath)
    {
        ZlpSafeFileOperations.SafeMoveFile(sourcePath, dstFilePath);
        return sourcePath;
    }

    [UsedImplicitly]
    public static ZlpFileInfo SafeCopy(this ZlpFileInfo sourcePath, string dstFilePath, bool overwrite = true)
    {
        ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
        return sourcePath;
    }

    [UsedImplicitly]
    public static ZlpFileInfo SafeCopy(this ZlpFileInfo sourcePath, ZlpFileInfo dstFilePath, bool overwrite = true)
    {
        ZlpSafeFileOperations.SafeCopyFile(sourcePath, dstFilePath, overwrite);
        return sourcePath;
    }
}