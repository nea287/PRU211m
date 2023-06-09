using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    
    protected bool isTyping = false;
    private void WriteDialogue()
    {

    }
    protected IEnumerator WriteText(string input, Text textHolder, float delay, AudioClip sound)
    {

        for (int i = 0; i < input.Length; i++)
        {

            //isTyping = true;
            textHolder.text += input[i];
            yield return new WaitForSeconds(delay);
            isTyping = true;

        }
        isTyping = false;
        



    }
}
