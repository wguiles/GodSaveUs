using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    //point arrow towards objective
    //display objective text
    //display time remaining
    //display cheese player has
    //public Vector3  targetVector;
    //public Image    uiArrow;
    public Image playerHealthSlider;

    public Image timeLeftSlider;
    public TextMeshProUGUI timeLeftText;

    //public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI playerCheeseAmount;
    public TextMeshProUGUI quotaText;

    public GameObject inGameArrow;

    public static GameUIManager ui_Manager;

    private void Awake()
    {
        if (ui_Manager == null)
            ui_Manager = this;
        else
            Destroy(this);
    }


    //The time remaining at the beginning of the level
    public float currentMaxTime;

    private void Start()
    {
        currentMaxTime = FindObjectOfType<LevelStats>().timeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        setQuotaText();

        UpdateArrowPosition();

        UpdateCheeseAmount(1);
    }

    void UpdateArrowPosition()
    {
        //uiArrow.rectTransform.up = (SpawnTargetScript.instance.GetCurrentTarget().transform.position - GameObject.Find("Player").transform.position);
        inGameArrow.transform.up = (SpawnTargetScript.instance.GetCurrentTarget().transform.position - GameObject.Find("Player").transform.position);
    }

    private void setQuotaText()
    {
        LevelStats level = FindObjectOfType<LevelStats>();

        if (level)
        {
            quotaText.text = level.cheeseLevelQuota.ToString();
        }
    }


    public void SetTimeRemaining(float TimeRemaining)
    {
        if (TimeRemaining < 10)
        {
            timeLeftText.text =  "0" + ((int)TimeRemaining).ToString();
        }
        else 
        {
            timeLeftText.text = ((int)TimeRemaining).ToString();
        }
        timeLeftSlider.fillAmount = TimeRemaining / currentMaxTime;

    }

    public void UpdateCheeseAmount(int amount)
    {
        amount = FindObjectOfType<PlayerStats>().GetCheeseCount();
        playerCheeseAmount.text = amount.ToString();
    }

}
