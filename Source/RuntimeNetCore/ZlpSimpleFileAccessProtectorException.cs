namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using System;

    public class ZlpSimpleFileAccessProtectorException :
        Exception
    {
        [UsedImplicitly]
        public ZlpSimpleFileAccessProtectorException()
        {
        }

        [UsedImplicitly]
        public ZlpSimpleFileAccessProtectorException(string message) : base(message)
        {
        }

        [UsedImplicitly]
        public ZlpSimpleFileAccessProtectorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}