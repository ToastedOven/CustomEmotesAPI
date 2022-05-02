using EmotesAPI;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


class SyncCurrentEmoteSpot : INetMessage
{
    NetworkInstanceId mapperId;
    NetworkInstanceId emoteSpotId;

    public SyncCurrentEmoteSpot()
    {

    }

    public SyncCurrentEmoteSpot(NetworkInstanceId mapperId, NetworkInstanceId emoteSpotId)
    {
        this.mapperId = mapperId;
        this.emoteSpotId = emoteSpotId;
    }

    public void Deserialize(NetworkReader reader)
    {
        mapperId = reader.ReadNetworkId();
        emoteSpotId = reader.ReadNetworkId();
    }

    public void OnReceived()
    {
        GameObject mapperObject = Util.FindNetworkObject(mapperId);
        GameObject emoteSpotObject = Util.FindNetworkObject(emoteSpotId);
        if (!mapperObject || !emoteSpotObject)
        {
            DebugClass.Log($"Body is null!!!");
        }
        else
        {
            BoneMapper joinerMapper = mapperObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>();
            joinerMapper.currentEmoteSpot = emoteSpotObject;
        }
    }

    public void Serialize(NetworkWriter writer)
    {
        writer.Write(mapperId);
        writer.Write(emoteSpotId);
    }
}
