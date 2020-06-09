using System;
using System.Collections.Generic;
using System.Linq;
using Global.Save;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameComponents.Player
{
    public class PlayerRewards : MonoBehaviour
    {
        public static Dictionary<string, bool> Rewards;
        public static int RewardAvailableCount => Rewards.Count;
        public static int RewardObtainedCount => Rewards.Count(pair => pair.Value);

        private void Awake()
        {
            Rewards = new Dictionary<string, bool>();
        }

        private void OnEnable() // used as a late Start()
        {
            if(!SaveManager.IsReady) return;
            
            var sceneName = SceneManager.GetActiveScene().name;
            
            if (SaveManager.Instance.Data.Levels.TryGetValue(sceneName, out var levelResults))
            {
                Rewards = levelResults.CoinsPicked.ToDictionary(id => id, id => true);
            }
        }
        
        public static void RegisterReward(string rewardId)
        {
            Rewards[rewardId] = false;
        }

        public static void AddRewardPicked(string rewardId)
        {
            Rewards[rewardId] = true;
        }

        public static void SaveRewards()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SaveManager.Instance.Data.Levels[sceneName] = new SaveData.LevelResults()
            {
                Finished = true,
                CoinsPicked = Rewards.Where(pair => pair.Value).Select(pair => pair.Key).ToList()
            };
            SaveManager.Instance.Persist();
        }
    }
}
