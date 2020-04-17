using UnityEngine;
using UnityEngine.AI;
using Global.Input;
using Utils;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private SingleUnityLayer groundLayer;

        private NavMeshAgent _agent;

        private Vector2 _pointer;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            InputManager.PlayerRaycaster.AddListener(groundLayer.LayerIndex, Move);
        }
        
        private void Move(RaycastHit hit)
        {
            _agent.SetDestination(hit.point);
        }

    }
}
