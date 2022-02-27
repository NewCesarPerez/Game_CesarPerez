 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool chasePlayer;
    private int _killingCount;
    
    private int _playerDeathCount;
    private int _playerCurrentLife;
    private int _hits;

    [System.NonSerialized] public int _playerBaseDamage=30;
    
    public bool dontDestroy;
    private void Awake()
    {
        chasePlayer = false;
        _playerCurrentLife = 100;
        _playerBaseDamage = 40;
        _killingCount = 0;
        _playerDeathCount = 0;
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
        
       
        _killingCount++;
        Debug.Log(GetKillingCount());
    }

    public void AddHits()
    {
        _hits++;
    }

    public void AddPlayerDeathCount()
    {
        _playerDeathCount++;
        Debug.Log("Player number of death: " + GetPlayerDeathCount());

    }

    public void UpdatePlayerLife(int currentLife)
    {
        _playerCurrentLife = currentLife;
    }

    public int GetKillingCount()
    {
      return  _killingCount;
    }

    public int GetPlayerDeathCount()
    {
        return _playerDeathCount;
    }
    public int GetHits()
    {
        return _hits;
    }
}
