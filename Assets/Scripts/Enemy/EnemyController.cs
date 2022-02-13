using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;

public class EnemyController : BaseEnemy
{
    private float MinDistance = 1.5f;
    private float AttackAwareness = 3f;
    private int waypointsIndex;
    private float distance;
   [SerializeField] private float attackDistance=2f;
    private float chasePlayerAfterAttack = 1f;
    private float currentAttackTime;
    private float defaultAttackTime = 2f;
    private bool followPlayer, attackPlayer;
    private bool sightLock;
    private Rigidbody EnemyBody;
    [System.NonSerialized] public bool alertActivated = false;
    public event Action OnChase;
  

    [SerializeField] private List<Transform> waypoints;
   
    [SerializeField] private ChasePlayer[] enemies;
  


    private void Awake()
    {
        
        ChaseSpeed = 2f;
        RotationTime = 3f;
        EnemyBody = GetComponent<Rigidbody>();

        for (int i = 0; i < enemies.Length; i++)
        {
            OnChase += enemies[i].Chase;
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //InformEnemyWaypoints();
        waypointsIndex = 0;
        sightLock = false;
        transform.LookAt(waypoints[waypointsIndex].position);
        enemyAnimator=GetComponent<Animator>();

        //Apartir de aqui tutorial
        //followPlayer = true;
        currentAttackTime = defaultAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        
        AttackPlayer();
        //Range();
        //Patrol();
        //Chase();
        
        
    }

    private void FixedUpdate()
    {
        //FollowTarget();
        RayCastEnemyPlayer();

    }

    public void Patrol()
    {
        if (sightLock == false)
        {
            enemyAnimator.SetFloat("Velocity", 1f);
            transform.Translate(Vector3.forward * ChaseSpeed * Time.deltaTime);
        }
    }

    void IncreaseIndex()
    {
        waypointsIndex++;
        
        if (waypointsIndex >=waypoints.Count)
        {
            waypointsIndex = 0;

        }
        transform.LookAt(waypoints[waypointsIndex].position);
    }
    void Range()
    {
        
        if (sightLock == false) {
            distance = Vector3.Distance(transform.position, waypoints[waypointsIndex].position);
            
            if (distance < 1f )
            {
                
                IncreaseIndex();
            }
        }

    }

    public void Chase()
    {
        
        var distanceVector = target.position - transform.position;
        var direction = distanceVector.normalized;

        if (distanceVector.magnitude > MinDistance && safeHit != null)
            {
            sightLock = true;
                enemyAnimator.SetFloat("Velocity", 1f);
                enemyAnimator.SetBool("EnemyOnSight", true);

                transform.position += ChaseSpeed * Time.deltaTime * direction;
                LookAtPlayer();
            Alert();
            }
        else if (distanceVector.magnitude <= MinDistance && safeHit != null)
            {

            enemyAnimator.SetBool("EnemyOnSight", true);
            enemyAnimator.SetFloat("Velocity", 0f);
            enemyAnimator.SetBool("AttackPlayer", true);

            sightLock = true;
            LookAtPlayer();
            Alert();

        }

        else
        {
            enemyAnimator.SetBool("EnemyOnSight", false);
            enemyAnimator.SetBool("AttackPlayer", false);
            sightLock = false;
            transform.LookAt(waypoints[waypointsIndex].position);
        }

        //Ver pq no funciona

        //if (distanceVector.magnitude <= AttackAwareness && safeHit == null && enemyAnimator.GetBool("EnemyOnSight")==true) 
        //{
        //    Debug.Log("Entrando al awareness");
        //    enemyAnimator.SetFloat("Velocity", 1f);
        //    transform.position += ChaseSpeed * Time.deltaTime * direction;
        //    LookAtPlayer();
        //}
    }

    public void InformEnemyWaypoints()
    {
       for (int i=0; i < waypoints.Count; i++)
        {
            Debug.Log("Punto de patrullaje N° " + i + ": " + waypoints[i].position);
        }
    }
    
    public void Alert()
    {
        OnChase?.Invoke();
        alertActivated=true;
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

    protected override void RayCastEnemyPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(eyesTransform.position, transform.forward, out hit, maxDistance, layerToCollide);
        safeHit = hit.collider;
        
        if (safeHit != null)
        {

            followPlayer = true;



        }
        FollowTarget();
    }
    
void FollowTarget()
    {
        var distanceWithPlayer = Vector3.Distance(transform.position, target.position);
        
        if (!followPlayer) return;
     
        if (distanceWithPlayer > attackDistance)
        {
            transform.LookAt(target);
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
            EnemyAttack(Random.Range(0,3));
            currentAttackTime = 0f;
        }
        if(Vector3.Distance(transform.position, target.position) > attackDistance + chasePlayerAfterAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }
}


