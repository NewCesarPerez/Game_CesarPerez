using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject CreditCanvas;
   


    public void GoToLevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void QuitGame()
    {

        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #endif
       
    }

    public void GoToCredits()
    {
        CreditCanvas.SetActive(true);
        MainMenuCanvas.SetActive(false);

    }
    public void GoToMainMenu()
    {
        CreditCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }
}
