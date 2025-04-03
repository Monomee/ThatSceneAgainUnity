using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMananger : MonoBehaviour
{
    public void LoadNewGame()
    {
        //Save Game
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.Save();

        //Load Game
        LoadContinueGame();
    }
    public void LoadContinueGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
