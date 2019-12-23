namespace ZetaLongPaths
{
    using JetBrains.Annotations;
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [UsedImplicitly]
    public class ZlpException : Exception
    {
        public ZlpException()
        {
        }

        public ZlpException(string message) : base(message)
        {
        }

        public ZlpException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ZlpException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}