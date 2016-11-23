namespace ZetaLongPaths
{
    using System;

    public class ZlpSimpleFileAccessProtectorException :
        Exception
    {
        public ZlpSimpleFileAccessProtectorException()
        {
        }

        public ZlpSimpleFileAccessProtectorException(string message) : base(message)
        {
        }

        public ZlpSimpleFileAccessProtectorException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}