using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMainView : MonoBehaviour
{
    [SerializeField] private Transform view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            view.position = new Vector3(view.position.x, transform.position.y, view.position.z);
        }
    }
}
