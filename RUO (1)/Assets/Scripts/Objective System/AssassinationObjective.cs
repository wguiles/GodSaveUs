using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AssassinationObjective : Objective
{
    [SerializeField]
    private GameObject target;
    private bool isAlive;
    private GameObject player;
    public GameObject[] temp;

    public AssassinationObjective()
    {
        //change from kill mouse to kill rat based on level selection

    }

    // Start is called before the first frame update
    void Start()
    {
        //FindTarget();
        player = GameObject.FindGameObjectWithTag("Player");
        isAlive = true;
    }

    void Update()
    {

        //if (target == null)
        //{
        //    base.Complete();
        //    SetGoalLocation(new Vector3(0f, 0f, 0f));
        //}
        //else
        //{
        if (target != null)
        {
            SetGoalLocation(target.transform.position);
        }
        else
        {
            Complete();
        }
        //}
    }

    public override void Complete()
    {
        base.Complete();
        FindObjectOfType<PlayerStats>().CollectCheese(100);
        Destroy(this);
    }


    //Constructor for creating objective
    public void AssasinationDifficulty(objectiveDifficulty diff)
    {
        base.SetDifficulty(diff);
    }

    //Generates an array of enemies and selects a target based on the objectives difficulty
    public void FindTarget()
    {
        //Debug.Log("Find target has been called");

        //collects enemy objects into a list
        List<GameObject> initialTargets = new List<GameObject>();
        initialTargets.AddRange(GameObject.FindGameObjectsWithTag("Enforcer"));
        initialTargets.AddRange(GameObject.FindGameObjectsWithTag("Runner"));
        temp = initialTargets.ToArray();

        //sorts the list based on the distance from the player
        GameObject[] potentialTargets = initialTargets.ToArray(); //Converts list to array
        SortByDistance(potentialTargets, 0, potentialTargets.Length - 1);

        //prep for target selection
        int hardIndex = potentialTargets.Length - 1; //marks the endpoint of the array
        int easyIndex = hardIndex / 3; //marks the end point of the first third of the array
        int mediumIndex = (hardIndex / 3) * 2 ; //marks the end point of the second third of the array

        //the higher the difficulty the farther the distance of the selected target
        if (GetDifficulty() == objectiveDifficulty.easy)
        {
            //Randomly pick an enemy between the 0 index and the easyIndex
            int rnd = Random.Range(0, easyIndex);

            //if the current easy target is already a target select another in the list
            if (potentialTargets[rnd].GetComponent<EnemyStats>().GetIsTarget() && potentialTargets.Length != 0)
            {
                //iterate through the list till it finds an enemy that isn't targeted
                for (int index = 0; index <= potentialTargets.Length - 1; index++)
                {
                    if (!potentialTargets[index].GetComponent<EnemyStats>().GetIsTarget())
                    {
                        target = potentialTargets[index];
                        break;
                    }
                }
            }
            else
            {
                target = potentialTargets[rnd];
            }

        }
        else if (GetDifficulty() == objectiveDifficulty.medium)
        {
            //Randomly pick an enemy betweent the easyIndex and mediumIndex
            int rnd = Random.Range(easyIndex, mediumIndex);

            //if the current medium target is already a target select another in the list
            if (potentialTargets[rnd].GetComponent<EnemyStats>().GetIsTarget())
            {
                //iterate through the list till it finds an enemy that isn't targeted
                for (int index = easyIndex; index <= potentialTargets.Length; index++)
                {
                    if (!potentialTargets[index].GetComponent<EnemyStats>().GetIsTarget())
                    {
                        target = potentialTargets[index];
                        break;
                    }
                }
            }
            else
            {
                target = potentialTargets[rnd];
            }
        }
        else if (GetDifficulty() == objectiveDifficulty.hard)
        {
            //Randomly pick an enemy betweent the mediumIndex and hardIndex
            int rnd = Random.Range(mediumIndex, hardIndex);

            //if the current hard target is already a target select another in the list
            if (potentialTargets[rnd].GetComponent<EnemyStats>().GetIsTarget())
            {
                //iterate through the list till it finds an enemy that isn't targeted
                for (int index = mediumIndex + 1; index <= potentialTargets.Length; index++)
                {
                    if (!potentialTargets[index].GetComponent<EnemyStats>().GetIsTarget())
                    {
                        target = potentialTargets[index];
                        break;
                    }
                }
            }
            else
            {
                target = potentialTargets[rnd];
            }
        }


          target = potentialTargets[Random.Range(0, potentialTargets.Length)];

        objectiveText = "Kill";
        SetGoalLocation(target.transform.position);
        target.GetComponent<EnemyStats>().SetIsTarget(true);
        target.GetComponent<EnemyStats>().setAssignedObjective(this);
    }

    private void SortByDistance(GameObject[] array, int first, int last)
    {
        if (array[first].GetComponent<EnemyStats>().GetStartDistFromPlayer() < array[last].GetComponent<EnemyStats>().GetStartDistFromPlayer())
        {
            // Partitioning index
            int part = partition(array, first, last);

            if (part > 1)
            {
                SortByDistance(array, first, part - 1);
            }
            if (part + 1 < last)
            {
                SortByDistance(array, part + 1, last);
            }
        }
    }


    int partition(GameObject[] array, int first, int last)
    {
        GameObject x = array[last];
        int i = (first - 1);

        for (int j = first; j <= last - 1; j++)
        {
            if (array[j].GetComponent<EnemyStats>().GetStartDistFromPlayer() <= x.GetComponent<EnemyStats>().GetStartDistFromPlayer())
            {
                i++;
                Swap(array[i], array[j]);
            }
        }
        Swap(array[i + 1], array[last]);
        return (i + 1);
    }

    public static void Swap(GameObject first, GameObject last)
    {
        GameObject temp;

        temp = first;
        first = last;
        last = temp;
    }
}
