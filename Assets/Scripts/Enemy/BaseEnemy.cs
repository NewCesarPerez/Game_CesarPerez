using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{

    protected int maxLife;
    protected int currentLife;
    protected float ChaseSpeed;
    protected float RotationTime;
    protected Collider safeHit;
    protected Animator enemyAnimator;
    [SerializeField] protected Transform target;
    [SerializeField] protected Transform eyesTransform;
    [SerializeField] protected LayerMask layerToCollide;
    [SerializeField] protected float maxDistance;


    public virtual void LookAtPlayer()
    {
        Quaternion newRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, RotationTime * Time.deltaTime);
    }

    protected virtual void RayCastEnemyPlayer()
    {
        RaycastHit hit;
        Physics.Raycast(eyesTransform.position, transform.forward, out hit, maxDistance, layerToCollide);
        safeHit = hit.collider;
        if (safeHit != null)
        {

            Debug.Log("Player en la mira");


        }

      
    }

    

}
