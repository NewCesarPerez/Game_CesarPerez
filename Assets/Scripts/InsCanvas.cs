using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsCanvas : MonoBehaviour
{
    [SerializeField] private GameObject InstructPanel;
    private float timeToDeactiveInstPanel=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToDeactiveInstPanel -= Time.deltaTime;
        DeActivatePanel();
    }

    void DeActivatePanel()
    {
        if(timeToDeactiveInstPanel<=0)
        InstructPanel.SetActive(false);
    }
}
