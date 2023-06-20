using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    public event Action OnDialogueFinished;
    
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Sprite _heroDialogueSprite;
    [SerializeField] private Sprite _helperDialogueSprite;

    private Queue<Character> _characters;
    private Queue<string> _sentences;
    private Image _backgroundImage;

    private void Awake()
    {
        Instance = this;
        _backgroundImage = GetComponent<Image>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
        
        _characters = new Queue<Character>();
        _sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);
        _sentences.Clear();

        for (var i = 0; i < dialogue.Lines.Length; i++)
        {
            _characters.Enqueue(dialogue.Characters[i]);
            _sentences.Enqueue(dialogue.Lines[i]);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var nextCharacter = _characters.Dequeue();
        var nextLine = _sentences.Dequeue();

        _backgroundImage.sprite = nextCharacter == Character.Player ? _heroDialogueSprite : _helperDialogueSprite;
        _textField.text = nextLine;
    }

    private void EndDialogue()
    {
        OnDialogueFinished?.Invoke();
        gameObject.SetActive(false);
    }
}
