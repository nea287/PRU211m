using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTerrainGround : MonoBehaviour
{
    [SerializeField] private float pushForce;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();    

    }
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
        
        Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

        if(otherRigidbody != null)
        {
            anim.SetTrigger("JumpTerrain");
            Vector2 forceDirection = collision.contacts[0].point - (Vector2)transform.position;
            forceDirection = forceDirection.normalized;
            otherRigidbody.AddForce(forceDirection * pushForce, ForceMode2D.Impulse);
        }
    }

}
