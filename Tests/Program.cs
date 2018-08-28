// <copyright file="Program.cs" company="Racing Sim Tools">
// Original work Copyright (c) Codemasters. All rights reserved.
//
// Modified work Copyright (c) Racing Sim Tools.
//
// Licensed under the MIT license.
// See LICENSE file in the project root for full license information.
// </copyright>

namespace Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Reactive;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using F1UdpNet;

    public static class Program
    {
        public static void Main(string[] args)
        {
            IObserver<IF1Packet> observer = Observer
                .Create<IF1Packet>(output =>
                {
                    Console.WriteLine(output.Header.m_packetId);
                    if ((e_PacketId)output.Header.m_packetId == e_PacketId.CarTelemetry)
                    {
                        var casted = (PacketCarTelemetryData)output;
                        Console.WriteLine(
                            casted.m_carTelemetryData[casted.Header.m_playerCarIndex]
                            .m_engineRPM);
                    }
                });

            F1UdpClient.StartRead(new UdpClient(20777)).Subscribe(observer);

            while (true)
            {
            }
        }
    }
}
