using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelStats : MonoBehaviour
{
    public int cheeseLevelQuota;
    public int cheeseUpgradeQuota;
    public float timeLeft;

    public GameObject ResultsPanel;

    private GameObject player;
    private int playerCheese;
    private int currentLevel;
    public bool LevelStarted;
    private MenuManager menuManager;
    bool levelComplete;

    public TextMeshProUGUI textCountDown;

    private void Start()
    {
        //temporary start method for shizz
        menuManager = FindObjectOfType<MenuManager>();
        currentLevel = menuManager.GetCurrentLevel();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    private void OnEnable()
    {
        SoundManager.instance.StopAllSounds();
        SoundManager.instance.PlaySound("GameplayMusic");
    }

    public void StartCountdown()
    {
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int secondsLeft = 3;

        while(secondsLeft > 0)
        {
            yield return new WaitForSeconds(0.5f);
            secondsLeft--;
            SoundManager.instance.PlaySound("Countdown");
            textCountDown.text = secondsLeft.ToString();
        }

        FindObjectOfType<PlayerController>().gameStarted = true;
        SpawnManager.instance.StartSpawning();
       
        textCountDown.text = "GO!";
        LevelStarted = true;
        textCountDown.transform.parent.gameObject.SetActive(false);

    }

    public enum LevelType
    {
        Rats,
        Mice
    }

    public LevelType enemies;

    public void SetLevelType(LevelType type)
    {
        enemies = type;
    }

    public void GenerateEnemies()
    {
        if (enemies == LevelType.Rats)
        {
            EnableRats();
        }
        else
        {
            EnableMice();
        }

    }

    private void EnableRats()
    {
        foreach(Transform t in transform)
        {
            //EnemyStats temp = t.GetComponent<EnemyStats>();

            //if (temp != null)
            //{
            //    if (temp.faction == EnemyStats.FactionType.rat)
            //        temp.gameObject.SetActive(true);
            //    else
            //        temp.gameObject.SetActive(false);
            //}

            if (t.gameObject.name == "Rats")
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    private void EnableMice()
    {
        foreach (Transform t in transform)
        {
            //EnemyStats temp = t.GetComponent<EnemyStats>();

            //if (temp != null)
            //{
            //    if (temp.faction == EnemyStats.FactionType.mouse)
            //        temp.gameObject.SetActive(true);
            //    else
            //        temp.gameObject.SetActive(false);
            //}

            if (t.gameObject.name == "Mice")
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (LevelStarted)
        {
            timeLeft -= Time.deltaTime;
        }

        GameUIManager.ui_Manager.SetTimeRemaining(timeLeft);

        

        //If the time limit for the level is up
        if(timeLeft <= 0 && !levelComplete)
        {
            gameObject.SetActive(false);



            SoundManager.instance.StopAllSounds();
            ResultsPanel.SetActive(true);
            SoundManager.instance.PlaySound("ResultsMusic");

            ResultsPanel.GetComponent<ResultsScreenScript>().FillBar();

            levelComplete = true;

            //Time.timeScale = 0;
            playerCheese = player.GetComponent<PlayerStats>().GetCheeseCount();
            //Debug.Log("End of level cheese: " + playerCheese);

            //Determine if quotas were met for level and upgrade
            if (playerCheese < cheeseLevelQuota)
            {
                //You lose screen of not meeting quota
                menuManager.ActivateLoseScreen("Level Quota Not Met");
            }
            else if (playerCheese >= cheeseLevelQuota && playerCheese < cheeseUpgradeQuota)
            {
                menuManager.ActivateLevelCompleteScreen("Upgrade Quota Not Met");
            }
            else if (playerCheese >= cheeseLevelQuota && playerCheese >= cheeseUpgradeQuota)
            {
                menuManager.ActivateLevelCompleteScreen("Upgrade Quota Met");

                //Determine which faction
                if (enemies == LevelType.Rats)
                {
                    if (currentLevel == 1)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().RatUpgrade1();
                    }
                    if(currentLevel == 2)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().RatUpgrade2();
                    }
                    if(currentLevel == 3)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().RatUpgrade3();
                    }
                    if (currentLevel == 4)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().RatUpgrade4();
                    }
                }
                else if(enemies == LevelType.Mice)
                {
                    if (currentLevel == 1)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().MiceUpgrade1();
                    }
                    if (currentLevel == 2)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().MiceUpgrade2();
                    }
                    if (currentLevel == 3)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().MiceUpgrade3();
                    }
                    if (currentLevel == 4)
                    {
                        //Activate the upgrade
                        player.GetComponent<PlayerUpgrades>().MiceUpgrade4();
                    }
                }
                ////Display Level Quota met but not upgrade screen
                //else
                //{
                //    
                //}

                gameObject.SetActive(false);
            }
        }
    }

    //new button states
    //rats and mice spawning in level
    //what the hell is happening
    //too many things
    //melee attack feels wierd
    //player feedback nonexistent
    // -- how to tell enemies are being hurt
    //slow everything down
    //mice too fast
    //rats in walls
    //move camera around
    //reason for slash

    //should not 
}
