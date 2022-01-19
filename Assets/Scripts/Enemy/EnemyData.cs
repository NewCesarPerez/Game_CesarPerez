using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="ScriptableObjects/Enemy", fileName = "NewEnemyData", order =0)]
public class EnemyData : ScriptableObject
{
    public float maxHealth;
    public float AttackAwareness;
    public float baseDamage;
}
