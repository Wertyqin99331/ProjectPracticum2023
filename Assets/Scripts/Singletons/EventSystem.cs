
using UnityEngine;

public class EventSystem : MonoBehaviour
{
	private static EventSystem _instance;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
			return;
		}
        
		Destroy(gameObject);
	}
}
