using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class EmoteConstraint : MonoBehaviour
{
    public Transform originalBone;
    public Transform emoteBone;
    public bool constraintActive = false;
    void Update()
    {
        if (constraintActive)
        {
            originalBone.localPosition = emoteBone.localPosition;
            originalBone.localEulerAngles = emoteBone.localEulerAngles;
        }
    }
    internal void AddSource(ref Transform originalBone, ref Transform emoteBone)
    {
        this.originalBone = originalBone;
        this.emoteBone = emoteBone;
    }
}
