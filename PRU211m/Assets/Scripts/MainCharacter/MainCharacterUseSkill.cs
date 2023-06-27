using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterUseSkill : MonoBehaviour
{
    [SerializeField]private float useSkillCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs; 
    private Animator anim;
    private MainCharacterMovement mainCharacterMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        mainCharacterMovement = GetComponent<MainCharacterMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && cooldownTimer > useSkillCooldown && mainCharacterMovement.CanAttack())
        {
            CastSkill();
        }
        else
        {
            anim.ResetTrigger("Cast");
        }

        cooldownTimer += Time.deltaTime;
    }

    private void CastSkill()
    {
        anim.SetTrigger("Cast");
        cooldownTimer = 0;

        fireballs[FindSkillProjecttile()].transform.position = firepoint.position;
        fireballs[FindSkillProjecttile()].GetComponent<Pojectie>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindSkillProjecttile()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                
                return i;
            }
        }
        return 0;
    }
}
