using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float MinDistance = 3f;
    private float ChaseSpeed=2f;
    private float RotationTime=3f;
    private Animator enemyAnimator;
    private Collider safeHit;
    private int waypointsIndex;
    private float distance;
    private bool sightLock;
    [SerializeField] private Transform eyesTransform;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform[] waypoints;
    



    

    // Start is called before the first frame update
    void Start()
    {
        waypointsIndex = 0;
        sightLock = false;
        transform.LookAt(waypoints[waypointsIndex].position);
        enemyAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Range();
        Patrol();
        Chase();
        RayCastEnemyPlayer();
    }

    public void LookAtPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RotationTime * Time.deltaTime);
    }

    void RayCastEnemyPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(eyesTransform.position, transform.forward, out hit, maxDistance, layerToCollide);
        safeHit = hit.collider;
        if (hit.collider != null)
        {
            
            Debug.Log("Player en la mira");
            
            
        }

        else
        {
            Debug.Log("No veo al player");

        }
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
        Debug.Log(waypointsIndex);
        if (waypointsIndex >=waypoints.Length)
        {
            waypointsIndex = 0;

        }
        transform.LookAt(waypoints[waypointsIndex].position);
    }
    void Range()
    {
        
        if (sightLock == false) {
            distance = Vector3.Distance(transform.position, waypoints[waypointsIndex].position);
            Debug.Log("Incrementando index");
            if (distance < 1f )
            {
                Debug.Log("Incrementando index");
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
            }
        else if (distanceVector.magnitude <= MinDistance && safeHit != null)
            {

            enemyAnimator.SetBool("EnemyOnSight", true);
            enemyAnimator.SetFloat("Velocity", 0f);
            sightLock = true;
            LookAtPlayer();
        }

        else
        {
            enemyAnimator.SetBool("EnemyOnSight", false);
            sightLock = false;
            transform.LookAt(waypoints[waypointsIndex].position);
        }


    }

}