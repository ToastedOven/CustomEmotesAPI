using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class EmoteLocation : MonoBehaviour
{
    public static List<GameObject> emoteLocations = new List<GameObject>();
    public int spot;
    public int validPlayers = 0;
    internal BoneMapper owner;
    void Start()
    {
        SetColor();
        spot = emoteLocations.Count;
        emoteLocations.Add(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ModelLocator>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>())
        {
            BoneMapper mapper = other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>();
            if (mapper)
            {
                validPlayers++;
                SetColor();
                //new SyncCurrentEmoteSpot(other.GetComponent<NetworkIdentity>().netId, gameObject.GetComponent<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Clients);
                mapper.currentEmoteSpot = this.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ModelLocator>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>())
        {
            BoneMapper mapper = other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>();
            if (mapper)
            {
                validPlayers--;
                SetColor();
                if (mapper.currentEmoteSpot == this.gameObject)
                {
                    mapper.currentEmoteSpot = null;
                }
            }
        }
    }
    internal void SetColor()
    {
        if (validPlayers > 0)
        {
            foreach (var item in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.material.color = Color.green;
            }
        }
        else
        {
            foreach (var item in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.material.color = Color.grey;
            }
        }
    }
}
