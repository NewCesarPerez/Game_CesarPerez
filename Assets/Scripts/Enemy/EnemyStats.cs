using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
   

    [SerializeField]  EnemyData enemyInfo;


    private float _enemyLife;
    private int playerBaseDamage;
    private float counterToDie;
    private float timeToDie = 6;

    [System.NonSerialized]  public float _EnemyMaxHealth;
    [System.NonSerialized] public float _EnemyBaseDamage;
    [System.NonSerialized] public float _EnemyAttackAwareness;
    private Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        //playerBaseDamage = GameManager.instance._playerBaseDamage;
        playerBaseDamage = 25;
        _EnemyMaxHealth = enemyInfo.maxHealth;
        _EnemyAttackAwareness = enemyInfo.AttackAwareness;
       
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        
        _enemyLife = _EnemyMaxHealth;
        Debug.Log("Vida del enemigo: "+_enemyLife);
        counterToDie = timeToDie;
    }
    
    // Update is called once per frame
    void Update()
    {
        EnemyDeath();
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
            _enemyLife -= playerBaseDamage ;
            Debug.Log("Vida del enemigo: " + _enemyLife);
            
        }
    }

    

   private void EnemyDeath()
    {
        if (_enemyLife <= 0)
        {
            counterToDie -= Time.deltaTime;
            //GameManager.instance.AddKillingCount(1);
            animator.SetBool("Death",true);
            if(counterToDie<=0)
                Destroy(gameObject);
        }
    }

    
}
