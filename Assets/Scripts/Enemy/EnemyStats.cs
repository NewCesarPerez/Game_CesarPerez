using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    private int _enemyLife;
    // Start is called before the first frame update

    private void Awake()
    {
        _enemyLife = 30;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KillingCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword") && _enemyLife>0)
        {
            _enemyLife -= 10;

        }
    }

    private void KillingCount()
    {
        if (_enemyLife == 0)
        {
            GameManager.instance.AddKillingCount(1);
            Destroy(gameObject);
        }
    }

    
}
