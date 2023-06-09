using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static bool flagCheckDialog = false;
    protected IEnumerator WriteText(string[] input, Text textHolder, float delay, AudioClip sound, GameObject game)
    {
        int count = 0;
        while(input.Length >= count)
        {

            for (int i = 0; i < input[count].Length; i++)
            {

                textHolder.text += input[count][i];
                yield return new WaitForSeconds(delay);
            }
            textHolder.text = "";
            if (Input.GetMouseButtonDown(0))
            {
                count++;
            }
            
        }
        game.active = false;
        flagCheckDialog = true;

    }
}
