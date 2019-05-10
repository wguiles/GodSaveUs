using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultsScreenScript : MonoBehaviour
{

    public Image LevelresultsCircle;
    public Image UpgradeResultsCircle;

    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI achievementText;

    public int LevelQuotaAquired;
    public int UpgradeQuotaAquired;
    public int CheeseAmountAquired;
    public float fillSpeed;

    private float cheeseAmountCount = 0f;
    private bool ResultsShowing;

    public GameObject MenuButton;

    private bool firstSoundPlayed;
    private bool secondSoundPlayed;

    public LevelStats.LevelType KillMe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("Results Screen Sound Called");
        SoundManager.instance.PlaySound("BarFillUp");
    }

    // Update is called once per frame
    void Update()
    {

        if (ResultsShowing)
        {
            LevelresultsCircle.fillAmount = Mathf.Lerp(0f, 1f, (float)cheeseAmountCount / LevelQuotaAquired);

            if (cheeseAmountCount < CheeseAmountAquired)
            {
                cheeseAmountCount += Time.deltaTime * fillSpeed;
                resultsText.text = ((int)cheeseAmountCount).ToString() + " / " + LevelQuotaAquired.ToString();
            }
            
            if (cheeseAmountCount >= LevelQuotaAquired)
            {
                MenuButton.GetComponentInChildren<TextMeshProUGUI>().text = "Press <A> To Continue";
                if (!firstSoundPlayed)
                {
                    SoundManager.instance.PlaySound("KillConfirmed");
                    firstSoundPlayed = true;
                   
                }


                achievementText.text = "Level Quota Aquired!";

                resultsText.text = ((int)cheeseAmountCount).ToString() + " / " + UpgradeQuotaAquired.ToString();

                UpgradeResultsCircle.fillAmount = Mathf.Lerp(0f, 1f, ((float)cheeseAmountCount - LevelQuotaAquired) / (UpgradeQuotaAquired - LevelQuotaAquired));
            }

            else
            {
                MenuButton.GetComponentInChildren<TextMeshProUGUI>().text = "Press <A> To Retry";
            }

            if (cheeseAmountCount >= UpgradeQuotaAquired)
            {

                if (!secondSoundPlayed)
                {
                    SoundManager.instance.PlaySound("KillConfirmed");
                    secondSoundPlayed = true;
                }
                achievementText.text = "Upgrade Quota Aquired!";

                GivePlayerUpgrade();
            }

            if (cheeseAmountCount >= CheeseAmountAquired)
            {
                SoundManager.instance.StopSound("BarFillUp");
                StartCoroutine(ActivateMenuButton());
             
            }
        }
    }

    public void FillBar()
    {
        CheeseAmountAquired = FindObjectOfType<PlayerStats>().GetCheeseCount();
        FindObjectOfType<PlayerStats>().gameObject.SetActive(false);
        LevelQuotaAquired = 200;
        UpgradeQuotaAquired = 300;
        ResultsShowing = true;
    }

    IEnumerator ActivateMenuButton()
    {
        yield return new WaitForSeconds(2.0f);
        MenuButton.SetActive(true);
    }

    public void loadNextLevel()
    {
        SoundManager.instance.StopAllSounds();

        if (cheeseAmountCount > LevelQuotaAquired)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GivePlayerUpgrade()
    {
        //check rats or mice
        //check level 

        if (KillMe == LevelStats.LevelType.Rats)
        {
            if (SceneManager.GetActiveScene().name == "TownsSpaceScene")
            {
                FindObjectOfType<PlayerUpgrades>().MiceUpgrade1();
            }
            else if (SceneManager.GetActiveScene().name == "ApartmentScene")
            {
                FindObjectOfType<PlayerUpgrades>().MiceUpgrade2();
            }
            else if (SceneManager.GetActiveScene().name == "PowerPlantScene2")
            {
                FindObjectOfType<PlayerUpgrades>().MiceUpgrade3();
            }
        }
        else if (KillMe == LevelStats.LevelType.Mice)
        {
            if (SceneManager.GetActiveScene().name == "TownsSpaceScene")
            {
                FindObjectOfType<PlayerUpgrades>().RatUpgrade1();
            }
            else if (SceneManager.GetActiveScene().name == "ApartmentScene")
            {
                FindObjectOfType<PlayerUpgrades>().RatUpgrade2();
            }
            else if (SceneManager.GetActiveScene().name == "PowerPlantScene2")
            {
                FindObjectOfType<PlayerUpgrades>().RatUpgrade3();
            }
        }
    }

}
