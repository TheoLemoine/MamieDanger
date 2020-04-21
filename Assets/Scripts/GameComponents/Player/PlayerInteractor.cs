using Abstract;
using Global.Input;
using UnityEngine;
using Utils;

namespace GameComponents.Player
{
    public class PlayerInteractor : AInteractorBehaviour
    {
        [SerializeField] private SingleUnityLayer clickableLayer;
        private Vector2 _pointer;

        private void Start()
        {
            InputManager.PlayerRaycaster.AddListener(clickableLayer.LayerIndex, Interact);
        }

        public void Interact(RaycastHit hit)
        {
            var id = hit.collider.gameObject.GetInstanceID();
                
            if (_interactablesInRange.TryGetValue(id, out var interactable))
                interactable.Interact(this);
        }
    }
}