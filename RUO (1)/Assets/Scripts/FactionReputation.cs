using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionReputation : MonoBehaviour
{
    ////reference for the bar at the top of the screen
    //[SerializeField] private Image ReputationSliderImage;
    //[SerializeField] private Image ratIcon;
    //[SerializeField] private Image mouseIcon;
    //[SerializeField] private Text ratsCheeseFeedBack;
    //[SerializeField] private Text miceCheeseFeedBack;

    //private const int REPUTATION_MIN = 0;
    ////This is where it starts but is also the midpoint
    //private const int REPUTATION_START = 50;    
    //private const int REPUTATION_MAX = 100;
    //private int reputationPoints;
    //private float aggroRadius;
    //private int MiceCheese;
    //private int RatCheese;

    //public GameObject bonusEnforcers;

    //// Use this for initialization
    //void Start()
    //{
    //    reputationPoints = REPUTATION_START;
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public int GetReputationPoints()
    //{
    //    return reputationPoints;
    //}

    //public int GetReputationPointMax()
    //{
    //    return REPUTATION_MAX;
    //}

    //public int GetReputationPointMin()
    //{
    //    return REPUTATION_MIN;
    //}

    //public int GetReputationPointMid()
    //{
    //    return REPUTATION_START;
    //}

    //public void AddToRats(int cheeseToAdd)
    //{
    //    RatCheese += cheeseToAdd;

    //    StartCoroutine(feedback(ratsCheeseFeedBack, "+ " + cheeseToAdd.ToString()));
    //}

    //public void AddToMice(int cheeseToAdd)
    //{
    //    MiceCheese += cheeseToAdd;

    //    StartCoroutine(feedback(miceCheeseFeedBack, "+ " + cheeseToAdd.ToString()));
    //}


    //public void ModifyReputationPoints(int points)
    //{
    //    if (points < 0)
    //    {
    //        StartCoroutine(redFlash(mouseIcon));
    //    }
    //    else if (points > 0)
    //    {
    //        StartCoroutine(redFlash(ratIcon));
    //    }
    //    else
    //    {
    //        return;
    //    }

    //    //if it goes over the max
    //    if ((reputationPoints += points) >= REPUTATION_MAX)
    //    {
    //        reputationPoints = REPUTATION_MAX;
    //    }
    //    //if it goes under the min
    //    else if ((reputationPoints += points) <= REPUTATION_MIN)
    //    {
    //        reputationPoints = REPUTATION_MIN;

    //    }
    //    //if its in the middle
    //    else
    //    {
    //        reputationPoints += points;
    //    }

    //    Upgrades();
    //    updateUI();
    //}

    ////---------------------------------------------------------------------------------------------------------------------------------------
    ////MAYBE MOVE THIS TO ITS OWN SCRIPT
    ////Determines if the player has gained enough reputation with a faction
    ////If they have they get an upgrade
    //private void Upgrades()
    //{
    //    PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();

    //    if (reputationPoints >= 70)
    //    {
    //        player.LoseMeleeAttackUpgrade();
    //        player.SlowDownTimeUpgrade();
    //    }
    //    else if (reputationPoints <= 30)
    //    {
    //        player.LoseSlowDownTimeUpgrade();
    //        player.MeleeAttackUpgrade();
    //    }
    //}
    ////-------------------------------------------------------------------------------------------------------------------------------------------

    //private void updateUI()
    //{
    //    ReputationSliderImage.fillAmount = (float)reputationPoints / 100;
    //}

    //private IEnumerator feedback(Text t, string s)
    //{
    //    t.text = s;
    //    t.enabled = true;
    //    yield return new WaitForSeconds(1.0f);
    //    t.enabled = false;
    //}

    //private IEnumerator redFlash(Image icon)
    //{
    //    for (int i = 0; i < 6; i++)
    //    {
    //        icon.color = Color.red;
    //        yield return new WaitForSeconds(0.2f);
    //        icon.color = Color.white;
    //        yield return new WaitForSeconds(0.2f);
    //    }

    //    icon.color = Color.white;
    //}
}
