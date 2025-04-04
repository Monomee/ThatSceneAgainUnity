using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    //---const---
    public const string LEVEL_SAVE_KEY = @"Level";
    public const int FIRST_LEVEL = 0;
    const int NUMBERS_LEVEL = 7;

    //---singleton---
    public static GameManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    //---Object and player---
    [SerializeField] GameObject Barrier;
    [SerializeField] GameObject Player;
    public bool reverse;
    public Transform startPosition;

    //---level---
    [SerializeField] GameObject lastScene;
    GameObject currentLevelObj;
    public int currentLevel;
    GameObject levelPrefab;

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
    
    public void LoadCurrentLevel()
    {
        if (currentLevelObj != null) 
            Destroy(currentLevelObj); // Delete current level      
        
        Debug.Log("Loading level: " + currentLevel);

        //Destroy(Player.GetComponent<Player>().deadBody);
        Player.transform.position = startPosition.position;

        levelPrefab = LoadLevelPrefab(currentLevel+1);
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
        UIManager.Instance.content.text = UIManager.Instance.contentContainer[currentLevel].ToString();
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
        AudioManager.Instance.backgroundMusic.Pause();
        AudioManager.Instance.endMusic.Play();
        lastScene.SetActive(true);
        UIManager.Instance.PauseGame();
    }
}

