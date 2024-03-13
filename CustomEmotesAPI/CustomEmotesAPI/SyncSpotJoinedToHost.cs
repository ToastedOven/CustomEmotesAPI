using EmotesAPI;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


class SyncSpotJoinedToHost : INetMessage
{
    NetworkInstanceId netId;
    NetworkInstanceId spot;
    bool worldProp;
    int posInArray;
    public SyncSpotJoinedToHost()
    {

    }

    public SyncSpotJoinedToHost(NetworkInstanceId netId, NetworkInstanceId spot, bool worldProp, int posInArray)
    {
        this.netId = netId;
        this.spot = spot;
        this.worldProp = worldProp;
        this.posInArray = posInArray;
    }

    public void Deserialize(NetworkReader reader)
    {
        netId = reader.ReadNetworkId();
        spot = reader.ReadNetworkId();
        worldProp = reader.ReadBoolean();
        posInArray = reader.ReadInt32();
    }

    public void OnReceived()
    {
        new SyncSpotJoinedToClient(netId, spot, worldProp, posInArray).Send(R2API.Networking.NetworkDestination.Clients);
    }

    public void Serialize(NetworkWriter writer)
    {
        writer.Write(netId);
        writer.Write(spot);
        writer.Write(worldProp);
        writer.Write(posInArray);
    }
}
