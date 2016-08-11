namespace ZetaLongPaths
{
    public class ZlpSimpleFileAccessProtectorInformation
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const int DefaultRetryCount = 3;

        // ReSharper disable once MemberCanBePrivate.Global
        public const int DefaultSleepDelaySeconds = 2;

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public bool Use { get; set; } = true;

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string Info { get; set; }

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public int RetryCount { get; set; } = DefaultRetryCount;

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public int SleepDelaySeconds { get; set; } = DefaultSleepDelaySeconds;

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public bool DoGarbageCollectBeforeSleep { get; set; } = true;
    }
}
