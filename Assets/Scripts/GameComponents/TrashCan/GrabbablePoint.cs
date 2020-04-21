using Abstract;
using Interfaces;
using UnityEngine;

namespace GameComponents.TrashCan
{
    public class GrabbablePoint : AInteracToggleBehaviour
    {
        [SerializeField] private SpringJoint joint;
        [SerializeField] private Rigidbody rigidbody;

        private float _baseSpring;

        private void Start()
        {
            _baseSpring = joint.spring;
            joint.spring = 0;
        }

        protected override void InteractStart(IInteractor interactor)
        {
            joint.connectedBody = interactor.GetGameObject().GetComponent<Rigidbody>();
            joint.spring = _baseSpring;
            
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ;
        }

        protected override void InteractStop()
        {
            joint.connectedBody = null;
            joint.spring = 0;
            
            rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}