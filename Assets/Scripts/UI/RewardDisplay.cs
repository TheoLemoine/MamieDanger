using System.Collections;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] private UIScale[] rewards;
    [SerializeField][Range(0.01f, 1f)] private float offset = 0.2f;

    public void AnimateRewards()
    {
        var i = 0;
        foreach (var reward in rewards)
        {
            StartCoroutine(WaitForAnimate(offset * i, reward));
            i++;
        }
    }

    private IEnumerator WaitForAnimate(float seconds, UIScale reward)
    {
        yield return new WaitForSeconds(seconds);
        reward.ScaleIn();
    }
}
