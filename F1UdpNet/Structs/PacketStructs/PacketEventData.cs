// <copyright file="PacketEventData.cs" company="Racing Sim Tools">
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
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Details of events that happen during the course of the race.
    /// </summary>
    public struct PacketEventData : IF1Packet
    {
        /// <summary>
        /// Header
        /// </summary>
        public PacketHeader m_header;

        /// <summary>
        /// Event string code:
        /// "SSTA" -> On session start
        /// "SEND" -> On session end
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] m_eventStringCode;

        /// <inheritdoc/>
        public PacketHeader Header => this.m_header;
    }
}
