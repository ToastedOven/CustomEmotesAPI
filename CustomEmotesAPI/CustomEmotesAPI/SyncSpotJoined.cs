using EmotesAPI;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


class SyncSpotJoined : INetMessage
{
    NetworkInstanceId netId;
    NetworkInstanceId spotId;

    public SyncSpotJoined()
    {

    }

    public SyncSpotJoined(NetworkInstanceId netId, NetworkInstanceId spotId)
    {
        this.netId = netId;
        this.spotId = spotId;
        DebugClass.Log($"----------syncing spot is: {this.spotId}");
    }

    public void Deserialize(NetworkReader reader)
    {
        netId = reader.ReadNetworkId();
        spotId = reader.ReadNetworkId();
    }

    public void OnReceived()
    {
        GameObject bodyObject = Util.FindNetworkObject(netId);
        if (!bodyObject)
        {
            DebugClass.Log($"Body is null!!!");
        }
        BoneMapper joinerMapper = bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>();
        //if (!joinerMapper.currentEmoteSpot)
        //{
        //    DebugClass.Log($"----------currentemotespot wasn't, setting it to {spotId}    {EmoteLocation.emoteLocations[spotId]}");
        //}
        joinerMapper.currentEmoteSpot = Util.FindNetworkObject(spotId);
        if (joinerMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner.worldProp)
        {
            CustomEmotesAPI.JoinedProp(joinerMapper.currentEmoteSpot, joinerMapper, joinerMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner);
        }
        else
        {
            CustomEmotesAPI.JoinedBody(joinerMapper.currentEmoteSpot, joinerMapper, joinerMapper.currentEmoteSpot.GetComponent<EmoteLocation>().owner);
        }
    }

    public void Serialize(NetworkWriter writer)
    {
        writer.Write(netId);
        writer.Write(spotId);
    }
}
