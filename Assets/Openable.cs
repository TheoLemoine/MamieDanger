using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using UnityEngine;

public class Openable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject doorModel;
    [SerializeField] private Transform hingeTransform;
    [SerializeField] [Range(0f, 2f)] private float animationDuration = 1f;
    private Quaternion _baseHingeRotation;
    private bool _isOpened = false;
    void Start()
    {
        _baseHingeRotation = hingeTransform.rotation;
    }

    public void InteractStart(GameObject interactor)
    {
        if (IsInteracting()) return;

        hingeTransform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), animationDuration);
        _isOpened = true;
    }

    public void InteractStop()
    {
        if (!IsInteracting()) return;
        hingeTransform.DORotateQuaternion(_baseHingeRotation, animationDuration);
        
        _isOpened = false;
    }

    public bool IsInteracting()
    {
        return _isOpened;
    }
}
