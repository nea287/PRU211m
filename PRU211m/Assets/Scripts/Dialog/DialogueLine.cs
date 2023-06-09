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
    public static bool flagCheckDialog = false;
    private int currentText = 0;
    
    private void Awake()
    {
        textHolder = GetComponent<Text>();
        StartCoroutine(WriteText(input[currentText], textHolder, delay, source));
        currentText++;
        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;  
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit"))
        {

            if (game.active && !isTyping)
            {
                textHolder.text = "";
                StartCoroutine(WriteText(input[currentText], textHolder, delay, source));
                currentText++;
            }
            if (currentText == input.Length)
            {
                game.active = false;
                flagCheckDialog = true;

            }

        }

    }
}
