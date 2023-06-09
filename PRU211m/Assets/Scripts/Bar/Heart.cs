using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    private Animator anim;
    private bool dead;
    private Rigidbody2D myBody;
    public static bool flagCheckDie = false;
    public static bool flagCheckDieOfEnemy = false;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invunerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound; 

    public float currentHealth { get; set; }
    private bool invulnerable;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    [System.Obsolete]
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
            //SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("Grounded", true);
                anim.SetTrigger("Die");
                if (gameObject.name.Equals("Trap"))
                {
                    flagCheckDie = true;
                }
                dead = true;
                //SoundManager.instance.PlaySound(deathSound);
            }
            else
            {
                this.gameObject.SetActive(false);
                gameObject.SetActiveRecursively(false);
            }

        }
    }

    private void PlayerDie()
    {
        if (!dead)
        {
            foreach(Behaviour component in components)
            {
                component.enabled = false;
            }
            anim.SetBool("Grounded", true);
            anim.SetTrigger("Die"); // them trigger vao game
            if (gameObject.name.Equals("Enemy")) // them vao
            {
                flagCheckDie = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
