using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Teleporter : MonoBehaviour
{

    [SerializeField] private Transform exitTransform;
    [SerializeField] private SingleUnityLayer playerLayer;
    private Teleporter _exitTeleporter;

    private void Start()
    {
        _exitTeleporter = exitTransform.GetComponent<Teleporter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer.LayerIndex && enabled)
            StartCoroutine(Teleport(other));
    }
    
    
    private IEnumerator Teleport(Collider other)
    {
        var navAgent = other.GetComponent<NavMeshAgent>();
        var cachedTransform = transform;
        var cachedExitPos = exitTransform.position;

        // If the exit is a teleporter, disable it
        if (_exitTeleporter) _exitTeleporter.enabled = false;
        
        // Make the player walk to the portal end
        navAgent.destination = cachedTransform.position - cachedTransform.forward;
        yield return new WaitForSeconds(.8f);
        
        // Teleport and walk outside
        other.transform.position = cachedExitPos;
        navAgent.destination = cachedExitPos + exitTransform.forward * 2f;
        yield return new WaitForSeconds(.1f);
        
        // Reenable teleporter exit
        if (_exitTeleporter) _exitTeleporter.enabled = true;
    }

}
