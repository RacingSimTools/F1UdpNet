// <copyright file="F1UdpClient.cs" company="Racing Sim Tools">
// Original work Copyright (c) Codemasters. All rights reserved.
//
// Modified work Copyright (c) Racing Sim Tools.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace F1UdpNet
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.MemoryMappedFiles;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Reactive;
    using System.Reactive.Concurrency;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Reactive.Threading;
    using System.Reactive.Threading.Tasks;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Client class used to connect and read packets from F1 2018
    /// </summary>
    public static class F1UdpClient
    {
        /// <summary>
        /// Gets the observable stream of packets.
        /// </summary>
        public static IObservable<IF1Packet> CarInfoStream { get; private set; }

        /// <summary>
        /// Starts reading into the <see cref="CarInfoStream"/> observable.
        /// </summary>
        /// <param name="udpClient">The udpClient to read from</param>
        /// <returns><see cref="CarInfoStream"/> observable</returns>
        public static IObservable<IF1Packet> StartRead(UdpClient udpClient)
        {
           CarInfoStream = Observable.FromAsync(udpClient.ReceiveAsync)
                .Repeat()
                .Publish()
                .RefCount()
                .Select(p => MarshalPacket(p))
                .DistinctUntilChanged(new PacketTimeEquality());
            return CarInfoStream;
        }

        private static IF1Packet MarshalPacket(UdpReceiveResult result)
        {
            byte[] arr = result.Buffer;

            //PacketHeader header = MarshalType<PacketHeader>(arr);

            switch (arr.Length)
            {
                case 1341:
                    return MarshalType<PacketMotionData>(arr);
                case 147:
                    return MarshalType<PacketSessionData>(arr);
                case 841:
                    if (arr[4] == 2)
                    {
                        return MarshalType<PacketCarSetupData>(arr);
                    }
                    else
                    {
                        return MarshalType<PacketLapData>(arr);
                    }
                case 25:
                    return MarshalType<PacketEventData>(arr);
                case 1082:
                    return MarshalType<PacketParticipantsData>(arr);
                case 1085:
                    return MarshalType<PacketCarTelemetryData>(arr);
                case 1061:
                    return MarshalType<PacketCarStatusData>(arr);
                default:
                    return null;
            }
        }

        private static T MarshalType<T>(byte[] arr)
        {
            T str = default(T);

            int size = Marshal.SizeOf(str);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(arr, 0, ptr, size);

            str = (T)Marshal.PtrToStructure(ptr, str.GetType());
            Marshal.FreeHGlobal(ptr);

            return str;
        }
    }
}
