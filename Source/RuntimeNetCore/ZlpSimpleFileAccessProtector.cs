namespace ZetaLongPaths
{
    using System;
    using System.Diagnostics;
    using System.Threading;

#if NETCORE
    using RuntimeNetCore;
#else
    using Properties;
#endif

    /// <summary>
    /// Execute an action. On error retry multiple times, sleep between the retries.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public static class ZlpSimpleFileAccessProtector
    {
        /// <summary>
        /// Execute an action. On error retry multiple times, sleep between the retries.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static void Protect(
            Action action,
            ZlpSimpleFileAccessProtectorInformation info = null)
        {
            info = info ?? new ZlpSimpleFileAccessProtectorInformation();

            if (info.Use)
            {
                var count = 0;
                while (true)
                {
                    try
                    {
                        action?.Invoke();
                        return;
                    }
                    catch (Exception x)
                    {
                        Trace.TraceWarning($@"Error during file operation. ('{info.Info}').", x);

                        if (count++ > info.RetryCount)
                        {
                            throw new Exception(string.Format(Resources.TriedTooOften, info.RetryCount), x);
                        }
                        else
                        {
                            if (info.DoGarbageCollectBeforeSleep)
                            {
                                Trace.TraceInformation(
                                    $@"Error '{x}' during file operation, tried {count} times, doing a garbage collect now.");
                                GC.Collect();
                            }

                            Trace.TraceInformation(
                                $@"Error '{x}' during file operation, tried {count} times, sleeping for {info
                                    .SleepDelaySeconds} seconds and retry again.");
                            Thread.Sleep(TimeSpan.FromSeconds(info.SleepDelaySeconds));
                        }
                    }
                }
            }
            else
            {
                action?.Invoke();
            }
        }
    }
}