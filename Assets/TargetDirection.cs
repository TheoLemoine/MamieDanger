using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDirection : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float distance = 50f;
    private Camera _camera;
    private RectTransform _indicatorTransform;

    private void Start()
    {
        _camera = Camera.main;
        _indicatorTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        var angle = CalcAngle();
        var newPos = new Vector3((float)Math.Cos(angle) * distance, (float)Math.Sin(angle) * distance, 0);
        _indicatorTransform.localPosition = newPos;
    }

    private float CalcAngle()
    {
        var targetScreenPoint = _camera.WorldToScreenPoint(targetTransform.position);
        var cursorPoint = new Vector2(targetScreenPoint.x - Screen.width / 2f, targetScreenPoint.y - Screen.height / 2f);
        return (float)Math.Atan2(cursorPoint.y, cursorPoint.x);
    }
}
