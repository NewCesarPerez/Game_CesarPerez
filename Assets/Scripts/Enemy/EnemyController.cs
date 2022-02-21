using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyController : BaseEnemy
{
   
    private float MinDistance = 1.5f;
    private float AttackAwareness = 3f;
    
    private float distance;
   
    private float chasePlayerAfterAttack = 1f;
    private float currentAttackTime;
    private float defaultAttackTime = 2f;
    private bool followPlayer, attackPlayer;
    private bool sightLock;
    private Rigidbody EnemyBody;
    [System.NonSerialized] public bool alertActivated = false;
    public event Action OnChase;
   
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private ChasePlayer[] enemies;
    [SerializeField] private GameObject FlameParticleSystem;
    [SerializeField] private GameObject BattleMusic;
    [SerializeField] private GameObject AmbientMusic;
    [SerializeField] private GameObject FlameSFX;




    public UnityEvent OnEnemiesInst;


    private void Awake()
    {
        
        ChaseSpeed = 2f;
        RotationTime = 3f;
        EnemyBody = GetComponent<Rigidbody>();

      
    }
    // Start is called before the first frame update
    void Start()
    {
        AmbientMusic.SetActive(true);
        if(SceneManager.GetActiveScene().name!="LevelThree")
        BattleMusic.SetActive(false);

        else if(SceneManager.GetActiveScene().name == "LevelThree")
            BattleMusic.SetActive(true);
        SpellAttackOff();
        SpellSFXOff();


        enemyAnimator =GetComponent<Animator>();

        //Apartir de aqui tutorial
        //followPlayer = true;
        currentAttackTime = defaultAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        AttackPlayer();
       
        
        
    }

    private void FixedUpdate()
    {
        //FollowTarget();
        RayCastEnemyPlayer();

    }

    

    

    
    public void EnemyAttack(int attack)
    {
        if (attack == 0)
        {
            enemyAnimator.SetTrigger("Slash");
        }

        if (attack == 1)
        {
            enemyAnimator.SetTrigger("Inlash");
        }

        if (attack == 2)
        {
            enemyAnimator.SetTrigger("HvyAtt");
        }

        if (attack == 3) 
        {
            enemyAnimator.SetTrigger("Spell");

        }
    }

    public void PlayIdleAnim()
    {
        enemyAnimator.Play("EnemyIdle");
    }

    public void Stunned()
    {
        enemyAnimator.SetTrigger("KnDown");
    }
    
    public void GetUp()
    {
        enemyAnimator.SetTrigger("GetUp");
    }

   
    public void Death()
    {
        enemyAnimator.SetTrigger("Death");

    }

    protected override void RayCastEnemyPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(eyesTransform.position, transform.forward, out hit, maxDistance, layerToCollide);
        safeHit = hit.collider;
        
        if (safeHit != null &&followPlayer==false)
        {
            AmbientMusic.SetActive(false);
            BattleMusic.SetActive(true);
            followPlayer = true;
            OnEnemiesInst?.Invoke();
          
        }

        if (SceneManager.GetActiveScene().name == "LevelThree")
        {
            followPlayer = true;
        }
        

        FollowTarget();
    }
    
void FollowTarget()
    {
        
        var distanceWithPlayer = Vector3.Distance(transform.position, target.transform.position);
        
        if (!followPlayer) return;
     
        if (distanceWithPlayer > attackDistance)
        {
            transform.LookAt(target.transform);
            EnemyBody.velocity = transform.forward * ChaseSpeed;
            enemyAnimator.SetFloat("Velocity", 1f);
            


        }
        else if (distanceWithPlayer <= attackDistance)
        {
           
            EnemyBody.velocity = Vector3.zero;
            enemyAnimator.SetFloat("Velocity", 0f);
            followPlayer = false;
            attackPlayer = true;
        }

    }

    void AttackPlayer()
    {
        
        if (!attackPlayer) return;
        currentAttackTime += Time.deltaTime;
        if (currentAttackTime > defaultAttackTime)
        {
           
            if (SceneManager.GetActiveScene().name != "LevelThree")
            {
            EnemyAttack(Random.Range(0,3));
            }

            else if (SceneManager.GetActiveScene().name == "LevelThree")
            {
                EnemyAttack(Random.Range(0, 4));
            }
            currentAttackTime = 0f;
        }
        if(Vector3.Distance(transform.position, target.transform.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

    void SpellAttackOn()
    {
        if(SceneManager.GetActiveScene().name=="LevelThree")
        FlameParticleSystem.SetActive(true);
       
        
    }

    void SpellAttackOff()
    {
        if (SceneManager.GetActiveScene().name == "LevelThree")
            FlameParticleSystem.SetActive(false);
    }

    void SpellSFXOn()
    {
        if (SceneManager.GetActiveScene().name=="LevelThree")
        FlameSFX.SetActive(true);
    }

    void SpellSFXOff()
    {
        if (SceneManager.GetActiveScene().name == "LevelThree")
            FlameSFX.SetActive(false);
    }
}


