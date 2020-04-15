using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class TeleporterCoordinator : MonoBehaviour
{
    [SerializeField] private TeleporterEntry entry1;
    [SerializeField] private TeleporterEntry entry2;
    [SerializeField] private SingleUnityLayer playerLayer;
    [SerializeField] private bool startDisabled;
    
    void Start()
    {
        entry1.RegisterCoordinator(this, startDisabled);
        entry2.RegisterCoordinator(this, startDisabled);
    }
    
    public int PlayerLayer => playerLayer.LayerIndex;

    public TeleporterEntry GetExitFromEntry(TeleporterEntry from)
    {
        if (from == entry1)
            return entry2;
        if (from == entry2)
            return entry1;
        throw new ArgumentException("The provided entry is not registered in this coordinator");
    }

    public void DisableTunnel(bool value)
    {
        entry1.DisableEntry(value);
        entry2.DisableEntry(value);
    }
}
