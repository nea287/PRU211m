using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLine : Dialogue
{
    private Text textHolder;
    [Header("Text Options")]
    [SerializeField] private string[] input;
    [SerializeField] private float delay;
    [Header("Sound")]
    [SerializeField] private AudioClip source;
    [Header("Character Image")]
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;
    [SerializeField] private GameObject game;
    
    private void Awake()
    {
        textHolder = GetComponent<Text>();
        StartCoroutine(WriteText(input, textHolder, delay, source, game));
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;  
    }
}
