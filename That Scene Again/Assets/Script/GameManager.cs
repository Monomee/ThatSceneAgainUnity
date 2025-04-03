using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public const string LEVEL_SAVE_KEY = @"Level";
    public const int FIRST_LEVEL = 0;
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
    public List<GameObject> levelConfig = new List<GameObject>();
    public Dictionary<int, GameObject> levelContainer = new Dictionary<int, GameObject>();
    GameObject currentLevelObj;

    int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        int idLevel = FIRST_LEVEL;

        foreach (GameObject level in levelConfig)
        {
            levelContainer.Add(idLevel, level);
            idLevel++;
        }
        currentLevel = GetCurrentSavedLevel();

        LoadCurrentLevel();
    }
    public void LoadCurrentLevel()
    {
        if (currentLevelObj != null) 
            Destroy(currentLevelObj); // Delete current level      
        
        Debug.Log("Loading level: " + currentLevel);
      
        Player.transform.position = startPosition.position;
        UpdateObjectsBaseOnLevel(currentLevel);
        UpdateLogicBaseOnLevel(currentLevel);
        currentLevelObj = Instantiate(levelContainer[currentLevel]);
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
        return PlayerPrefs.GetInt("Level", GameManager.FIRST_LEVEL);
    }

    public void GoToNextLevel()
    {       
        SaveGame();
        LoadCurrentLevel();
    }
    public void SetNextLevel()
    {
        currentLevel++;
        if (currentLevel >= levelConfig.Count)
        {
            currentLevel = FIRST_LEVEL;
        }
        GoToNextLevel();
    }
}

