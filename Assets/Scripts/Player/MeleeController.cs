using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private bool input;
    private Animator animation;
    private int Sword;
    private int _runningAttack;
    private float swordColliderTimer;

    [SerializeField] private GameObject swordCollider;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        swordCollider.SetActive(false);
        swordColliderTimer = 1.5f;
        
        animation = GetComponent<Animator>();
        Sword = 0;
        _runningAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        swordColliderTimer -= Time.deltaTime;

        
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
           
            animation.SetBool("SwordIn", false);
            animation.SetBool("Melee", true);
            Sword = 1;
            

        }

        else if (Input.GetKeyDown(KeyCode.K) && Sword == 1)
        {
            animation.SetBool("SwordIn", true);
            animation.SetBool("Melee", false);
            Sword = 0;
            
        }


    }

    void RunnigAttack()
    {
        if (Input.GetKeyDown(KeyCode.I)&&animation.GetBool("Melee")==true)
        {


            swordCollider.SetActive(true);
            animation.SetTrigger("Attack");
            //animation.SetBool("Melee", true);
            _runningAttack = 1;

            swordColliderTimer = 1.2f;
        }

        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
        }


    }

    
    void SlashThreeAttack()
    {
        if (Input.GetKeyDown(KeyCode.J) && animation.GetBool("Melee") == true)
        {
            swordCollider.SetActive(true);
            animation.SetTrigger("S3");
            swordColliderTimer = 1.5f;

        }


        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
        }



    }
    
    

    void JumpSlashAttack()
    {
       
        if (Input.GetKeyDown(KeyCode.L) && animation.GetBool("Melee") == true)
        {
           
            swordCollider.SetActive(true);
            
            animation.SetTrigger("J&Slash");
            
            swordColliderTimer = 1.5f;

        }
       

        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
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

    void KickAttack()
    {
        if (Input.GetKeyDown(KeyCode.L) && animation.GetBool("Melee") == false)
        {



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
}
