﻿using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[DefaultExecutionOrder(-2)]
public class EmoteConstraint : MonoBehaviour
{
    public Transform originalBone;
    public Transform emoteBone;
    Vector3 originalPosition;
    Quaternion originalRotation;
    public bool constraintActive = false;
    public bool revertTransform;
    bool firstTime = true;
    bool hasEverActivatedConstraints = false;
    void LateUpdate()
    {
        if (constraintActive)
        {
            originalBone.position = emoteBone.position;
            originalBone.rotation = emoteBone.rotation;
        }
    }
    public void ActivateConstraints()
    {
        originalPosition = originalBone.localPosition;
        originalRotation = originalBone.localRotation;
        hasEverActivatedConstraints = true;
        constraintActive = true;
    }
    public void DeactivateConstraints()
    {
        constraintActive = false;
        if (firstTime || !revertTransform || !hasEverActivatedConstraints)
        {
            firstTime = false;
        }
        else
        {
            originalBone.localPosition = originalPosition;
            originalBone.localRotation = originalRotation;
        }
    }
    internal void AddSource(ref Transform originalBone, ref Transform emoteBone)
    {
        this.originalBone = originalBone;
        this.emoteBone = emoteBone;
    }
}
