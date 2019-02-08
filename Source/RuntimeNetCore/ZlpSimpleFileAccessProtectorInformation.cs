namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;

    public class ZlpSimpleFileAccessProtectorInformation
    {
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

        [UsedImplicitly] public HandleExceptionDelegate HandleException { get; set; }
    }

    public delegate void HandleExceptionDelegate(HandleExceptionInfo hei);

    public class HandleExceptionInfo
    {
        [UsedImplicitly] public Exception Exception { get; }

        [UsedImplicitly] public int CurrentRetryCount { get; }

        public HandleExceptionInfo(Exception exception, int currentRetryCount)
        {
            Exception = exception;
            CurrentRetryCount = currentRetryCount;
        }

        /// <summary>
        /// Return value. Set optionally to TRUE to force premature throwing.
        /// </summary>
        [DefaultValue(false)]
        [UsedImplicitly]
        public bool WantThrow { get; set; }
    }
}