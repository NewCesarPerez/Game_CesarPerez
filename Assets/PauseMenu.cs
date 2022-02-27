using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPause = false;
    private bool CallPauseMenu = false;
    [SerializeField] private GameObject PauseMenuUI;
    [SerializeField] private GameObject StatsPanelUI;
    [SerializeField] private TextMeshProUGUI EnemyDeaths;
    [SerializeField] private TextMeshProUGUI PlayersDeaths;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&StatsPanelUI.activeSelf==false)
        {
            if (gameIsPause)
            {
                Resume();

            }
            else
            {
                Pause();
            }

        }

        ShowEnemyDeaths();
        ShowPlayersDeaths();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPause = true;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToStats()
    {
        StatsPanelUI.SetActive(true);
        PauseMenuUI.SetActive(false);

    }
    public void GoToPauseMenu()
    {
        StatsPanelUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    void ShowEnemyDeaths()
    {
        EnemyDeaths.text= GameManager.instance.GetKillingCount().ToString();
    }

    void ShowPlayersDeaths()
    {
        PlayersDeaths.text = GameManager.instance.GetPlayerDeathCount().ToString();
    }
}
