 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool chasePlayer;
    private int _killingCount;
    private int _playerDeathCount;
    private int _playerCurrentLife;
    private int killingToCount;
    [System.NonSerialized] public int _playerBaseDamage=30;
    
    public bool dontDestroy;
    private void Awake()
    {
        chasePlayer = false;
        _playerCurrentLife = 100;
        _playerBaseDamage = 40;
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

    public void AddKillingCount()
    {
        killingToCount++;
        _killingCount += killingToCount;
    }

    public void UpdatePlayerLife(int currentLife)
    {
        _playerCurrentLife = currentLife;
    }

    public int GetKillingCount()
    {
      return  _killingCount;
    }
}
