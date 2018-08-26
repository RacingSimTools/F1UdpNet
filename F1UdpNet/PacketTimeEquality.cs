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
    using System.Collections.Generic;

    /// <summary>
    /// Compares packets using just the session timestamp.
    /// Used to drop all packets when game is paused.
    /// </summary>
    internal class PacketTimeEquality : IEqualityComparer<IF1Packet>
    {
        /// <inheritdoc/>
        public bool Equals(IF1Packet x, IF1Packet y)
        {
            return x.Header.m_sessionTime == y.Header.m_sessionTime;
        }

        /// <inheritdoc/>
        public int GetHashCode(IF1Packet obj)
        {
            return obj.Header.m_sessionTime.GetHashCode();
        }
    }
}