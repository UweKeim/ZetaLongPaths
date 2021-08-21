namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using System;
    using System.ComponentModel;

    public class ZlpHandleExceptionInfo
    {
        [UsedImplicitly] public Exception Exception { get; }

        [UsedImplicitly] public int CurrentRetryCount { get; }

        public ZlpHandleExceptionInfo(Exception exception, int currentRetryCount)
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