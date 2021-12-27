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
        if (Input.GetKeyDown(KeyCode.I) && _runningAttack == 0)
        {
            Debug.Log("Ataque con salto");
            Debug.Log(_runningAttack);

            animation.SetBool("Attack", true);
            //animation.SetBool("Melee", true);
            _runningAttack = 1;


        }

        else if (Input.GetKeyUp(KeyCode.I) && _runningAttack == 1)
        {
            Debug.Log("Seteando de vuelta");
           
            animation.SetBool("Attack", true);
            //animation.SetBool("Melee", false);
            _runningAttack = 0;

        }
    }

}
