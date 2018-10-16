namespace ZetaLongPaths.Tools
{
    using JetBrains.Annotations;
    using Properties;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;

    /// <inheritdoc />
    /// <summary>
    /// Simple helper class for accessing files and folders on an UNC network location
    /// with another user than the current thread's user.
    /// It is basically a wrapper for the "NET USE" functionality.
    /// </summary>
    /// <remarks>
    /// See also https://stackoverflow.com/a/1197430/107625
    /// </remarks>
    [UsedImplicitly]
    public class ZlpNetworkConnection : IDisposable
    {
        private string _networkName;

        public ZlpNetworkConnection(
            string networkName,
            bool activate,
            string userName,
            string password)
        {
            if (activate)
            {
                doNetUse(networkName, userName, password, null);
            }
        }

        protected ZlpNetworkConnection(
            string networkName,
            bool activate,
            string userName,
            string password,
            NetResource netResource)
        {
            if (activate)
            {
                doNetUse(networkName, userName, password, netResource);
            }
        }

        private void doNetUse(string networkName, string userName, string password, NetResource netResource)
        {
            netResource = netResource ?? new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkName
            };

            var result = WNetAddConnection2(
                netResource,
                password,
                userName,
                0);

            if (result != 0)
            {
                throw new IOException(string.Format(Resources.ErrorConnectingToRemoteShare, networkName, result),
                    result);
            }

            _networkName = networkName;
        }

        ~ZlpNetworkConnection()
        {
            doDispose();
        }

        public void Dispose()
        {
            doDispose();
            GC.SuppressFinalize(this);
        }

        private void doDispose()
        {
            if (!string.IsNullOrEmpty(_networkName))
            {
                var result = WNetCancelConnection2(_networkName, 0, true);
                Trace.TraceInformation("Result for canceling network connection: {0}.", result);
            }
        }

        [DllImport(@"mpr.dll")]
        private static extern int WNetAddConnection2(
            NetResource netResource,
            string password,
            string username,
            int flags);

        [DllImport(@"mpr.dll")]
        private static extern int WNetCancelConnection2(
            string name,
            int flags,
            bool force);

        [StructLayout(LayoutKind.Sequential)]
        [UsedImplicitly]
        protected internal class NetResource
        {
#pragma warning disable 414
#pragma warning disable 169
            // ReSharper disable NotAccessedField.Global
            // ReSharper disable UnusedMember.Global
            public ResourceScope Scope;
            public ResourceType ResourceType;
            public ResourceDisplaytype DisplayType;
            public int Usage;
            public string LocalName;
            public string RemoteName;
            public string Comment;
            public string Provider;
            // ReSharper restore UnusedMember.Global
            // ReSharper restore NotAccessedField.Global
#pragma warning restore 169
#pragma warning restore 414
        }

        [UsedImplicitly]
        protected internal enum ResourceScope
        {
#pragma warning disable 414
#pragma warning disable 169
            // ReSharper disable NotAccessedField.Global
            // ReSharper disable UnusedMember.Global
            Connected = 1,
            GlobalNetwork,
            Remembered,
            Recent,
            Context
            // ReSharper restore UnusedMember.Global
            // ReSharper restore NotAccessedField.Global
#pragma warning restore 169
#pragma warning restore 414
        };

        [UsedImplicitly]
        protected internal enum ResourceType
        {
#pragma warning disable 414
#pragma warning disable 169
            // ReSharper disable NotAccessedField.Global
            // ReSharper disable UnusedMember.Global
            Any = 0,
            Disk = 1,
            Print = 2,
            Reserved = 8,
            // ReSharper restore UnusedMember.Global
            // ReSharper restore NotAccessedField.Global
#pragma warning restore 169
#pragma warning restore 414
        }

        [UsedImplicitly]
        protected internal enum ResourceDisplaytype
        {
#pragma warning disable 414
#pragma warning disable 169
            // ReSharper disable NotAccessedField.Global
            // ReSharper disable UnusedMember.Global
            Generic = 0x0,
            Domain = 0x01,
            Server = 0x02,
            Share = 0x03,
            File = 0x04,
            Group = 0x05,
            Network = 0x06,
            Root = 0x07,
            Shareadmin = 0x08,
            Directory = 0x09,
            Tree = 0x0a,
            Ndscontainer = 0x0b
            // ReSharper restore UnusedMember.Global
            // ReSharper restore NotAccessedField.Global
#pragma warning restore 169
#pragma warning restore 414
        }
    }
}