using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Heart playerHeart;
    [SerializeField] private Image bar;
    public static bool flagCheckHeart = false;
    // Start is called before the first frame update
    void Start()
    {
        bar.fillAmount = playerHeart.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = playerHeart.currentHealth / 10;
        if (bar.fillAmount <= 0) flagCheckHeart = true;
        else flagCheckHeart = false;
    }
}
