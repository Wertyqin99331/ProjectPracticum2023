
using UnityEngine;

public class Grid : MonoBehaviour
{
	private static Grid _instance;

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
