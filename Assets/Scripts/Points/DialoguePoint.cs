using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePoint : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    private bool _isActivated = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        print("started");
        if (col.GetComponent<Player>() && !_isActivated)
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
