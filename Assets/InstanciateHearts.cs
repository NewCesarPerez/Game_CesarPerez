using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciateHearts : MonoBehaviour
{
    [SerializeField] private GameObject emergencyHeart;
    [SerializeField] private Transform heartPosition;

    private bool allowInst = true;


    public void InstHeart()
    {
        if (allowInst == true) {
            allowInst = false;
            Instantiate(emergencyHeart, heartPosition.position, Quaternion.identity, transform);
        }
    }
}
