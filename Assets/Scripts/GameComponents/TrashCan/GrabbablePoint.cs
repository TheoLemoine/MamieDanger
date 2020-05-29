using System;
using Abstract;
using GameComponents.Player;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.TrashCan
{
    public class GrabbablePoint : AInteracToggleBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Transform handle;

        [SerializeField] private Vector3 relativeTargetPosition;
        
        private NavMeshObstacle _navObstacle;

        private IInteractor _interactor;
        private Transform _grabber;

        private void Start()
        {
            _navObstacle = GetComponent<NavMeshObstacle>();
        }

        private void FixedUpdate()
        {
            if (IsInteracting())
            {
                var targetPos = _grabber.TransformPoint(relativeTargetPosition);
                
                rigidbody.MovePosition(targetPos);
                rigidbody.MoveRotation(_grabber.rotation);
            }
        }

        protected override void InteractStart(IInteractor interactor)
        {
            _interactor = interactor;
            _grabber = interactor.GetGameObject().transform;
            
            _navObstacle.enabled = false;
            
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
            rigidbody.isKinematic = true;
            
            if(_interactor is PlayerInteractor playerInteractor)
            {
                playerInteractor
                    .GetIkOverride()
                    .SetGoal(AvatarIKGoal.RightHand, handle);
            }
        }

        protected override void InteractStop()
        {
            _grabber = null;

            _navObstacle.enabled = true;
            
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.isKinematic = false;
            
            if(_interactor is PlayerInteractor playerInteractor)
            {
                playerInteractor
                    .GetIkOverride()
                    .UnsetGoal(AvatarIKGoal.RightHand);
            }
            
            _interactor = null;
        }
    }
}