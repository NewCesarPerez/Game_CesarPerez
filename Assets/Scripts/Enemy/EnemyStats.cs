using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
   

    [SerializeField]  EnemyData enemyInfo;
    [SerializeField] private Image EnemyHealthImage;

    private float counter;
    private float _enemyLife;
    private int playerBaseDamage;
    private float counterToDie;
    private float timeToDie = 5;

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
         counter = 0.2f;
        _enemyLife = _EnemyMaxHealth;
        //Debug.Log("Vida del enemigo: "+_enemyLife);
        counterToDie = timeToDie;
    }
    
    // Update is called once per frame
    void Update()
    {
        EnemyDeath();
        //KillingCount();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("HeroFoot"))
        {
            Debug.Log("Colision pie");
            if (SceneManager.GetActiveScene().name != "LevelThree")
            {
                Impacted(Random.Range(0, 5));
            }

            else if (SceneManager.GetActiveScene().name == "LevelThree")
            {
                Impacted(Random.Range(0, 10));
            }
                _enemyLife -= playerBaseDamage;
            Debug.Log("Vida del enemigo pie: " + _enemyLife);
        }

        
        if (other.gameObject.layer == LayerMask.NameToLayer("HeroSword"))
        {
            if (SceneManager.GetActiveScene().name != "LevelThree")
            {
                Impacted(Random.Range(0, 5));
            }

            else if (SceneManager.GetActiveScene().name == "LevelThree")
            {
                Impacted(Random.Range(0, 10));
            }
            _enemyLife -= playerBaseDamage;
            var healthImagePorcentage = _enemyLife / _EnemyMaxHealth;
            EnemyHealthImage.fillAmount = healthImagePorcentage;
            

        }
    }


    public void Impacted(int number)
    {
        if (number == 1) animator.SetTrigger("Impacted");

        else if (number == 2) 
        
        {animator.SetTrigger("KnDown");
         animator.SetTrigger("GetUp");


        }

    }
    private void EnemyDeath()
    {
       
        if (_enemyLife <= 0)
        {
            
            counterToDie -= Time.deltaTime;
            GameManager.instance.AddKillingCount();
            animator.SetBool("Death", true);
           animator.SetTrigger("DeathTrigger");
            
           

            
            
            if(counterToDie<=0)
                Destroy(gameObject);
        }
        

    }
    

    
}
