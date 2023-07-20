using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNAttack : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float colliderDistance;
    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;


    //References
    private Animator anim;
    private bool check = false;
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
        if (PlayerInSight())
        {
            check = true;
        }
        if (check)
        {
            transform.Translate(speed, 0, 0);
            anim.SetTrigger("Run");
        }
    }
    private bool PlayerInSight()
    {
        
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * -transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LeftSide")
        {
            speed = -speed;
            transform.Translate(speed, 0, 0);
        }
        else if (collision.tag == "RightSide")
        {
            speed = -speed;
            transform.Translate(speed, 0, 0);
        }
    }
}
