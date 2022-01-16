using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private bool input;
    private Animator animation;
    private int Sword;
    private int _runningAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
        Sword = 0;
        _runningAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SwordOut();
        RunnigAttack();
        KickTwo();
        SlashThreeAttack();
        KickThree();
        KickAttack();
        JumpSlashAttack();
        Blocking();
    }

    void SwordOut()
    {
        
        if (Input.GetKeyDown(KeyCode.K)&&Sword==0)
        {
            Debug.Log("Espada 0");
            Debug.Log(Sword);

            animation.SetBool("SwordIn", false);
            animation.SetBool("Melee", true);
            Sword = 1;
            

        }

        else if (Input.GetKeyDown(KeyCode.K) && Sword == 1)
        {
            Debug.Log("Espada 1");
            Debug.Log(Sword);
            animation.SetBool("SwordIn", true);
            animation.SetBool("Melee", false);
            Sword = 0;
            
        }


    }

    void RunnigAttack()
    {
        if (Input.GetKeyDown(KeyCode.I)&&animation.GetBool("Melee")==true)
        {
            Debug.Log("Ataque con salto");
            Debug.Log(_runningAttack);

            animation.SetTrigger("Attack");
            //animation.SetBool("Melee", true);
            _runningAttack = 1;


        }

        
    }

    
    void SlashThreeAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && animation.GetBool("Melee") == true)
        {
            animation.SetTrigger("S3");
            
        }
        

    }

    
    void KickAttack()
    {
        if (Input.GetKeyDown(KeyCode.L) && animation.GetBool("Melee") == false)
        {
            Debug.Log("Kick");


            animation.SetTrigger("KickAttack");
        }

    }
    void KickTwo()
    {
        if (Input.GetKeyDown(KeyCode.J) && animation.GetBool("Melee") == false)
        {
            animation.SetTrigger("KickTwo");
        }
    }
    void KickThree()
    {
        if (Input.GetKeyDown(KeyCode.I) && animation.GetBool("Melee") == false)
        {
            animation.SetTrigger("KickThree");
        }
    }

    void JumpSlashAttack()
    {
        if (Input.GetKeyDown(KeyCode.L) && animation.GetBool("Melee") == true)
        {
            Debug.Log("JumpSlash");


            animation.SetTrigger("J&Slash");
        }

    }

    void Blocking()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animation.SetBool("Blocking", true);
        }

        else if (Input.GetKeyUp(KeyCode.P))
        {
            animation.SetBool("Blocking", false);
        }
    }

}
