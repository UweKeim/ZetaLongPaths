// Stephen Toub

namespace ZetaLongPaths.Native.FileOperations
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class ComReleaser<T> : IDisposable where T : class
    {
        public ComReleaser(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (!Marshal.IsComObject(obj)) throw new ArgumentOutOfRangeException(nameof(obj));
            Item = obj;
        }

        public T Item { get; private set; }

        public void Dispose()
        {
            if (Item != null)
            {
                Marshal.FinalReleaseComObject(Item);
                Item = null;
            }
        }
    }
}