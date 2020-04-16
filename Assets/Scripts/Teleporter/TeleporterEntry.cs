using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class TeleporterEntry : MonoBehaviour
{
    [SerializeField] private MeshRenderer shadow;
    [SerializeField] private string alphaPropName;
    [SerializeField] [Range(0.2f, 3f)] private float fadeDuration = 2f;
    [SerializeField] [Range(0f, 1f)] private float disabledAlpha = 0.1f;
    private int _alphaPropId;
    private TeleporterEntry _exitTeleporter;
    private TeleporterCoordinator _coordinator;
    private bool _disableTeleport;

    public void RegisterCoordinator(TeleporterCoordinator coordinator, bool startDisabled)
    {
        _alphaPropId = Shader.PropertyToID(alphaPropName);
        _coordinator = coordinator;
        _disableTeleport = startDisabled;
        if (startDisabled) shadow.material.SetFloat(_alphaPropId, disabledAlpha);
    }

    public void DisableEntry(bool value)
    {
        _disableTeleport = value;
        shadow.material.DOFloat(value ? disabledAlpha : 1f, _alphaPropId, fadeDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_coordinator.PlayerTag) && !_disableTeleport)
            StartCoroutine(Teleport(other));
    }

    private IEnumerator Teleport(Component other)
    {
        var fromTransform = transform;
        
        var to = _coordinator.GetExitFromEntry(this);
        var toTransform = to.transform;
        var toPosition = toTransform.position;
        
        var navAgent = other.GetComponent<NavMeshAgent>();

        // Disable portal exit
        to._disableTeleport = true;
        
        // Make the player walk to the portal end
        navAgent.destination = fromTransform.position - fromTransform.forward;
        yield return new WaitForSeconds(.8f);
        
        // Teleport and walk outside
        other.transform.position = toPosition;
        navAgent.destination = toPosition + toTransform.forward * 2f;
        yield return new WaitForSeconds(.1f);
        
        // Reenable teleporter exit
        to._disableTeleport = false;
    }
}
