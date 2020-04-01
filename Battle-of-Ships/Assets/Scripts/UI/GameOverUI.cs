using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Text playerWinText;
    [SerializeField] private ShipStats playerOneStats;
    private const float TIME_AFTER_END_SCREEN_SHOW = 3;
    private const string WIN_SOUND_NAME = "WinSFX";
    public static bool gameEnded;
    const string RESTART_SCENE_NAME = "GameScene";
    
    private void Update()
    {
       CheckCanShowEndScreen();
    }

    void CheckCanShowEndScreen()
    {
        if (gameEnded)
        {
            AudioManager.audioManagerInstance.PlaySound(WIN_SOUND_NAME);
            StartCoroutine(ShowEndScreen());
            gameEnded = false;
        } 
    }
    
    
    IEnumerator ShowEndScreen()
    {
        string winPlayerName = GetWhoWin();
        playerWinText.text = winPlayerName + "Win";
        yield return new WaitForSeconds(TIME_AFTER_END_SCREEN_SHOW);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    string GetWhoWin()
    {
        if (playerOneStats.currentHealth > 0)
        {
            return "Player 1 ";
        }
        else
        {
            return "Player 2 ";
        }
    }
    
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(RESTART_SCENE_NAME, LoadSceneMode.Single);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}