using System;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private void Start()
    {
        PlayerRewardCounter.IncrementRewardInMap();
    }

    public void NotifyPlayerCounter()
    {
        PlayerRewardCounter.IncrementRewardPicked();
    }
}
