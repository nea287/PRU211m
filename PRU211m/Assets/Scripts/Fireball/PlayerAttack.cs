using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private GameObject[] fireball;
    [SerializeField] private Transform firePoints;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && cooldownTimer > attackCooldown
           && Time.timeScale > 0)
            Attack();
        cooldownTimer += Time.deltaTime;
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if (!fireball[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        cooldownTimer = 0;
        fireball[FindFireball()].transform.position = firePoints.position;
        fireball[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        audioSource.PlayOneShot(attackSound);

    }
}
