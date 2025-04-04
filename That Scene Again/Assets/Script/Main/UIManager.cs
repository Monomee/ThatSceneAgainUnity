using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    //---instruction----
    public TextMeshProUGUI content;
    public string[] contentContainer = {"1. Just take the key", "2. Don't trust anything", "3. Don't move without permission",
        "4. Reverse movement", "5. Moon's gravity", "6. New way to move", "7. Think outside of the box"};
    public string[] instructionContainer =
    {
        "1. Take the key and complete the game, ez right?",
        "2. Look at the signs, something will be appeared, move carefully.",
        "3. Eyes open, don't move. Eyes close, move.",
        "4. Reverve, left is right, right is left.",
        "5. Jump higher, fall slower, take advantage of that",
        "6. <Update later>",
        "7. Top right first, take the key by enter password (using Morse code), then return to start postion and go to the left, drag object to take the key"
    };
    [SerializeField] GameObject textInstruction;
    [SerializeField] Transform canvas;
    GameObject newText;
    bool isClicked = false;
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        isClicked = false;
        Destroy(newText);
    }
    public void ContinueAfterLoop()
    {
        Time.timeScale = 1;
        AudioManager.Instance.backgroundMusic.Play();
        AudioManager.Instance.endMusic.Stop();
    }
    public void LoadMenu()
    {
        AudioManager.Instance.backgroundMusic.Play();
        AudioManager.Instance.endMusic.Stop();
        Destroy(newText);
        SceneManager.LoadScene("Menu");
    }
    public void LoadInstruction()
    {
        if (!isClicked)
        {
            textInstruction.GetComponentInChildren<TextMeshProUGUI>().text = instructionContainer[GameManager.Instance.currentLevel];
            newText = Instantiate(textInstruction, canvas);
            isClicked = true;
        }
    }
}
