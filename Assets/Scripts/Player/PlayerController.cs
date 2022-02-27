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
    [SerializeField] private GameObject blockingHitEffect;
    [SerializeField] private GameObject swordBlockSFX;

    private float _maxTime;
    private float _runningTime;
    
    private float timeToGetHit = 0.6f;
    private float timeToGetBurn = 1f;
    private float setTimerToActivateBlockSFX = 0.3f;
    private float setTimerToDeActivateBlockSFX = 0.3f;
    private float timeToDeActivateBlockSFX;
    private float timeToActivateBlockSFX;


    private float _maxLife = 200f;
    private float _currentLife;
    private float _lowLife;
    
    public UnityEvent OnEmergencyHeart;
    private Animator animator;
    private int contacto = 0;

    private void Awake()
    {

        swordBlockSFX.SetActive(false);
        timeToDeActivateBlockSFX = setTimerToDeActivateBlockSFX;
        timeToActivateBlockSFX = setTimerToActivateBlockSFX;
    }


    // Start is called before the first frame update
    void Start()
    {
       
        _currentLife = _maxLife;
        _lowLife = _maxLife / 2;
        _maxTime = 4f;
        _runningTime = 0;

        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToGetHit -= Time.deltaTime;
        timeToGetBurn -= Time.deltaTime;
        timeToDeActivateBlockSFX -= Time.deltaTime;
        timeToActivateBlockSFX -= Time.deltaTime;

        
        MovePlayer();
        DeActivateBlockSFX();
        Run();
        PlayerLowLife();
        PlayerDeath();
        PlayerRevival();
        TestingHearts();
        Debug.Log(GameManager.instance.GetPlayerDeathCount());
        //RayCastPlayerEnemy();
        
    }

    public float GetCurrentLife()
    {
        return _currentLife;
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

        if (h < 0 && v == 0)
        {
            animator.SetBool("TurnLeft", true);
        }
        else if ((h > 0 && v == 0))
        {
            animator.SetBool("TurnRight", true);
        }
        else { animator.SetBool("TurnRight", false); animator.SetBool("TurnLeft", false); }
    }

    private void Run()
    {
        
        if (Input.GetKey(KeyCode.LeftShift)&&animator.GetBool("WalkBack")==false && animator.GetBool("TurnRight") == false && animator.GetBool("TurnLeft") == false)
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

        
        if (collision.gameObject.layer==LayerMask.NameToLayer("EnemySword") && timeToGetHit<=0 &&animator.GetBool("Block")==false)
        {
            
            
            float enemyBaseDamage = collision.gameObject.GetComponent<EnemySwordDamage>()._EnemyBaseDamage;

           

            _currentLife -= enemyBaseDamage;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;

            Debug.Log("Restando hp: " + _currentLife);
            if (animator.GetBool("Death") == false)
            {
                animator.SetTrigger("Impacted");
                timeToGetHit = 0.6f;
            }
        }


        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemySword") && animator.GetBool("Block") == true)
        {
            
            
            if (timeToActivateBlockSFX <= 0) {
                Debug.Log("Entrando al activador");
                swordBlockSFX.SetActive(true);
                timeToActivateBlockSFX = setTimerToActivateBlockSFX;
                timeToDeActivateBlockSFX = setTimerToDeActivateBlockSFX;
                
               
                
            }

            //if (timeToDeActivateBlockSFX <= 0)
            //{
            //    swordBlockSFX.SetActive(false);
            //    timeToDeActivateBlockSFX = setTimerToDeActivateBlockSFX;
            //}

            
            //timeToDeActivateBlockSFX = setTimerToDeActivateBlockSFX;
            Debug.Log("Bloqueo y chispas " + collision.gameObject.layer);
            ContactPoint contact2 = collision.contacts[0];
            Vector3 SparkPosition = contact2.point;
            SparkPosition.y -= 0.5f;
            SparkPosition.z += 0.3f;
            Instantiate(blockingHitEffect, SparkPosition, Quaternion.identity);
            var blockHit=FindObjectOfType<blockHit>();
            Destroy(blockHit.gameObject, 0.5f);
       
        }
        //swordBlockSFX.SetActive(false);





    }

    public void PlayerDeathCount()
    {
        GameManager.instance.AddPlayerDeathCount();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyFlame") && timeToGetBurn <= 0 && animator.GetBool("Block") == false&&_currentLife>0)
        { 
           
        
            Debug.Log("Player colisiona con la layer " + other.gameObject.layer);
            Debug.Log("Quemado");

            float enemyBaseDamage = 60;

            _currentLife -= enemyBaseDamage;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;

            Debug.Log("Restando hp: " + _currentLife);
            
                animator.SetTrigger("Impacted");
                timeToGetBurn = 1f;
            
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyFlame") && animator.GetBool("Block") == true)
        {
            Debug.Log("Bloqueo LLamas " + other.gameObject.layer);


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

    void DeActivateBlockSFX()
    {
        if (timeToDeActivateBlockSFX <= 0)
        {
            swordBlockSFX.SetActive(false);
            timeToDeActivateBlockSFX = setTimerToDeActivateBlockSFX;
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
