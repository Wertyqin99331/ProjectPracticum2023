using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        LevelManager.Instance.OnLevelStarted += OnLevelStartedHandler;
        LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
        LevelManager.Instance.OnLevelRestarted += OnLevelRestartedHandler;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLevelStarted -= OnLevelStartedHandler;
        LevelManager.Instance.OnLevelEnded -= OnLevelEndedHandler;
        LevelManager.Instance.OnLevelRestarted -= OnLevelRestartedHandler;
    }

    private void OnLevelStartedHandler() => gameObject.SetActive(true);
    private void OnLevelEndedHandler() => gameObject.SetActive(false);
    private void OnLevelRestartedHandler() => gameObject.SetActive(false);
}
