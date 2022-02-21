using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordDamage : MonoBehaviour
{
    [SerializeField] EnemyData enemyInfo;
    [System.NonSerialized] public float _EnemyBaseDamage;
    private Animator _playerAnimator;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        _EnemyBaseDamage = enemyInfo.baseDamage;
        //Debug.Log("SwordDamage: " + _EnemyBaseDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
