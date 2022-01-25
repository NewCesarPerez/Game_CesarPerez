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
    private float _CrouchTime;
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
        _CrouchTime = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeToGetHit -= Time.deltaTime;
        MovePlayer();
        MoveWithoutSword();
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

    }

    private void MoveWithoutSword()
    {
        _runningTime -= Time.deltaTime;
        _CrouchTime -= Time.deltaTime;
        //Metodo horrible para que el player solo se agache cada 4 segundos y por 4 segundos. INICIO
        if (Input.GetKey(KeyCode.Keypad0) && _CrouchTime > 0)
        {

        }
        if (Input.GetKey(KeyCode.Keypad0)&&_CrouchTime <= 0)
        {
            
            animator.SetBool("Crouch", true);
            if (_CrouchTime <= -_maxTime)
            {
                _CrouchTime = 0;
                animator.SetBool("Crouch", false);
                _CrouchTime = _maxTime;
            }
        }

        else if (Input.GetKeyUp(KeyCode.Keypad0)){
            animator.SetBool("Crouch", false);
            if (_CrouchTime <= 0)
            {
                _CrouchTime = _maxTime;
            }
        }
        else if (_CrouchTime <= 0)
        {
            _CrouchTime = 0;
        }
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Run", true);
            animator.SetBool("RwS", true);
            PlayerSpeed = 6f;
            PlayerRotateSpeed = 100f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Run", false);
            animator.SetBool("RwS", false);
            PlayerSpeed = 1f;
            PlayerRotateSpeed = 65f;
            if (_runningTime <= 0)
            {
                _runningTime = _maxTime;
            }
        }
        //Metodo horrible para que el player solo se agache cada 4 segundos y por 4 segundos. FIN

        //Metodo horrible para que el player solo corra cada 4 segundos y por 4 segundos. INICIO

        //if (Input.GetKey(KeyCode.LeftShift) && _runningTime > 0)
        //{

        //}
        //if (Input.GetKey(KeyCode.LeftShift) && _runningTime<=0)
        //{

        //    //_runningTime =_maxTime;

        //    animator.SetBool("Run", true);
        //    animator.SetBool("RwS", true);
        //    PlayerSpeed = 6f;
        //    PlayerRotateSpeed = 100f;

        //    if (_runningTime <= -_maxTime)
        //    {
        //        _runningTime = 0;
        //        animator.SetBool("Run", false);
        //        animator.SetBool("RwS", false);
        //        PlayerSpeed = 1f;
        //        PlayerRotateSpeed = 65f;
        //        _runningTime = _maxTime;
        //    }

        //}
        //else if (_runningTime <= 0)
        //{
        //    _runningTime = 0;
        //}
        //Metodo horrible para que el player solo corra cada 4 segundos y por 4 segundos. FIN
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
        if (collision.gameObject.CompareTag("EnemySword")&&timeToGetHit<=0 &&animator.GetBool("Blocking")==false)
        {
            
            
            float enemyBaseDamage = collision.gameObject.GetComponent<EnemySwordDamage>()._EnemyBaseDamage; 
            
           
            _currentLife -= enemyBaseDamage;
            var healthImagePorcentage = _currentLife / _maxLife;
            healthImage.fillAmount = healthImagePorcentage;

            Debug.Log("Restando hp: " + _currentLife);
            animator.SetTrigger("Impacted");
            timeToGetHit = 1f;
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
