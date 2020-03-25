using DG.Tweening;
using UnityEngine;
using Abstract;
using Interfaces;

public class Openable : AInteracToggleBehaviour
{
    [SerializeField] private GameObject doorModel;
    [SerializeField] private Transform hingeTransform;
    [SerializeField] [Range(0f, 2f)] private float animationDuration = 1f;
    
    private Quaternion _baseHingeRotation;
    
    void Start()
    {
        _baseHingeRotation = hingeTransform.rotation;
    }

    protected override void InteractStart(IInteractor interactor)
    {
        hingeTransform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), animationDuration);
    }

    protected override void InteractStop(IInteractor interactor)
    {
        hingeTransform.DORotateQuaternion(_baseHingeRotation, animationDuration);
        
    }
}
