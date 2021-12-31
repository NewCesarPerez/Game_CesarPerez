using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _playerLife;
    private int _killingCount;
    // Start is called before the first frame update

    private void Awake()
    {
        _playerLife = 100;
        _killingCount = 0;
        GameManager.instance.UpdatePlayerLife(_playerLife);
        GameManager.instance.AddKillingCount(_killingCount);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _playerLife -= 10;
            GameManager.instance.UpdatePlayerLife(_playerLife);
        }
    }
}
