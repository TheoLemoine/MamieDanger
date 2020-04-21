using System;
using System.Collections;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace GameComponents.Door
{
    [Serializable]
    public class OpenEvent : UnityEvent<bool>
    {
    }

    public class Openable : MonoBehaviour, IInteractable
    {
        [SerializeField] [Range(0f, 2f)] private float animationDuration = 1f;
        [SerializeField] private BlockingTrigger blockingArea = null;
        [SerializeField] private OpenEvent openEvent;
    
        private NavMeshObstacle _navMeshObstacle;
        private Quaternion _baseHingeRotation;
        private bool _isOpen;
    
        void Start()
        {
            if(openEvent == null)
                openEvent = new OpenEvent();
            _baseHingeRotation = transform.rotation;
            _navMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        public void Interact(IInteractor interactor)
        {
            if (!_isOpen)
            {
                if (blockingArea != null && blockingArea.IsAreaBlocked())
                    StartCoroutine(CantOpenAnimation());
                else
                {
                    transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), animationDuration);
                    _navMeshObstacle.enabled = false;
                    _isOpen = true;
                    openEvent.Invoke(_isOpen);
                }
            }
            else
            {
                if (blockingArea != null && blockingArea.IsAreaBlocked())
                    StartCoroutine(CantCloseAnimation());
                else
                {
                    transform.DORotateQuaternion(_baseHingeRotation, animationDuration);
                    _navMeshObstacle.enabled = true;
                    _isOpen = false;
                    openEvent.Invoke(_isOpen);
                }
            }
        }

        private IEnumerator CantOpenAnimation()
        {
            transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -15, 0), 0.3f);
            yield return new WaitForSeconds(0.3f);
            transform.DORotateQuaternion(_baseHingeRotation, 0.3f);
        }
    
        private IEnumerator CantCloseAnimation()
        {
            transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -75, 0), 0.3f);
            yield return new WaitForSeconds(0.3f);
            transform.DORotateQuaternion(_baseHingeRotation * Quaternion.Euler(0, -90, 0), 0.3f);
        }

        public void StopInteraction(){}

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}