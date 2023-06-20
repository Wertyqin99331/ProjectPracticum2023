
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.Instance.OnDialogueFinished += OnDialogueFinishedHandler;
    }

    private void OnDestroy()
    {
        DialogueManager.Instance.OnDialogueFinished -= OnDialogueFinishedHandler;
    }

    private void OnDialogueFinishedHandler()
    {
        SceneManager.LoadScene(4);
    }
}
