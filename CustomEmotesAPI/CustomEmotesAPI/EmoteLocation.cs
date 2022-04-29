using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class EmoteLocation : MonoBehaviour
{
    public int validPlayers = 0;
    internal BoneMapper owner;
    void Start()
    {
        SetColor();
    }
    void OnTriggerEnter(Collider other)
    {
        BoneMapper mapper = other.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>();
        if (mapper)
        {
            validPlayers++;
            SetColor();
            mapper.currentEmoteSpot = this.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
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
    void SetColor()
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
