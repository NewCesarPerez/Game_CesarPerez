using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

public class ChasePlayer : BaseEnemy
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
    




    private void Awake()
    {

        ChaseSpeed = 2f;
        RotationTime = 3f;
        EnemyBody = GetComponent<Rigidbody>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        //InformEnemyWaypoints();

        sightLock = false;

        enemyAnimator = GetComponent<Animator>();
        followPlayer = true;

        
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
        FollowTarget();
    

    }


    

    //Enemy Animations: Tutorial


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

    public void Impacted()
    {
        enemyAnimator.SetTrigger("Impacted");

    }
    public void Death()
    {
        enemyAnimator.SetTrigger("Death");

    }

    private Transform otherEntity;
    public float MinimumDistance = 50.0f;
    private Transform thisEntity;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            otherEntity = other.GetComponent<Transform>();
            thisEntity = GetComponent<Transform>();

            float currentDistance = Vector3.Distance(thisEntity.position, otherEntity.transform.position);
            
            if (currentDistance < 4.0f)
            {
                
                Vector3 dist = new Vector3(transform.position.x - otherEntity.transform.position.x, 0, 0);
                
                transform.position += dist*1.5f * Time.deltaTime;
            }


        }
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
            Debug.Log("Entrando al Attaque");
            EnemyAttack(Random.Range(0, 3));
            currentAttackTime = 0f;
        }
        if (Vector3.Distance(transform.position, target.transform.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }

   
}


    

