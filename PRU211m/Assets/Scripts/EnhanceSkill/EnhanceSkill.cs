using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceSkill : MonoBehaviour
{
    [SerializeField]
    private MainCharacterMovement classMovement;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            MainCharacterMovement.JumpPowerOfSkill = (float)(MainCharacterMovement.JumpPowerOfSkill * 1.2);
            gameObject.SetActive(false);
            //gameObject.SetActiveRecursively(false);
        }
    }
}
