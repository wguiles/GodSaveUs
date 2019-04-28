using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private const int OBJECTIVES_MAX = 1;
    private int currlev;
    public GameObject package;
    //public float killTimeBonus = 5f;

    [SerializeField] public List<Objective> playerObjectives;
    Objective.objectiveDifficulty currentDifficulty;
    Objective.objectiveDifficulty secondaryDifficulty;
    int currentObjective = 0;

    // Start is called before the first frame update
    void Start()
    {
        currlev = FindObjectOfType<MenuManager>().GetCurrentLevel();
        playerObjectives.Capacity = 1;
    }

    public void CreateStartingList()
    {
        playerObjectives = new List<Objective>();

        for (int index = 0; index < OBJECTIVES_MAX; index++)
        {
            //Debug.Log("Adding Objective");
            //first index is assasination
            playerObjectives.Add(GenerateAssasinationObjective());
            //second index is package
            //playerObjectives.Add(GeneratePackageObjective());
        }
    }

    // Update is called once per frame
    void Update()
    {

        DisplayObjectives();

        //Checks if the list has any completed objectives

        if (playerObjectives.Count == 0)
        {
            playerObjectives.Add(GenerateAssasinationObjective());
        }

        foreach (Objective obj in playerObjectives.ToArray())
        {
            //If there is a completed objective

            if (obj == null)
            {
                playerObjectives.Remove(obj);
                Destroy(obj);
                playerObjectives.Add(GenerateAssasinationObjective());
                
            }
             else if (obj.GetIsCompleted())
            {
                //Adds time to the clock when an objective has been completed
                //FindObjectOfType<MenuManager>().GetCurrentLevelObject().GetComponent<LevelStats>().timeLeft += killTimeBonus;


                FindObjectOfType<PlayerStats>().CollectCheese(100);

                Objective newObjective = null;
                //Determines which type of objective it was and generates a replacement objective
                if (obj.GetComponent<AssassinationObjective>().isActiveAndEnabled)
                {
                    newObjective = GenerateAssasinationObjective();
                }
                else if (obj.GetComponent<PackageObjective>().isActiveAndEnabled)
                {
                    newObjective = GeneratePackageObjective();
                }

                //newObjective = GenerateAssasinationObjective();

                //Remove the objective from the list
                Objective objToDestroy = obj;
                playerObjectives.Remove(obj);


                //Removes the completed objective
                Destroy(obj);

                //Adds new objective to list
                playerObjectives.Add(newObjective);
            }
            else if (obj.GetIsFailed())
            {
                //Debug.Log("Objective Failed");


                Objective objToDestroy = obj;

                playerObjectives.Remove(obj);

                Destroy(objToDestroy);

                //playerObjectives.Add(GenerateObjective());
            }



        }

    }

    public void ToggleObjective(int objectiveDirection)
    {
        currentObjective += objectiveDirection;

        //Debug.Log("OBJDIR: " + objectiveDirection.ToString());

        if (currentObjective > 3)
        {
            currentObjective = 0;
        }
        else if (currentObjective < 0)
        {
            currentObjective = 3;
        }
    }

    void DisplayObjectives()
    {
        if (currentObjective > 3 || currentObjective < 0)
        {
            return;
        }

        //point arrow towards current Objective
       // GameUIManager.ui_Manager.SetTargetVector(playerObjectives[currentObjective].GetGoalLocation());

        //set text of current objective
//        GameUIManager.ui_Manager.SetObjectiveText(playerObjectives[currentObjective].objectiveText);
        //activate icons above enemies if assasination
    }

    //Generates an assasination objective
    private Objective GenerateAssasinationObjective()
    {
        //Gets the current level in order to determine difficulties
        currlev = FindObjectOfType<MenuManager>().GetCurrentLevel();

        //Randomly picks between two difficulties based on the level
        if (currlev == 1)
        {
            currentDifficulty = Objective.objectiveDifficulty.easy;
            secondaryDifficulty = Objective.objectiveDifficulty.easy;
        }
        else if (currlev == 2)
        {
            currentDifficulty = Objective.objectiveDifficulty.easy;
            secondaryDifficulty = Objective.objectiveDifficulty.medium;
        }
        else if (currlev == 3)
        {
            currentDifficulty = Objective.objectiveDifficulty.medium;
            secondaryDifficulty = Objective.objectiveDifficulty.medium;
        }
        else if (currlev == 4)
        {
            currentDifficulty = Objective.objectiveDifficulty.medium;
            secondaryDifficulty = Objective.objectiveDifficulty.hard;
        }
        else if (currlev == 5)
        {
            currentDifficulty = Objective.objectiveDifficulty.hard;
            secondaryDifficulty = Objective.objectiveDifficulty.hard;
        }

        //Randomly picks the objective difficulty
        int objDiff = Random.Range(0, 2);

        //Sets the objective dificulty (default is easy)
        Objective.objectiveDifficulty tempDifficulty = Objective.objectiveDifficulty.easy;
        if (objDiff == 0)
        {
            tempDifficulty = currentDifficulty;
        }
        else if (objDiff == 1)
        {
            tempDifficulty = secondaryDifficulty;
        }

        //generate an assasination
        AssassinationObjective assasinate = gameObject.AddComponent<AssassinationObjective>();
        assasinate.SetDifficulty(tempDifficulty);
        assasinate.GetComponent<AssassinationObjective>().FindTarget();
        return assasinate;
    }

    //Generates a random objective
    private Objective GeneratePackageObjective()
    {
        ////Gets the current level in order to determine difficulties
        //currlev = GetComponent<MenuManager>().GetCurrentLevel();

        ////Randomly picks between two difficulties based on the level
        //if (currlev == 1)
        //{
        //    currentDifficulty = Objective.objectiveDifficulty.easy;
        //    secondaryDifficulty = Objective.objectiveDifficulty.easy;
        //}
        //else if (currlev == 2)
        //{
        //    currentDifficulty = Objective.objectiveDifficulty.easy;
        //    secondaryDifficulty = Objective.objectiveDifficulty.medium;
        //}
        //else if (currlev == 3)
        //{
        //    currentDifficulty = Objective.objectiveDifficulty.medium;
        //    secondaryDifficulty = Objective.objectiveDifficulty.medium;
        //}
        //else if (currlev == 4)
        //{
        //    currentDifficulty = Objective.objectiveDifficulty.medium;
        //    secondaryDifficulty = Objective.objectiveDifficulty.hard;
        //}
        //else if (currlev == 5)
        //{
        //    currentDifficulty = Objective.objectiveDifficulty.hard;
        //    secondaryDifficulty = Objective.objectiveDifficulty.hard;
        //}

        ////Randomly picks the objective type and difficulty
        //int objDiff = Random.Range(0, 2);

        ////Sets the objective dificulty (default is easy)
        //Objective.objectiveDifficulty tempDifficulty = Objective.objectiveDifficulty.easy;
        //if (objDiff == 0)
        //{
        //    tempDifficulty = currentDifficulty;
        //}
        //else if (objDiff == 1)
        //{
        //    tempDifficulty = secondaryDifficulty;
        //}

        //generate a package
        Objective getPackage = gameObject.AddComponent<PackageObjective>();
        getPackage.GetComponent<PackageObjective>().SpawnPackage();

        return getPackage;
    }
}