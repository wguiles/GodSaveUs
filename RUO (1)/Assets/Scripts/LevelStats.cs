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
    public const float TIME_TO_ADD = 5;

    public GameObject ResultsPanel;

    private GameObject player;
    private int playerCheese;
    private int currentLevel;
    public bool LevelStarted;
    private MenuManager menuManager;
    bool levelComplete;

    public TextMeshProUGUI textCountDown;
    public TextMeshProUGUI LocationText;

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

       // player.transform.position = new Vector3(SpawnManager.instance.RandomSpawnerPosition().x, SpawnManager.instance.RandomSpawnerPosition().y, transform.position.z);
            

    }

    private IEnumerator CountDown()
    {
        int secondsLeft = 3;

        while(secondsLeft > 0)
        {
            yield return new WaitForSeconds(0.5f);
            secondsLeft--;
            SoundManager.instance.PlaySound("Countdown");

            if (secondsLeft < 10)
            {
                textCountDown.text = "0" + secondsLeft.ToString();
            }
            else
            {
                textCountDown.text = secondsLeft.ToString();
            }

        }

        FindObjectOfType<PlayerController>().gameStarted = true;
        SpawnManager.instance.StartSpawning();
       
        textCountDown.text = "GO!";
        LevelStarted = true;
        textCountDown.transform.parent.gameObject.SetActive(false);

        yield return new WaitForSeconds(3.0f);

        LocationText.gameObject.SetActive(false);

    }

    public void AddTime()
    {
        timeLeft += TIME_TO_ADD;
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

    public void EnableRats()
    {
        enemies = LevelType.Rats;
    }

    public void EnableMice()
    {
        enemies = LevelType.Mice;
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
            FindObjectOfType<ResultsScreenScript>().KillMe = enemies;
            SoundManager.instance.PlaySound("ResultsMusic");

            ResultsPanel.GetComponent<ResultsScreenScript>().FillBar(cheeseLevelQuota, cheeseUpgradeQuota);

           

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
                if (enemies == LevelType.Mice)
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
                else if(enemies == LevelType.Rats)
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
