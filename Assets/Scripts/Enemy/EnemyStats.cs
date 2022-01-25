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
    private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        _EnemyMaxHealth = enemyInfo.maxHealth;
        _EnemyAttackAwareness = enemyInfo.AttackAwareness;
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        _enemyLife = _EnemyMaxHealth;
        Debug.Log("Vida del enemigo: "+_enemyLife);
    }
    
    // Update is called once per frame
    void Update()
    {
        //KillingCount();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colision espada player");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colision espada player 2");
            animator.SetTrigger("Impacted");
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
