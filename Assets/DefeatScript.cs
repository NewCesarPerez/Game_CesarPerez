using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScript : MonoBehaviour
{
    [SerializeField] private GameObject PlayerCanvas;
    //[SerializeField] private GameObject PlayerLife;
    [SerializeField] private GameObject DefeatCanvas;
    [SerializeField] private GameObject BGM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TurnOffFightMusic();
    }

    void ToDefeatCanvas()
    {
        DefeatCanvas.SetActive(true);
        
        PlayerCanvas.SetActive(false);
        //if (PlayerLife.GetComponent<PlayerController>().GetCurrentLife() <= 0)
        //{

        //}
    }

    void TurnOffFightMusic() {
        if (FindObjectOfType<MeleeController>().GetComponent<PlayerController>().GetCurrentLife() <=0) 
            {
            BGM.SetActive(false);
        }
        
        
        }

   public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
