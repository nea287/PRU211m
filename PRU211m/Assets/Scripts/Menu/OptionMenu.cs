using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
