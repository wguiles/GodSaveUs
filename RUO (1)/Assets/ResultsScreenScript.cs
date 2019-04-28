using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        
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
                achievementText.text = "Level Quota Aquired!";

                resultsText.text = ((int)cheeseAmountCount).ToString() + " / " + UpgradeQuotaAquired.ToString();

                UpgradeResultsCircle.fillAmount = Mathf.Lerp(0f, 1f, ((float)cheeseAmountCount - LevelQuotaAquired) / (UpgradeQuotaAquired - LevelQuotaAquired));
            }

            if (cheeseAmountCount >= UpgradeQuotaAquired)
            {
                achievementText.text = "Upgrade Quota Aquired!";
            }

            if (cheeseAmountCount >= CheeseAmountAquired)
            {
                StartCoroutine(ActivateMenuButton());
            }
          
        }
    }

    public void FillBar()
    {
        CheeseAmountAquired = FindObjectOfType<PlayerStats>().GetCheeseCount();
        FindObjectOfType<PlayerStats>().gameObject.SetActive(false);
        LevelQuotaAquired = 100;
        UpgradeQuotaAquired = 250;
        ResultsShowing = true;
    }

    IEnumerator ActivateMenuButton()
    {
        yield return new WaitForSeconds(2.0f);
        MenuButton.SetActive(true);
    }
}
