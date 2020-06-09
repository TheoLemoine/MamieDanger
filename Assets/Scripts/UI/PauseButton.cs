using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PauseButton : MonoBehaviour
{
    [SerializeField][Range(0.1f, 1f)] private float duration = 0.4f;
    [SerializeField] private RectTransform pauseBarTransform1;
    [SerializeField] private RectTransform pauseBarTransform2;
    [SerializeField] private Pause pause;

    private bool _isCross;

    public void Toggle()
    {
        if (_isCross)
        {
            TransitionToPauseSymbol();
            pause.ResumeToGame();
        }
        else
        {
            TransitionToCross();
            pause.PauseGame();
        }
        _isCross = !_isCross;
    }


    public void TransitionToCross()
    {
        pauseBarTransform1.DOLocalRotate(new Vector3(0, 0, -45), duration).SetUpdate(true);
        pauseBarTransform1.DOLocalMove(new Vector3(-2.95f, 5.8f, 0), duration).SetUpdate(true);
        pauseBarTransform1.DOScaleY(1.213f, duration).SetUpdate(true);
        pauseBarTransform2.DOLocalRotate(new Vector3(0, 0, 45), duration).SetUpdate(true);
        pauseBarTransform2.DOLocalMove(new Vector3(10.28f, 5.8f, 0), duration).SetUpdate(true);
        pauseBarTransform2.DOScaleY(1.213f, duration).SetUpdate(true);
    }

    public void TransitionToPauseSymbol()
    {
        pauseBarTransform1.DOLocalRotate(new Vector3(0, 0, 0), duration).SetUpdate(true);
        pauseBarTransform1.DOLocalMove(new Vector3(-4.8487f, 5.6177f, 0), duration).SetUpdate(true);
        pauseBarTransform1.DOScaleY(1, duration).SetUpdate(true);
        pauseBarTransform2.DOLocalRotate(new Vector3(0, 0, 0), duration).SetUpdate(true);
        pauseBarTransform2.DOLocalMove(new Vector3(11.634f, 4.9147f, 0), duration).SetUpdate(true);
        pauseBarTransform2.DOScaleY(1, duration).SetUpdate(true);
    }
}
