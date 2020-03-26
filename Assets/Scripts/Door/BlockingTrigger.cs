﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BlockingTrigger : MonoBehaviour
{
    private List<int> _blockerIds = new List<int>();
    [SerializeField] [TagSelector] private string blockerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(blockerTag))
            _blockerIds.Add(other.GetInstanceID());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(blockerTag))
            _blockerIds.Remove(other.GetInstanceID());
    }

    public bool IsAreaBlocked()
    {
        return _blockerIds.Count != 0;
    }
}
