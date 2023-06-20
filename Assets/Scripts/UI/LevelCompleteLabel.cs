using System;
using System.Threading.Tasks;
using UnityEngine;

public class LevelCompleteLabel: MonoBehaviour
{
	private void Start()
	{
		gameObject.SetActive(false);
		LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
	}

	private async void OnLevelEndedHandler()
	{
		gameObject.SetActive(true);
		await Task.Delay(3000);
		gameObject.SetActive(false);
	}
}