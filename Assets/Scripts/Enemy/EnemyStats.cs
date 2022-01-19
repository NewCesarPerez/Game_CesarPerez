using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    [SerializeField]  EnemyData enemyInfo;

    private float _enemyLife;

    [System.NonSerialized]  public float _EnemyMaxHealth;
    [System.NonSerialized] public float _EnemyBaseDamage;
    [System.NonSerialized] public float _EnemyAttackAwareness;

    // Start is called before the first frame update

    private void Awake()
    {
        _EnemyMaxHealth = enemyInfo.maxHealth;
        _EnemyBaseDamage = enemyInfo.baseDamage;
        _EnemyAttackAwareness = enemyInfo.AttackAwareness;
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
