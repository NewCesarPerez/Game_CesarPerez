using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerRotateSpeed;
    [SerializeField] private Transform eyesTransform;
    [SerializeField] private LayerMask layerToCollide;
    [SerializeField] private float maxDistance;
    private float _maxTime;
    private float _runningTime;
    private float _CrouchTime;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {

        _maxTime = 4f;
        _runningTime = _maxTime;
        _CrouchTime = _maxTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        MoveWithoutSword();
        RayCastPlayerEnemy();
        Debug.Log("Tiempo corriendo " + _runningTime);
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

        if (Input.GetKeyDown(KeyCode.Keypad0)&&_CrouchTime<=0)
        {
            _CrouchTime = _maxTime;
            animator.SetBool("Crouch", true);
        }

        else if (Input.GetKeyUp(KeyCode.Keypad0)){
            animator.SetBool("Crouch", false);
        }

        Debug.Log("Tiempo corriendo " + _runningTime);
        if (Input.GetKey(KeyCode.LeftShift) && _runningTime<=0)
        {
            
            _runningTime =_maxTime;
            Debug.Log("Tiempo corriendo " + _runningTime);
            Debug.Log("ApretandoShift");
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

}
