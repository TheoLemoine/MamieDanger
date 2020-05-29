using System;
using UnityEngine;

public class PlayerRewardCounter : MonoBehaviour
{
    private static int _numberPicked;
    private static int _numberInMap;
    public static int NumberPicked => _numberPicked;
    public static int NumberInMap => _numberInMap;

    [SerializeField] private int inMapResetNumber;
    [SerializeField] private int pickedResetNumber;

    private void Awake()
    {
        _numberPicked = inMapResetNumber;
        _numberInMap = pickedResetNumber;
    }

    public static void IncrementRewardPicked()
    {
        _numberPicked++;
    }

    public static void IncrementRewardInMap()
    {
        _numberInMap++;
    }
}
