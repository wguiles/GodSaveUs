using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [Header("Level Stuff")]
    [SerializeField] private GameObject[] levels;

    public GameObject pauseScreen;
    public GameObject loseScreen;
    public GameObject levelCompleteScreen;
    public Text loseScreenText;
    public Text levelCompleteScreenText;
    public string gameScene;

    public static bool isPaused = false;
    private bool inLevel = false;

    int currentLevel = 0; //Index starts at Start Menu

    public void ActivateLoseScreen(string text)
    {
        if(!loseScreen.activeSelf)
        {
            loseScreen.SetActive(true);
            loseScreenText.text = text;
        }
        
    }

    public void DeactivateLoseScreen()
    {
        loseScreen.SetActive(false);
    }

    public void ActivateLevelCompleteScreen(string text)
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetCanMove(false);

        if (!levelCompleteScreen.activeSelf)
        {
            levelCompleteScreen.SetActive(true);
            levelCompleteScreenText.text = text;
        }
            
    }

    public void DeactivateLevelCompleteScreen()
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetCanMove(true);

        levelCompleteScreen.SetActive(false);   
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public GameObject GetCurrentLevelObject()
    {
        return levels[currentLevel];
    }

    public void DeactivateCurrentLevel()
    {
        levels[currentLevel].SetActive(false);
    }

    public void LoadLevel(int Faction)
    {
       

        //Deactivate targets from previous level
        DeactivateTargets();
        if (currentLevel == levels.Length)
        {
            //returns to main menu
            ReturnToStartMenu();
        }
        //deactivates current level
        levels[currentLevel].SetActive(false);

        //enable objective scripts
        GetComponent<GameUIManager>().enabled = true;
        levels[currentLevel].GetComponent<ObjectiveManager>().enabled = true;


        //generate enemies based on choice (0 rats
        LevelStats loadedLevel = levels[currentLevel].GetComponent<LevelStats>();

        if (Faction == 0)
        {
            loadedLevel.SetLevelType(LevelStats.LevelType.Rats);
        }
        else
        {
            loadedLevel.SetLevelType(LevelStats.LevelType.Mice);
        }

        //activates next level
        loadedLevel.GenerateEnemies();
        levels[currentLevel].SetActive(true);

       // levels[currentLevel].GetComponent<ObjectiveManager>().CreateStartingobjective();

        if (currentLevel == levels.Length)
        {
            //returns to main menu
            ReturnToStartMenu();
        }
        else
        {
            currentLevel++;
        }

        //Time.timeScale = 1;
    }

    //Deactivates the targets from the previous level
    public void DeactivateTargets()
    {
        GameObject[] targetList = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targetList)
        {
            Destroy(target);
        }
    }

    public void PauseGame()
    {
        if (FindObjectOfType<LevelStats>().LevelStarted)
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            Debug.Log("Timescale: " + Time.timeScale.ToString());
            pauseScreen.SetActive(true);
        }
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
    }

    public void ReturnToStartMenu()
    {
        Time.timeScale = 1.0f;
        //pauseScreen.SetActive(false);
        //levels[currentLevel].SetActive(false);
        //levels[0].SetActive(true);
        loadScene(gameScene);
    }
    

    public void loadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start Button"))
        {
            PauseGame();
        }
    }

    public void PlayHighlightedSound()
    {
        SoundManager.instance.PlaySound("PlayerFired");
    }

    public void PlaySelectedSound()
    {
        SoundManager.instance.PlaySound("DashAttackS");
    }

}
