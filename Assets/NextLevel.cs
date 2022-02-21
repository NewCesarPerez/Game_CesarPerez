using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    [SerializeField] private GameObject VictoryCanvas;
    [SerializeField] private GameObject PlayerCanvas;
    [SerializeField] private GameObject EnemyCanvas;
    // Start is called before the first frame update

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "LevelThree")
        {
            PlayerCanvas.SetActive(true);
            EnemyCanvas.SetActive(true);
            VictoryCanvas.SetActive(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ToLevelThree();
        ToVictoryCanvas();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<EnemyController>() == null)
        {
            if (SceneManager.GetActiveScene().name=="LevelOne"&& other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SceneManager.LoadScene("LevelTwo");
            }

            
        }
    }

    private void ToLevelThree()
    {
       if (SceneManager.GetActiveScene().name == "LevelTwo" && FindObjectOfType<EnemyController>() == null)
        {
            SceneManager.LoadScene("LevelThree");
        }
    }

    private void ToVictoryCanvas()
    {
        if(SceneManager.GetActiveScene().name == "LevelThree" && FindObjectOfType<EnemyController>() == null)
        {
            PlayerCanvas.SetActive(false);
            EnemyCanvas.SetActive(false);
            VictoryCanvas.SetActive(true);
        }
    }


    public void ToMainMenu()
    {
        
            SceneManager.LoadScene("MainMenu");
        
    }
}
