using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{

    public enum ComboState
    {
        NONE,
        SLASH_ATTACK,
        SLIDE_ATTACK,
        TWO_STRIKE,
        KICK,
       

    }

    private bool activateTimerToReset;
    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;
    private ComboState currentComboState;
    [SerializeField] private Transform SwordReference;
    [SerializeField] private Transform SwordParentRun;
    [SerializeField] private Transform SwordParentBlocking;


    private Vector3 SwordPosition;
    private bool input;
    private Animator animation;
    private int Sword;
    private int _runningAttack;
    private float swordColliderTimer;

    [SerializeField] private GameObject swordCollider;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        currentComboState = ComboState.NONE;
        
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


        ComboAttacks();
        ResetComboState();

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

    void TwoStrikes()
    {
        
            swordCollider.SetActive(true);
            animation.SetTrigger("TwoStrikes");
            swordColliderTimer = 1.2f;
        

        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
        }


    }

    
    void SlashAttack()
    {
        
            swordCollider.SetActive(true);
            animation.SetTrigger("SlashAttack");
            swordColliderTimer = 1.5f;

        


        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
        }



    }
    
    

    void SlideAttack()
    {
               
            swordCollider.SetActive(true);
            
            animation.SetTrigger("SlideAttack");
        
            
            swordColliderTimer = 1.5f;

        
       

        if (swordColliderTimer <= 0 && swordCollider == true)
        {
            swordCollider.SetActive(false);
        }
        



    }

    void Blocking()
    {
        float pos;
        SwordPosition=new Vector3 (0.0962f,-0.0023f, -0.08050466f);
        if (Input.GetKeyDown(KeyCode.P))
        {
            animation.SetBool("Block", true);

            //SwordReference.SetParent(SwordParentBlocking);
        }

        else if (Input.GetKeyUp(KeyCode.P))
        {
            animation.SetBool("Block", false);
            //SwordReference.SetParent(SwordParentRun);
            //SwordReference.transform.localPosition = SwordPosition;

        }
    }

    void KickOne()
    {
       

            animation.SetTrigger("Kick");
        

    }
    void KickTwo()
    {
       
            animation.SetTrigger("KickTwo");
        
    }
    void KickThree()
    {
       
            animation.SetTrigger("KickThree");
        
    }

    void ComboAttacks()
    {
        if (currentComboState == ComboState.TWO_STRIKE)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            
            currentComboState++;
            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (currentComboState == ComboState.SLASH_ATTACK)
            {
                SlashAttack();
            }
            if (currentComboState == ComboState.SLIDE_ATTACK)
            {
                SlideAttack();

            }
            if (currentComboState == ComboState.TWO_STRIKE)
            {
                TwoStrikes();
            }
            Debug.Log(currentComboState);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentComboState == ComboState.TWO_STRIKE)
            {
                return;
            }
            if (currentComboState==ComboState.NONE|| currentComboState == ComboState.SLASH_ATTACK|| currentComboState == ComboState.SLIDE_ATTACK)
            {
                currentComboState = ComboState.KICK;
                Debug.Log(currentComboState);
            }

            activateTimerToReset = true;
            current_Combo_Timer = default_Combo_Timer;
            if (currentComboState == ComboState.KICK)
            {
                KickTwo();
            }
            
        }





    }
    void ResetComboState()
    {
        if (activateTimerToReset == true)
        {
            current_Combo_Timer -= Time.deltaTime;
            if (current_Combo_Timer <= 0f)
            {
                currentComboState = ComboState.NONE;
                activateTimerToReset = false;

            }

        }
    }

}
