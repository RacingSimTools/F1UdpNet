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
    /// <summary>
    /// Interface used to group packets together.
    /// </summary>
    public interface IF1Packet
    {
        /// <summary>
        /// Gets packet header.
        /// </summary>
        PacketHeader Header { get; }
    }
}