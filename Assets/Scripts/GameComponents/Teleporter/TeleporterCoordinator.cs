using System;
using GameComponents.Player;
using UnityEngine;
using UnityEngine.AI;

namespace GameComponents.Teleporter
{
    public class TeleporterCoordinator : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private TeleporterEntry entry1;
        [SerializeField] private TeleporterEntry entry2;
        [SerializeField] private bool startDisabled;
        private NavMeshAgent _playerNavMesh;
        private PlayerInteractor _playerInteractor;
        private PlayerController _playerController;
    
        void Start()
        {
            entry1.RegisterCoordinator(this, startDisabled);
            entry2.RegisterCoordinator(this, startDisabled);
            _playerNavMesh = player.GetComponent<NavMeshAgent>();
            _playerInteractor = player.GetComponent<PlayerInteractor>();
            _playerController = player.GetComponent<PlayerController>();
        }
    
        public GameObject Player => player;
        public int PlayerId => player.GetInstanceID();
        public PlayerController PlayerController => _playerController;

        public NavMeshAgent PlayerNavMesh => _playerNavMesh;

        public PlayerInteractor PlayerInteractor => _playerInteractor;

        public TeleporterEntry GetExitFromEntry(TeleporterEntry from)
        {
            if (from == entry1)
                return entry2;
            if (from == entry2)
                return entry1;
            throw new ArgumentException("The provided entry is not registered in this coordinator");
        }

        public void DisableTunnel(bool value)
        {
            entry1.DisableEntry(value);
            entry2.DisableEntry(value);
        }

        public void HandleOpenEvent(bool value)
        {
            DisableTunnel(!value);
        }
    }
}
