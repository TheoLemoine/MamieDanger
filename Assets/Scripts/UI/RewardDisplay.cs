using System.Collections;
using GameComponents.Player;
using UI.Animations;
using UnityEngine;

namespace UI
{
    public class RewardDisplay : MonoBehaviour
    {
        [SerializeField][Range(0.01f, 1f)] private float offset = 0.2f;
        [SerializeField] [Range(0.01f, 1f)] private float notFoundAdditionalOffset = 0.1f;
        [SerializeField] private GameObject rewardFoundPrefab;
        [SerializeField] private GameObject rewardNotFoundPrefab;

        public void AnimateRewards()
        {
            var time = 0f;
            for (int i = 0; i < PlayerRewards.RewardAvailableCount; i++)
            {
                var isPicked = i < PlayerRewards.RewardObtainedCount;
                var prefab = isPicked ? rewardFoundPrefab : rewardNotFoundPrefab;
                var reward = Instantiate(prefab, transform);

                time += offset;
                if (i == PlayerRewards.RewardObtainedCount) time += notFoundAdditionalOffset;
            
                StartCoroutine(WaitForAnimate(time, reward.GetComponent<UIScale>()));
            }
        }

        private IEnumerator WaitForAnimate(float seconds, UIScale rewardUiScale)
        {
            yield return new WaitForSeconds(seconds);
            rewardUiScale.ScaleIn();
        }
    }
}
