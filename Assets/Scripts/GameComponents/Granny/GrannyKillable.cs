using DG.Tweening;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.Granny
{
    public class GrannyKillable : MonoBehaviour, IKillable
    {
        
        private Transform _transform;
        private NavMeshAgent _agent;
        private Collider _collider;
        
        private void Start()
        {
            _transform = GetComponent<Transform>();
            _agent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<Collider>();
        }

        public void Kill(GameObject killer)
        {
            _transform.DOScaleY(.1f, .2f);
            _agent.speed = 0f;
            _agent.angularSpeed = 0f;
            _collider.enabled = false;
        }
    }
}