
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelCompleteText;

    public static LevelManager Instance { get; private set; }
    public event Action OnLevelStarted;
    public event Action OnPlayButtonClicked;
    public event Action OnLevelRestarted;
    public event Action OnLevelEnded;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void StartNextLevel()
    {
        OnLevelStarted?.Invoke();
    }

    public void EndLevel()
    { ;
        OnLevelEnded?.Invoke();
        ShowLevelCompleteLabel();
        StartNewLevel();
    }

    public void MakeObjectsPhysical()
    {
        OnPlayButtonClicked?.Invoke();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        OnLevelRestarted?.Invoke();
    }

    private async void ShowLevelCompleteLabel()
    {
        _levelCompleteText.gameObject.SetActive(true);
        await Task.Delay(3000);
        _levelCompleteText.gameObject.SetActive(false);
    }

    private async void StartNewLevel()
    {
        await Task.Delay(5000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
