using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUgui;
    private float _timer;
    
    void Start()
    {
        _timer = 0;
        _textMeshProUgui = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        var seconds = (int)_timer % 60;
        var secondsText = seconds.ToString().PadLeft(2, '0');
        var minutes = (int)Mathf.Floor(_timer / 60f);
        var minutesText = minutes.ToString().PadLeft(2, '0');
        _textMeshProUgui.text = $"{minutesText}:{secondsText}";
    }
}
