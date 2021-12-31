 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int _killingCount;
    private int _playerLife;
    public bool dontDestroy;
    private void Awake()
    {
        _playerLife = 100;
        _killingCount = 0;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            if(dontDestroy)DontDestroyOnLoad(this);
        }
    }

    public void AddKillingCount(int killingToCount)
    {
        _killingCount += killingToCount;
    }

    public void UpdatePlayerLife(int currentLife)
    {
        _playerLife = currentLife;
    }

    public int KillingCount => _killingCount;
    public int PlayerLife => _playerLife;
}
