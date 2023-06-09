using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : MonoBehaviour
{
    [SerializeField] private Transform mainView;
    [SerializeField] private float speed;
    Vector3 vetical = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(new Vector3(mainView.position.x, transform.position.y, transform.position.z),
            new Vector3(mainView.position.x, transform.position.y, transform.position.z), ref vetical, speed);

    }
}
