using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
   

    [SerializeField]  EnemyData enemyInfo;
    [SerializeField] private Image EnemyHealthImage;
    [SerializeField] private Collider EnemyColliderOne;
    [SerializeField] private Collider EnemyColliderTwo;
    [SerializeField] private EnemyController EnemyController;
    [SerializeField] private TextMeshPro HitText;

    private float counter;
    private float _enemyLife;
    private int playerBaseDamage;
    private float counterToDie;
    private float timeToDie = 4.1f;
    private int hits;
    private float timerToCountHits;
    private float DefaultTimeToCountHits = 1f;
    

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
        hits = 0;
        animator = GetComponent<Animator>();
        timerToCountHits = DefaultTimeToCountHits;
       
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
        ResetTimerToCountHits();
        timerToCountHits -= Time.deltaTime;
        //HitText.SetText(hits.ToString());
    }

    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("HeroFoot"))
        {
            GameManager.instance.AddHits();
            timerToCountHits = DefaultTimeToCountHits;
            Debug.Log("Hits: " + GameManager.instance.GetHits());
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
            var healthImagePorcentage = _enemyLife / _EnemyMaxHealth;
            EnemyHealthImage.fillAmount = healthImagePorcentage;
            Debug.Log("Vida del enemigo pie: " + _enemyLife);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("HeroSword"))
        {

            GameManager.instance.AddHits();
            timerToCountHits = DefaultTimeToCountHits;
            Debug.Log("Hits: " + GameManager.instance.GetHits());
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

        else if (number == 2 && _enemyLife>125) 
        
        {animator.SetTrigger("KnDown");
         animator.SetTrigger("GetUp");


        }

    }
    private void EnemyDeath()
    {
       
        if (_enemyLife <= 0)
        {
            
            counterToDie -= Time.deltaTime;
            
            animator.SetBool("Death", true);
           animator.SetTrigger("DeathTrigger");
            GetComponent<EnemyController>().enabled = false;
            if (GetComponent<ChasePlayer>() == true) { 
            GetComponent<ChasePlayer>().enabled = false;
            }
            GetComponent<EnemyController>().followPlayer = false;
            GetComponent<EnemyController>().EnemyBody.velocity = transform.forward * 0f;

            if (animator.GetBool("Death") == true)
            {
                EnemyColliderOne.enabled = false;
                EnemyColliderTwo.enabled = false;
            }

            if (counterToDie <= 0)
            {
                Destroy(gameObject);
                KillingCount();
            }
        }
        

    }

    public void KillingCount()
    {
        
        GameManager.instance.AddKillingCount();
        
    }

    private void ResetTimerToCountHits()
    {
        if (timerToCountHits <= 0) {
            hits = 0;
        timerToCountHits = DefaultTimeToCountHits;
        }
    }


    public string GetHits()
    {
        return hits.ToString();
    }

    //public void DeActivateVelocity()
    //{
    //    GetComponent<EnemyController>().enabled = false;
    //    if (GetComponent<ChasePlayer>())
    //    {
    //        GetComponent<ChasePlayer>().enabled = false;
    //    }
    //    GetComponent<EnemyController>().followPlayer = false;
    //    GetComponent<EnemyController>().EnemyBody.velocity = transform.forward * 0f;
    //    GetComponent<ChasePlayer>().followPlayer = false;
    //    GetComponent<ChasePlayer>().EnemyBody.velocity = transform.forward * 0f;
    //}

    //public void ActivateVelocity()
    //{
    //    GetComponent<EnemyController>().enabled = true;
    //    if (GetComponent<ChasePlayer>() )
    //    {
    //        GetComponent<ChasePlayer>().enabled = true;
    //    }

    //    GetComponent<EnemyController>().followPlayer = true;
    //    GetComponent<EnemyController>().EnemyBody.velocity = transform.forward * 2f;
    //    GetComponent<ChasePlayer>().followPlayer = true;
    //    GetComponent<ChasePlayer>().EnemyBody.velocity = transform.forward * 2f;
    //}

    
}
