using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.Teleporter
{
    public class TeleporterEntry : MonoBehaviour
    {
        [SerializeField] private MeshRenderer shadow;
        [SerializeField] private string alphaPropName;
        [SerializeField] [Range(0.2f, 3f)] private float fadeDuration = 2f;
        [SerializeField] [Range(0f, 1f)] private float disabledAlpha = 0.1f;
        private int _alphaPropId;
    
        private TeleporterEntry _exitTeleporter;
        private TeleporterCoordinator _coordinator;
    
        private bool _disableTeleport;
        private bool _waitToEnter;
        private bool _disableNextTriggerEnter;

        public void RegisterCoordinator(TeleporterCoordinator coordinator, bool startDisabled)
        {
            _alphaPropId = Shader.PropertyToID(alphaPropName);
            _coordinator = coordinator;
            _disableTeleport = startDisabled;
            if (startDisabled) shadow.material.SetFloat(_alphaPropId, disabledAlpha);
        }

        public void DisableEntry(bool value)
        {
            _disableTeleport = value;
            shadow.material.DOFloat(value ? disabledAlpha : 1f, _alphaPropId, fadeDuration);
        }

        private void Update()
        {
            if (_waitToEnter)
            {
                // Teleport only when player walked all the way through the teleporter
                var playerPos = _coordinator.Player.transform.position;
                var dist = (transform.position - playerPos).magnitude;
                if (dist < 1f) Teleport();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetInstanceID() == _coordinator.PlayerId && !_disableTeleport)
            {
                //  When the player is teleported, he enter the trigger box and we dont want him to be sent back into the teleporter
                if (_disableNextTriggerEnter)
                    _disableNextTriggerEnter = false;
                else
                    MoveToEnd();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetInstanceID() == _coordinator.PlayerId && _waitToEnter)
            {
                _waitToEnter = false;
                _coordinator.PlayerController.ListenInputs();
            }
        }

        private void MoveToEnd()
        {
            _coordinator.PlayerNavMesh.destination = transform.position;
            _coordinator.PlayerController.IgnoreInputs();
            _waitToEnter = true;
        }

        private void Teleport()
        {
            _waitToEnter = false;
            _coordinator.PlayerController.ListenInputs();
        
            var exit = _coordinator.GetExitFromEntry(this);
            var exitTransform = exit.transform;
            var exitPos = exitTransform.position;
            var warpPos = exitPos;
            var forward = exitTransform.forward;

            var interacToggle = _coordinator.PlayerInteractor.CurrentInteracting;
            if (interacToggle != null)
            {
                // Using dot product to know wether the interacting object is behind or in front of the player  
                var agentDir = Vector3.Dot(forward,
                    interacToggle.GetGameObject().transform.position - _coordinator.Player.transform.position);
                
                // Adjust teleport position accordingly
                var agentExitPos = agentDir < 0
                    ? exitPos + forward * 0.2f
                    : exitPos;
                warpPos += agentDir < 0 ? -forward * 0.2f : Vector3.zero;

                var navAgent = interacToggle.GetGameObject().GetComponent<NavMeshAgent>();
                if (navAgent != null)
                {
                    // Navmesh agents must be teleported using warp
                    navAgent.Warp(agentExitPos);
                    navAgent.Warp(agentExitPos + forward * 2f);
                }
                else
                    interacToggle.GetGameObject().transform.position = agentExitPos;
            }
            
            _coordinator.PlayerNavMesh.Warp(warpPos);
            _coordinator.PlayerNavMesh.destination = exitPos + forward * 2f;
            _coordinator.Player.transform.rotation = exitTransform.rotation;
            exit._disableNextTriggerEnter = true;
        }
    }
}
