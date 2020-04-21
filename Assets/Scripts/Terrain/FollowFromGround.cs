using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class FollowFromGround : MonoBehaviour
{

    [SerializeField] private Transform objectToFollow;
    [SerializeField] private SingleUnityLayer groundLayer;
    [SerializeField] private float maxRaycastDistance = 100f;
    [SerializeField] private Vector3 raycastOriginAdjustment;
    [SerializeField] [Range(0.001f, 0.05f)] private float normalOffset = 0.005f;
    [SerializeField] private bool keepObjectRotation = true;
    [SerializeField] private Vector3 objectRotationAdjustment;
    private Transform _transform;
    
    void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        if (Physics.Raycast(objectToFollow.position + objectToFollow.TransformVector(raycastOriginAdjustment), Vector3.down, out var hit, maxRaycastDistance, groundLayer.Mask))
        {
            _transform.position = hit.point + hit.normal * normalOffset;
            var yRotation = keepObjectRotation ? objectToFollow.rotation.eulerAngles.y : 0f;
            _transform.rotation = Quaternion.LookRotation(hit.normal, Vector3.up) * Quaternion.Euler(objectRotationAdjustment) * Quaternion.AngleAxis(yRotation, Vector3.up);
        }
    }
}
