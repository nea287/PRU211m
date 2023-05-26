using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            speed = -speed;
            transform.Translate(0, speed, 0);
        }
        else if (collision.tag == "Ground")
        {
            speed = -speed;
            transform.Translate(0, speed, 0);
        }
    }
}