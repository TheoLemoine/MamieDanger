using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelRewardDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject rewardFoundPrefab;
        [SerializeField] private GameObject rewardNotFoundPrefab;
        private List<Image> _notFoundCoins;

        public void LayoutPrefabs(int numberPicked)
        {
            // Clean up list if it's not empty
            if (_notFoundCoins != null && _notFoundCoins.Count != 0)
                foreach (var notFoundCoin in _notFoundCoins)
                    Destroy(notFoundCoin.gameObject);
            
            _notFoundCoins = new List<Image>();
            for (int i = 0; i < 3; i++)
            {
                if (i < numberPicked)
                    Instantiate(rewardFoundPrefab, transform);
                else
                    _notFoundCoins.Add(Instantiate(rewardNotFoundPrefab, transform).GetComponent<Image>());
            }
        }

        public void UpdateCoinsColor(Color color)
        {
            foreach (var coin in _notFoundCoins)
            {
                coin.color = color;
            }
        }
    }
}
