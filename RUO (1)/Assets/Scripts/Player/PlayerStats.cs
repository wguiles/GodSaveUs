using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float recoverTime;
    //ui output
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text cheeseAmount;
     private MenuManager menuManager;
    public GameObject DeathScreen;

    static int MAX_HEATLH = 10;

    //general stats
    private int health = 10;
    private int cheeseCount;
    private bool recovering;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();

        if (PlayerUpgrades.Rat2)
        {
            MAX_HEATLH = 20;
            health = MAX_HEATLH;
        }
    }

    public int GetCheeseCount()
    {
        return cheeseCount;
    }

    public int GetMaxHealth()
    {
        return MAX_HEATLH;
    }

    public void SetMaxHealth(int amount)
    {
        MAX_HEATLH += amount;
        health = MAX_HEATLH;
    }



    //Once the player gets the melee upgrade it shows the icon
    //public void ShowMeleeIcon()
    //{
    //    if (upgrade.sprite != meleeIcon)
    //    {
    //        upgradeText.enabled = true;
    //        upgradeText.text = "Melee Upgrade Unlocked";
    //        StartCoroutine(disableText());
    //    }

    //    upgrade.enabled = true;
    //    upgrade.sprite = meleeIcon;
    //}

    ////Once the player gets the freeze time upgrade it shows the icon
    //public void ShowIceShoeIcon()
    //{
    //    if (upgrade.sprite != iceShoeIcon)
    //    {
    //        upgradeText.enabled = true;
    //        upgradeText.text = "Teleport Upgrade Unlocked";
    //        StartCoroutine(disableText());
    //    }

    //    upgrade.enabled = true;
    //    upgrade.sprite = iceShoeIcon;
    //}

    //Removes the text from the screen after a few seconds
    private IEnumerator disableText()
    {
        yield return new WaitForSeconds(2.0f);
        //upgradeText.enabled = false;
    }

    //Disables the upgrade icon if the player no longer has it
    public void HideIcon()
    {
        //upgrade.enabled = false;
    }

    //Removes health from the player
    public void TakeDamage(int damageTaken)
    {
        if (!recovering && !GetComponent<PlayerController>().GetIsDashing())
        {
            recovering = true;
            StartCoroutine(Blink());
            SoundManager.instance.PlaySound("PlayerHurtS");
            if ((health -= damageTaken) <= 0)
            {

                UpdateHealth();
                Death();
            }

            UpdateHealth();
        }
    }

    //If the player takes damage they flash/blink while they are recovering
    private IEnumerator Blink()
    {
        float secondsPassed = 0.0f;
        Debug.Log("Blink Called");



        while (secondsPassed < recoverTime)
        {
            yield return new WaitForSeconds(0.1f);

            transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;

            secondsPassed += 0.1f;

            if (secondsPassed >= recoverTime)
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                recovering = false;
                break;
            }
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("Blink Left");

        recovering = false;

    }

    //Adds cheese to the current count
    public void CollectCheese(int cheese)
    {
        SoundManager.instance.PlaySound("GetCheeseS");
        cheeseCount += cheese;
        UpdateCheese();
    }

    //Subtracts cheese from the current amount
    public void LoseCheese(int cheese)
    {
        if (cheeseCount - cheese > 0)
        {
            SoundManager.instance.PlaySound("DroppedCheeseS");
            cheeseCount -= cheese;
        }
        else
        {
            cheese = 0;
        }
      
        Debug.Log("(Losing Cheese) Cheese Amount: " + cheeseCount);
        UpdateCheese();
    }

    //Upon death it loads the death scene
    private void Death()
    {
        //menuManager.ActivateLoseScreen("You Died :( ");
        SoundManager.instance.StopAllSounds();
        FindObjectOfType<LevelStats>().gameObject.SetActive(false);
        DeathScreen.SetActive(true);
       // Destroy(gameObject);
    }

    //Updates the UI health bar
    private void UpdateHealth()
    {
        healthImage.fillAmount = (float)health / MAX_HEATLH;
    }

    //Updates the UI for the current cheese amount
    private void UpdateCheese()
    {
        //cheeseAmount.text = "Cheese " + cheeseCount.ToString();
    }
    
    public void ResetPlayerStats()
    {
        health = MAX_HEATLH;
        cheeseCount = 0;
        UpdateHealth();
        UpdateCheese();
    }
}