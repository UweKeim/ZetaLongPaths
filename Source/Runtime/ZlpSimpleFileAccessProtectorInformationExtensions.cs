namespace ZetaLongPaths;

[UsedImplicitly]
public static class ZlpSimpleFileAccessProtectorInformationExtensions
{
    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation SetUse(
        this ZlpSimpleFileAccessProtectorInformation @this,
        bool use)
    {
        @this.Use = use;
        return @this;
    }

    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation SetInfo(
        this ZlpSimpleFileAccessProtectorInformation @this,
        string info)
    {
        @this.Info = info;
        return @this;
    }

    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation SetRetryCount(
        this ZlpSimpleFileAccessProtectorInformation @this,
        int retryCount)
    {
        @this.RetryCount = retryCount;
        return @this;
    }

    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation SetSleepDelaySeconds(
        this ZlpSimpleFileAccessProtectorInformation @this,
        int sleepDelaySeconds)
    {
        @this.SleepDelaySeconds = sleepDelaySeconds;
        return @this;
    }

    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation DoGarbageCollectBeforeSleep(
        this ZlpSimpleFileAccessProtectorInformation @this,
        bool doGarbageCollectBeforeSleep)
    {
        @this.DoGarbageCollectBeforeSleep = doGarbageCollectBeforeSleep;
        return @this;
    }

    [UsedImplicitly]
    public static ZlpSimpleFileAccessProtectorInformation SetHandleException(
        this ZlpSimpleFileAccessProtectorInformation @this,
        ZlpHandleExceptionDelegate handleException)
    {
        @this.HandleException = handleException;
        return @this;
    }
}