using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Utils;
using Utils.Attributes;

public class TeleporterCoordinator : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TeleporterEntry entry1;
    [SerializeField] private TeleporterEntry entry2;
    [SerializeField] private bool startDisabled;
    private NavMeshAgent _playerNavMesh;
    private PlayerInput _playerInput;
    
    void Start()
    {
        entry1.RegisterCoordinator(this, startDisabled);
        entry2.RegisterCoordinator(this, startDisabled);
        _playerNavMesh = player.GetComponent<NavMeshAgent>();
        _playerInput = player.GetComponent<PlayerInput>();
    }
    
    public GameObject Player => player;
    public int PlayerId => player.GetInstanceID();

    public NavMeshAgent PlayerNavMesh => _playerNavMesh;

    public PlayerInput PlayerInput => _playerInput;

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
