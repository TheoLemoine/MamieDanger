using System.Collections.Generic;
using System.Linq;
using GameComponents.Player;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private string finishedRewardKey = "level_finished";
    [SerializeField] private List<Image> rewardImages;
    [SerializeField] private Sprite rewardSprite;
    [SerializeField] private Sprite emptyRewardSprite;

    private int _lastRewardCount = -1;
    
    void Update()
    {
        var newRewardCount = PlayerRewards.Rewards.Count(pair => pair.Value && pair.Key != finishedRewardKey);
        var hasChanged = _lastRewardCount == newRewardCount;
        _lastRewardCount = newRewardCount;
        if (hasChanged) return;
        var index = 0;
        foreach (var rewardImage in rewardImages)
        {
            rewardImage.sprite = index < _lastRewardCount ? rewardSprite : emptyRewardSprite;
            index++;
        }
    }
}
