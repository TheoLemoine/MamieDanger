using Abstract;
using Global.Input;
using UnityEngine;
using Utils;

namespace GameComponents.Player
{
    public class PlayerInteractor : AInteractorBehaviour
    {
        [SerializeField] private HumanoidIKOverride ikOverride; 
        [SerializeField] private SingleUnityLayer clickableLayer;
        private Vector2 _pointer;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GetComponent<PlayerController>();
            InputManager.PlayerRaycaster.AddListener(clickableLayer.LayerIndex, Interact);
        }

        public void Interact(RaycastHit hit)
        {
            var id = hit.collider.gameObject.GetInstanceID();

            if (_interactablesInRange.TryGetValue(id, out var interactable))
            {
                interactable.Interact(this);
                _playerController.InterruptRoute();
            }
        }

        public HumanoidIKOverride GetIkOverride()
        {
            return ikOverride;
        }
    }
}