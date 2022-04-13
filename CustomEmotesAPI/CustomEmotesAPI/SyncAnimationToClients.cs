using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


class SyncAnimationToClients : INetMessage
{
    NetworkInstanceId netId;
    string animation;
    int position;

    public SyncAnimationToClients()
    {

    }

    public SyncAnimationToClients(NetworkInstanceId netId, string animation, int pos)
    {
        this.netId = netId;
        this.animation = animation;
        this.position = pos;
    }

    public void Deserialize(NetworkReader reader)
    {
        DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

        netId = reader.ReadNetworkId();
        animation = reader.ReadString();
        position = reader.ReadInt32();
    }

    public void OnReceived()
    {
        if (NetworkServer.active)
            return;


        GameObject bodyObject = Util.FindNetworkObject(netId);
        if (!bodyObject)
        {
            DebugClass.Log($"Body is null!!!");
        }

        DebugClass.Log($"Recieved message to play {animation} on client. Playing on {bodyObject.GetComponent<ModelLocator>().modelTransform}");

        bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(animation, position);
    }

    public void Serialize(NetworkWriter writer)
    {
        writer.Write(netId);
        writer.Write(animation);
        writer.Write(position);
    }
}
