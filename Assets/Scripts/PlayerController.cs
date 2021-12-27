using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float PlayerSpeed;
    [SerializeField] private float PlayerRotateSpeed;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        MoveWithoutSword();
    }

    void move(Vector3 dir)
    {
        
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
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
}
