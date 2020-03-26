using DG.Tweening;
using UnityEngine;
using Interfaces;

public class Openable : MonoBehaviour, IInteractable
{
    [SerializeField] [Range(0f, 2f)] private float animationDuration = 1f;
    
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
            transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), animationDuration);
            _isOpen = true;
        }
        else
        {
            transform.DORotateQuaternion(_baseHingeRotation, animationDuration);
            _isOpen = false;
        }
    }

    public void StopInteraction(){}

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
