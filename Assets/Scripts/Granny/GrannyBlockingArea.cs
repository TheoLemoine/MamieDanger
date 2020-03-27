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
    [SerializeField] [Range(0.2f, 2f)] private float knockbackAmount = 0.5f;

    private int _grannyId;
    private NavMeshAgent _grannyAgent;
    private GrannyController _grannyController;
    private Transform _transform;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _grannyId = granny.GetInstanceID();
        _grannyAgent = granny.GetComponent<NavMeshAgent>();
        _grannyController = granny.GetComponent<GrannyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetInstanceID() == _grannyId)
        {
            var newDest = granny.transform.position + GetClosestUnitVector() * knockbackAmount;
            _grannyAgent.SetDestination(newDest);
            _grannyController.StopInteraction();
        }
    }

    private Vector3 GetClosestUnitVector()
    {
        var grannyVector = granny.transform.position - _transform.position;
        Vector3[] unitVectors = {_transform.forward, _transform.right, _transform.up};
        var biggestDotProduct = 0f;
        var closestUnitVector = Vector3.zero;
        foreach (var unitVector in unitVectors)
        {
            var dotProduct = Vector3.Dot(grannyVector, unitVector);
            if (Mathf.Abs(dotProduct) > biggestDotProduct)
            {
                biggestDotProduct = dotProduct;
                closestUnitVector = unitVector;
            }
        }

        return biggestDotProduct > 0 ? closestUnitVector : -closestUnitVector;
    } 
}
