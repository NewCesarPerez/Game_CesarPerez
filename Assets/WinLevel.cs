using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField]private GameObject bridgeCollider;
   
    void Update()
    {

        LevelWin();
    }

    void LevelWin()
    {
        if (FindObjectOfType<EnemyController>() == null)
        {
            bridgeCollider.GetComponent<Collider>().isTrigger = true;

        }
    }
}
