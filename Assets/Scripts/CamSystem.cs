using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSystem : MonoBehaviour
{
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;
    // Start is called before the first frame update
    void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        AlternateCamera();
    }

    void AlternateCamera()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)){
            cam1.SetActive(!cam1.activeSelf);
            cam2.SetActive(!cam2.activeSelf);
        }
            
    }
}
