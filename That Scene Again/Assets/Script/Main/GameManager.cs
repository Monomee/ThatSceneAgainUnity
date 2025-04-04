using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public const string LEVEL_SAVE_KEY = @"Level";
    public const int FIRST_LEVEL = 0;
    const int NUMBERS_LEVEL = 7;

    public static GameManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    //Object and player
    [SerializeField] GameObject Barrier;
    [SerializeField] GameObject Player;
    public bool reverse;
    public Transform startPosition;

    //level
    [SerializeField] TextMeshProUGUI content;
    string[] contentContainer = {"1. Just take the key", "2. Don't trust anything", "3. Don't move without permission", 
        "4. Reverse movement", "5. Moon's gravity", "6. New way to move", "7. Think outside of the box"};
    string[] instructionContainer =
    {
        "1. Take the key and complete the game, ez right?",
        "2. Look at the signs, something will be appeared, move carefully.",
        "3. Eyes open, don't move. Eyes close, move.",
        "4. Reverve, left is right, right is left.",
        "5. Jump higher, fall slower, take advantage of that",
        "6. <Update later>",
        "7. Top right first, take the key by enter password (using Morse code), then return to start postion and go to the left, drag object to take the key"
    };
    [SerializeField] GameObject lastScene;
    GameObject currentLevelObj;
    int currentLevel;

    //music
    AudioSource[] music;
    AudioSource backgroundMusic;
    AudioSource endMusic;

    private void Awake()
    {
        music = AudioManager.Instance.GetComponents<AudioSource>();
        backgroundMusic = music[0];
        endMusic = music[1];
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = GetCurrentSavedLevel();

        LoadCurrentLevel();
    }
    public GameObject LoadLevelPrefab(int levelIndex)
    {
        string path = $"Levels/{levelIndex}"; // Đường dẫn tới prefab trong Resources
        GameObject levelPrefab = Resources.Load<GameObject>(path);

        if (levelPrefab == null)
        {
            Debug.LogError($"Không tìm thấy level {levelIndex} trong Resources!");
        }

        return levelPrefab;
    }
    GameObject levelPrefab;
    public void LoadCurrentLevel()
    {
        if (currentLevelObj != null) 
            Destroy(currentLevelObj); // Delete current level      
        
        Debug.Log("Loading level: " + currentLevel);

        Destroy(Player.GetComponent<Player>().deadBody);
        Player.transform.position = startPosition.position;

        GameObject levelPrefab = LoadLevelPrefab(currentLevel+1);
        if (levelPrefab != null)
        {
            currentLevelObj = Instantiate(levelPrefab);
        }

        UpdateObjectsBaseOnLevel(currentLevel);
        UpdateLogicBaseOnLevel(currentLevel);

        Debug.Log(currentLevel);
    }
    
    void UpdateLogicBaseOnLevel(int currentLevel)
    {
        switch (currentLevel)
        {
            case 3: //reverse movement
                DefaultSetting();
                reverse = true;
                break;
            case 4: //gravity like on the Moon
                DefaultSetting();
                Player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                Player.GetComponent<MovementController>().jumpForce = 15;
                break;
            case 5: //new way to move (idea base on mobile)
                DefaultSetting();
                break;
            default:
                DefaultSetting();
                break;
        }
    }
    void DefaultSetting()
    {
        Player.GetComponent<Rigidbody2D>().gravityScale = 1.05f;
        Player.GetComponent<MovementController>().jumpForce = 10;
        reverse = false;
    }
    void UpdateObjectsBaseOnLevel(int currentLevel)
    {
        content.text = contentContainer[currentLevel].ToString();
        switch (currentLevel) 
        {
            case 6: Barrier.SetActive(false); break;
            default: Barrier.SetActive(true); break;
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.Save();
    }

    public int GetCurrentSavedLevel()
    {
        return PlayerPrefs.GetInt("Level", FIRST_LEVEL);
    }

    public void GoToNextLevel()
    {       
        SaveGame();
        LoadCurrentLevel();
    }
    public void SetNextLevel()
    {
        currentLevel++;
        if (currentLevel >= NUMBERS_LEVEL)
        {
            GoToLastScene();
        }
        GoToNextLevel();    
    }
    public void GoToLastScene()
    {
        currentLevel = FIRST_LEVEL;
        music[0].Pause();
        music[1].Play();
        lastScene.SetActive(true);
        PauseGame();
    }
    
    //---------------
    [SerializeField] GameObject textInstruction;
    [SerializeField] Transform canvas;
    GameObject newText;
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        Destroy(newText);
    }
    public void ContinueAfterLoop()
    {
        Time.timeScale = 1;
        music[0].Play();
        music[1].Stop();
    }
    public void LoadMenu()
    {
        music[0].Play();
        music[1].Stop();
        Destroy(newText);
        SceneManager.LoadScene("Menu");
    }
    public void LoadInstruction()
    {
        textInstruction.GetComponentInChildren<TextMeshProUGUI>().text = instructionContainer[currentLevel];
        newText = Instantiate(textInstruction, canvas);
    }
}

