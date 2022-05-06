using EmotesAPI;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class EmoteLocation : MonoBehaviour
{
    public static List<EmoteLocation> emoteLocations = new List<EmoteLocation>();
    public int spot;
    public int validPlayers = 0;
    internal BoneMapper owner;
    internal BoneMapper emoter;
    internal JoinSpot joinSpot;

    void Start()
    {
        SetColor();
        //spot = emoteLocations.Count;
        emoteLocations.Add(this);
        StartCoroutine(setScale());
    }
    public void SetEmoterAndHideLocation(BoneMapper boneMapper)
    {
        emoter = boneMapper;
        SetVisible(false);
    }
    public IEnumerator setScale()
    {
        yield return new WaitForSeconds(.1f);
        if (owner.smr1)
        {
            Vector3 scal = owner.transform.parent.lossyScale;
            transform.localPosition = new Vector3(joinSpot.position.x / scal.x, joinSpot.position.y / scal.y, joinSpot.position.z / scal.z);
            transform.localEulerAngles = joinSpot.rotation;
            transform.localScale = new Vector3(joinSpot.scale.x / scal.x, joinSpot.scale.y / scal.y, joinSpot.scale.z / scal.z);
        }
    }
    internal void SetVisible(bool visibility)
    {
        if (visibility)
            gameObject.transform.localPosition += new Vector3(5000, 5000, 5000);
        else
            gameObject.transform.localPosition -= new Vector3(5000, 5000, 5000);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ModelLocator>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>() != owner)
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
        if (other.GetComponent<ModelLocator>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>() && other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>() != owner)
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
