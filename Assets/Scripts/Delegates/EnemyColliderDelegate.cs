using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliderDelegate : MonoBehaviour
{

    [SerializeField] private GameObject EnemySword;
    // Start is called before the first frame update
    void Start()
    {
        EnemySwordColliderOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnemySwordColliderOff()
    {
        if (EnemySword.GetComponent<Collider>().enabled == true)
            EnemySword.GetComponent<Collider>().enabled = false;
    }
    void EnemySwordColliderOn()
    {
        if (EnemySword.GetComponent<Collider>().enabled == false)
            EnemySword.GetComponent<Collider>().enabled = true;
    }
}
