using System;
using UnityEngine;
using Utils;
using Utils.Attributes;

public class TeleporterCoordinator : MonoBehaviour
{
    [SerializeField] private TeleporterEntry entry1;
    [SerializeField] private TeleporterEntry entry2;
    [SerializeField][TagSelector] private string playerTag;
    [SerializeField] private bool startDisabled;
    
    void Start()
    {
        entry1.RegisterCoordinator(this, startDisabled);
        entry2.RegisterCoordinator(this, startDisabled);
    }
    
    public string PlayerTag => playerTag;

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

    public void HandleOpenEvent(bool value)
    {
        DisableTunnel(!value);
    }
}
