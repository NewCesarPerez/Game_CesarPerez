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
    [SerializeField] private Transform eyesTransform;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private float maxDistance;
    public void Chase()
    {
        var distanceVector = target.position - transform.position;
        var direction = distanceVector.normalized;

        

  

        if (distanceVector.magnitude > MinDistance&&safeHit!=null)
        {
            enemyAnimator.SetFloat("Velocity", 1f);
            
            transform.position += ChaseSpeed * Time.deltaTime * direction;
        }
        else
        {
            
            enemyAnimator.SetFloat("Velocity", 0f);
        }
        LookAtPlayer();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

}
