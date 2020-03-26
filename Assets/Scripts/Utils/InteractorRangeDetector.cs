using System;
using Interfaces;
using Player;
using UnityEngine;
using UnityEngine.UIElements;
using Utils.EditorGUI;

namespace Utils
{
    public class InteractorRangeDetector : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        
        [SerializeField] private SingleUnityLayer clickableLayer;
        [SerializeField] private SingleUnityLayer notClickableLayer;
        [SerializeField] [TagSelector] private string playerTag = "Player";
        
        private IInteractable _targetInteractable;

        
        private void Start()
        {
            _targetInteractable = target.GetComponent<IInteractable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactor = other.GetComponent<IInteractor>();
            
            if (other.gameObject.CompareTag(playerTag))
                target.layer = clickableLayer.LayerIndex;
            
            interactor?.RegisterToRange(_targetInteractable);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactor = other.GetComponent<IInteractor>();
            
            if (other.gameObject.CompareTag(playerTag))
                target.layer = notClickableLayer.LayerIndex;
            
            interactor?.DeregisterFromRange(_targetInteractable);
        }
    }
}