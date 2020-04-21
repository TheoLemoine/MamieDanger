using Utils.Attributes;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class InteractorRangeDetector : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        
        [SerializeField] private SingleUnityLayer clickableLayer;
        [SerializeField] private SingleUnityLayer notClickableLayer;
        [SerializeField] [TagSelector] private string playerTag = "Player";
        
        [SerializeField] private UnityEvent enterRangeEvent;
        [SerializeField] private UnityEvent exitRangeEvent;
        
        private IInteractable _targetInteractable;

        
        private void Start()
        {
            _targetInteractable = target.GetComponent<IInteractable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var interactor = other.GetComponent<IInteractor>();

            if (other.gameObject.CompareTag(playerTag))
            {
                target.layer = clickableLayer.LayerIndex;
                enterRangeEvent.Invoke();
            }
            
            interactor?.RegisterToRange(_targetInteractable);
        }

        private void OnTriggerExit(Collider other)
        {
            var interactor = other.GetComponent<IInteractor>();

            if (other.gameObject.CompareTag(playerTag))
            {
                target.layer = notClickableLayer.LayerIndex;
                exitRangeEvent.Invoke();
            }
            
            interactor?.DeregisterFromRange(_targetInteractable);
        }
    }
}