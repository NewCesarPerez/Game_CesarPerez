using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateEnemies : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private Transform [] enemyPosition;
    [SerializeField] private Transform[] activateEnemies;

    private bool allowInst = true;


    //public void InstEnemies()
    //{

    //    Debug.Log("Entrando a inst enemies");
    //    Debug.Log(enemyPosition.Length);
    //    if (allowInst == true)
    //    {
    //        for (int i = 0; i < enemyPosition.Length; i++)

    //        {
    //            Debug.Log("Entrando a inst enemies");

    //            allowInst = false;

    //            Instantiate(enemyPrefab, enemyPosition[i].position, Quaternion.identity);

    //        }
    //    }

    //}

    public void InstEnemies()
    {

        Debug.Log("Entrando a inst enemies");
        
        if (allowInst == true)
        {
            for (int i = 0; i < activateEnemies.Length; i++)

            {
                Debug.Log("Entrando a activate enemies");

                allowInst = false;

                activateEnemies[i].gameObject.SetActive(true);
                

            }
            
        }

    }


}
