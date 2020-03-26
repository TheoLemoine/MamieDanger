using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Interfaces;

public class Openable : MonoBehaviour, IInteractable
{
    [SerializeField] [Range(0f, 2f)] private float animationDuration = 1f;
    [SerializeField] private BlockingTrigger _blockingArea = null;
    
    private Quaternion _baseHingeRotation;
    private bool _isOpen;
    
    void Start()
    {
        _baseHingeRotation = transform.rotation;
    }

    public void Interact(IInteractor interactor)
    {
        if (!_isOpen)
        {
            if (_blockingArea != null && _blockingArea.IsAreaBlocked())
                StartCoroutine(CantOpenAnimation());
            else
                transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), animationDuration);
            _isOpen = true;
        }
        else
        {
            transform.DORotateQuaternion(_baseHingeRotation, animationDuration);
            _isOpen = false;
        }
    }

    private IEnumerator CantOpenAnimation()
    {
        transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -15, 0), 0.3f);
        yield return new WaitForSeconds(0.3f);
        transform.DORotateQuaternion(_baseHingeRotation, 0.3f);
    }

    public void StopInteraction(){}

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
