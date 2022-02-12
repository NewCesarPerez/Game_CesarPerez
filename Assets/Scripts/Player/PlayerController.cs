using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerRotateSpeed;
    [SerializeField] private Transform eyesTransform;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private float maxDistance;
    [SerializeField] private Image healthImage;
    [SerializeField] private EnemyData enemyInfo;
    
    private float _maxTime;
    private float _runningTime;
    
    private float timeToGetHit = 0.5f;
    private float _maxLife = 100f;
    private float _currentLife;
    private float _lowLife = 50f;
    
    public UnityEvent OnEmergencyHeart;
    private Animator animator;
    private int contacto = 0;

    private void Awake()
    {
        

    }


    // Start is called before the first frame update
    void Start()
    {
       
        _currentLife = _maxLife;
        _maxTime = 4f;
        _runningTime = 0;
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToGetHit -= Time.deltaTime;
        MovePlayer();
        Run();
        PlayerLowLife();
        PlayerDeath();
        PlayerRevival();
        TestingHearts();
        //RayCastPlayerEnemy();
        
    }


    private void MovePlayer()
    {
        Vector3 dir;
        Vector3 dirRotate;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        dir = new Vector3(0, 0, v).normalized;
        dirRotate= new Vector3(0, h, 0).normalized;

        Vector3 rotate = dirRotate * PlayerRotateSpeed * Time.deltaTime;
        transform.Rotate(rotate);

        transform.Translate(dir * PlayerSpeed * Time.deltaTime);
        animator.SetFloat("Velocity", dir.magnitude);
        

        if (v < 0)
        {
            
            animator.SetBool("WalkBack", true);
        }
        else
        {
            animator.SetBool("WalkBack", false);
        }

    }

    private void Run()
    {
        
        if (Input.GetKey(KeyCode.LeftShift)&&animator.GetBool("WalkBack")==false)
        {
            animator.SetBool("Run", true);
            
            PlayerSpeed = 6f;
            PlayerRotateSpeed = 100f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Run", false);
            
            PlayerSpeed = 2f;
            PlayerRotateSpeed = 65f;
            
        }
       
    }

    void RayCastPlayerEnemy()
    {
        RaycastHit hit;
        Physics.Raycast(eyesTransform.position,transform.forward, out hit, maxDistance, layerToCollide );
        if (hit.collider != null)
        {
            Debug.Log("En la mira");
            animator.SetBool("Melee", true);
        }

        else
        {
            Debug.Log("No te veo");
            
        }
    }

    void PlayerLowLife()
    {
       
        if (_currentLife < _lowLife)
        {
            Debug.Log("Etrando a Player low life");
            OnEmergencyHeart?.Invoke();
        }
    }
    void PlayerDeath()
    {
        if (_currentLife <= 0)
        {
            animator.SetBool("Death",true);
        }
    }

    void PlayerRevival()
    {
        if (animator.GetBool("Death") == true && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            

            animator.SetBool("Death", false);
            animator.SetTrigger("Revival");
            _currentLife = _maxLife;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemySword")&&timeToGetHit<=0 &&animator.GetBool("Block")==false)
        {
            
            
            float enemyBaseDamage = collision.gameObject.GetComponent<EnemySwordDamage>()._EnemyBaseDamage;
            Debug.Log("Daño base enemigo " + enemyBaseDamage);

            _currentLife -= enemyBaseDamage;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;

            Debug.Log("Restando hp: " + _currentLife);
            if (animator.GetBool("Death") == false)
            {
                animator.SetTrigger("Impacted");
                timeToGetHit = 1f;
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("Heart"))
        {
            
            _currentLife = _maxLife;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;
            Debug.Log("Entrando en el trigger: Vida " + _currentLife);


        }
    }

    void TestingHearts()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _currentLife = 40;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;
            Debug.Log(_currentLife);
        }
    }

}
