// <copyright file="PacketLapData.cs" company="Racing Sim Tools">
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
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Lap details of all the cars in the session.
    /// </summary>
    public struct PacketLapData : IF1Packet
    {
        /// <summary>
        /// Header
        /// </summary>
        public PacketHeader m_header;

        /// <summary>
        /// Lap data for all cars on track
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20)]
        public LapData[] m_lapData;

        /// <inheritdoc/>
        public PacketHeader Header => this.m_header;
    }
}
