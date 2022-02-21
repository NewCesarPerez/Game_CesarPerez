using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersDelegates : MonoBehaviour
{

    [SerializeField] private GameObject HeroSword, HeroSwordHandler, HeroFoot;
    

    // Start is called before the first frame update
    void Start()
    {
        HeroFootColliderOff();
        HeroSwordColliderOff();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HeroFootColliderOff()
    {
        if (HeroFoot.GetComponent<Collider>().enabled == true)
            HeroFoot.GetComponent<Collider>().enabled = false;
    }
    void HeroFootColliderOn()
    {
        if(HeroFoot.GetComponent<Collider>().enabled == false) 
            HeroFoot.GetComponent<Collider>().enabled = true;
    }

    void HeroSwordColliderOff()
    {
        if (HeroSword.GetComponent<Collider>().enabled == true)
            HeroSword.GetComponent<Collider>().enabled = false;

        if (HeroSwordHandler.GetComponent<Collider>().enabled == true)
            HeroSwordHandler.GetComponent<Collider>().enabled = false;
    }
    void HeroSwordColliderOn()
    {
        if (HeroSword.GetComponent<Collider>().enabled == false)
            HeroSword.GetComponent<Collider>().enabled = true;

        if (HeroSwordHandler.GetComponent<Collider>().enabled == false)
            HeroSwordHandler.GetComponent<Collider>().enabled = true;
    }

    
}
