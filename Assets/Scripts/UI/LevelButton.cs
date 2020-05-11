using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string levelSceneName;
    [SerializeField] private bool locked;
    [SerializeField] private Material lockedMaterial;
    [SerializeField] private Renderer[] renderersToOverride;

    public void Start()
    {
        if (!locked) return;
        foreach (var renderer1 in renderersToOverride)
            renderer1.material = lockedMaterial;
    }

    public void Click()
    {
        if (!locked)
            SceneManager.LoadScene(levelSceneName);
    }
}
