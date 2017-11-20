namespace ZetaLongPaths
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using JetBrains.Annotations;
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
                        Trace.TraceWarning($@"Error during file operation. ('{info.Info}'): {x.Message}");

                        if (count++ > info.RetryCount)
                        {
                            throw new ZlpSimpleFileAccessProtectorException(
                                string.Format(
                                    info.RetryCount == 1
                                        ? Resources.TriedTooOftenSingular
                                        : Resources.TriedTooOftenPlural, info.RetryCount), x);
                        }
                        else
                        {
                            var p = new HandleExceptionInfo(x, count);
                            info.HandleException?.Invoke(p);

                            if (p.WantThrow)
                            {
                                throw new ZlpSimpleFileAccessProtectorException(
                                    string.Format(
                                        info.RetryCount == 1
                                            ? Resources.TriedTooOftenSingular
                                            : Resources.TriedTooOftenPlural, info.RetryCount), x);
                            }

                            if (info.DoGarbageCollectBeforeSleep)
                            {
                                Trace.TraceInformation(
                                    $@"Error '{x}' during file operation, tried {
                                            count
                                        } times, doing a garbage collect now.");
                                DoGarbageCollect();
                            }

                            Trace.TraceInformation(
                                $@"Error '{x}' during file operation, tried {count} times, sleeping for {
                                        info
                                            .SleepDelaySeconds
                                    } seconds and retry again.");
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

        /// <summary>
        /// Execute an action. On error retry multiple times, sleep between the retries.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static T Protect<T>(
            Func<T> func,
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
                        return func.Invoke();
                    }
                    catch (Exception x)
                    {
                        Trace.TraceWarning($@"Error during file operation. ('{info.Info}'): {x.Message}");

                        if (count++ > info.RetryCount)
                        {
                            throw new ZlpSimpleFileAccessProtectorException(
                                string.Format(
                                    info.RetryCount == 1
                                        ? Resources.TriedTooOftenSingular
                                        : Resources.TriedTooOftenPlural, info.RetryCount), x);
                        }
                        else
                        {
                            var p = new HandleExceptionInfo(x, count);
                            info.HandleException?.Invoke(p);

                            if (p.WantThrow)
                            {
                                throw new ZlpSimpleFileAccessProtectorException(
                                    string.Format(
                                        info.RetryCount == 1
                                            ? Resources.TriedTooOftenSingular
                                            : Resources.TriedTooOftenPlural, info.RetryCount), x);
                            }

                            if (info.DoGarbageCollectBeforeSleep)
                            {
                                Trace.TraceInformation(
                                    $@"Error '{x}' during file operation, tried {
                                            count
                                        } times, doing a garbage collect now.");
                                DoGarbageCollect();
                            }

                            Trace.TraceInformation(
                                $@"Error '{x}' during file operation, tried {count} times, sleeping for {
                                        info
                                            .SleepDelaySeconds
                                    } seconds and retry again.");
                            Thread.Sleep(TimeSpan.FromSeconds(info.SleepDelaySeconds));
                        }
                    }
                }
            }
            else
            {
                return func.Invoke();
            }
        }

        [UsedImplicitly]
        public static void DoGarbageCollect()
        {
            // Central function to do Garbage Collection.

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}