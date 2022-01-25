using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChasePlayer : BaseEnemy
{
    private float MinDistance = 1.5f;
    private float AttackAwareness = 3f;
    private int waypointsIndex;
    private float distance;
    private bool sightLock;
    
    public event Action OnChase;
    [SerializeField] private List<Transform> waypoints;
    



    private void Awake()
    {
        ChaseSpeed = 2f;
        RotationTime = 3f;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyAnimator.SetBool("EnemyOnSight", false);
        enemyAnimator.SetBool("AttackPlayer", false);
        sightLock = false;
    }

    // Update is called once per frame
    void Update()
    {
        RayCastEnemyPlayer();
        Patrol();
        Range();
    }
    public void Patrol()
    {
        var distanceVector = target.position - transform.position;
        var direction = distanceVector.normalized;
        if (sightLock == false && safeHit == null)
        {
            enemyAnimator.SetFloat("Velocity", 1f);
            transform.Translate(Vector3.forward * ChaseSpeed * Time.deltaTime);
            transform.LookAt(waypoints[waypointsIndex].position);
        }

        else if (distanceVector.magnitude > MinDistance && safeHit != null)
        {
            sightLock = true;
            enemyAnimator.SetFloat("Velocity", 1f);
            enemyAnimator.SetBool("EnemyOnSight", true);

            transform.position += ChaseSpeed * Time.deltaTime * direction;
            LookAtPlayer();
        }
        else if (distanceVector.magnitude <= MinDistance && safeHit != null)
        {

            enemyAnimator.SetBool("EnemyOnSight", true);
            enemyAnimator.SetFloat("Velocity", 0f);
            enemyAnimator.SetBool("AttackPlayer", true);

            sightLock = true;
            LookAtPlayer();

        }
        
    }

    void IncreaseIndex()
    {
        waypointsIndex++;
        
        if (waypointsIndex >= waypoints.Count)
        {
            waypointsIndex = 0;

        }
        transform.LookAt(waypoints[waypointsIndex].position);
    }
    void Range()
    {

        if (sightLock == false)
        {
            distance = Vector3.Distance(transform.position, waypoints[waypointsIndex].position);

            if (distance < 1f)
            {

                IncreaseIndex();
            }
        }

    }

    public void Chase()
    {
        var distanceVector = target.position - transform.position;
        var direction = distanceVector.normalized;

        if (distanceVector.magnitude > MinDistance)
        {
            sightLock = true;
            enemyAnimator.SetFloat("Velocity", 1f);
            enemyAnimator.SetBool("EnemyOnSight", true);

            transform.position += ChaseSpeed * Time.deltaTime * direction;
            LookAtPlayer();
        }
        else if (distanceVector.magnitude <= MinDistance)
        {

            enemyAnimator.SetBool("EnemyOnSight", true);
            enemyAnimator.SetFloat("Velocity", 0f);
            enemyAnimator.SetBool("AttackPlayer", true);
            sightLock = true;
            LookAtPlayer();

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

}
