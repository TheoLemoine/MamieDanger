using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
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
    private List<GameObject> _interacTogglesInTrigger = new List<GameObject>();
    
    private bool _disableTeleport;
    private bool _waitToEnter;
    private bool _disableNextTriggerEnter;

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

    private void Update()
    {
        if (_waitToEnter)
        {
            var playerPos = _coordinator.Player.transform.position;
            var dist = (transform.position - playerPos).magnitude;
            if (dist < 0.5f) Teleport();
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetInstanceID() == _coordinator.PlayerId && !_disableTeleport)
        {
            if (_disableNextTriggerEnter)
                _disableNextTriggerEnter = false;
            else
                MoveToEnd();
        }
        
        
        var interacToggleComponent = other.GetComponent<AInteracToggleBehaviour>();
        if (interacToggleComponent != null)
            _interacTogglesInTrigger.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetInstanceID() == _coordinator.PlayerId && _waitToEnter)
        {
            _waitToEnter = false;
            _coordinator.PlayerInput.enabled = true;
        }
        
        if (_interacTogglesInTrigger.Contains(other.gameObject))
            _interacTogglesInTrigger.Remove(other.gameObject);
    }

    private void MoveToEnd()
    {
        _coordinator.PlayerNavMesh.destination = transform.position;
        _coordinator.PlayerInput.enabled = false;
        _waitToEnter = true;
    }

    private void Teleport()
    {
        _waitToEnter = false;
        _coordinator.PlayerInput.enabled = true;
        
        var exit = _coordinator.GetExitFromEntry(this);
        var exitPos = exit.transform.position;
        
        foreach (var interacToggle in _interacTogglesInTrigger)
        {
            Debug.Log(interacToggle.name);
            var navAgent = interacToggle.GetComponent<NavMeshAgent>();
            if (navAgent != null)
                navAgent.Warp(exitPos);
            else
                interacToggle.transform.position = exitPos;
        }

        _coordinator.PlayerNavMesh.Warp(exitPos);
        _coordinator.PlayerNavMesh.destination = exitPos + exit.transform.forward * 2f;
        exit._disableNextTriggerEnter = true;
    }
}
