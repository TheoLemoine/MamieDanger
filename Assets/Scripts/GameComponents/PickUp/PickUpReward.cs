using GameComponents.Player;
using UnityEngine;

namespace GameComponents.PickUp
{
    public class PickUpReward : MonoBehaviour
    {
        [SerializeField] private string pickUpId;

        private void Start()
        {
            PlayerRewards.RegisterReward(pickUpId);
        }

        public void Pick()
        {
            PlayerRewards.AddRewardPicked(pickUpId);
        }
    }
}