
using System;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>())
        {
            LevelManager.Instance.StartNextLevel();
            Destroy(gameObject);
        }
    }
}
