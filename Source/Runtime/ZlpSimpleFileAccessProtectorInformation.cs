namespace ZetaLongPaths
{
    using JetBrains.Annotations;

    public class ZlpSimpleFileAccessProtectorInformation
    {
        [UsedImplicitly]
        public static ZlpSimpleFileAccessProtectorInformation Default => new();

        [UsedImplicitly]
        public static int DefaultRetryCount =>
            ZlpSimpleFileAccessProtector.GetConfigIntOrDef(@"zlp.sfap.retryCount", 3);

        [UsedImplicitly]
        public static int DefaultSleepDelaySeconds =>
            ZlpSimpleFileAccessProtector.GetConfigIntOrDef(@"zlp.sfap.sleepDelaySeconds", 2);

        [UsedImplicitly] public bool Use { get; set; } = true;

        [UsedImplicitly] public string Info { get; set; }

        [UsedImplicitly] public int RetryCount { get; set; } = DefaultRetryCount;

        [UsedImplicitly] public int SleepDelaySeconds { get; set; } = DefaultSleepDelaySeconds;

        [UsedImplicitly] public bool DoGarbageCollectBeforeSleep { get; set; } = true;

        [UsedImplicitly] public ZlpHandleExceptionDelegate HandleException { get; set; }
    }
}