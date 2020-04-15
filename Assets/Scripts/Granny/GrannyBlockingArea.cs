using System;
using System.Collections;
using System.Collections.Generic;
using Granny;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class GrannyBlockingArea : MonoBehaviour
{
    [SerializeField] private GameObject granny;

    private int _grannyId;
    private GrannyController _grannyController;
    
    // Start is called before the first frame update
    void Start()
    {
        _grannyId = granny.GetInstanceID();
        _grannyController = granny.GetComponent<GrannyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetInstanceID() == _grannyId)
            _grannyController.StopInteraction();
    }
}
