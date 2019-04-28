using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunnerScript : EnemyStats
{
    [SerializeField] private GameObject[] cheeseList;
    [SerializeField] private GameObject closestCheese;

    [SerializeField]
    protected int cheeseMax = 10;
    
    private int cheeseAmount;
    private GameObject player;

    [SerializeField]
    private float ratTriggerDistance;

    //private Vector3 hideoutLocation;
    //private NavMeshAgent agent;

    public GameObject cheesePrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //Determines which faction the runner is part of and sets its hideout location accordingly
        //if (faction == FactionType.mouse)
        //{
        //    //hideoutLocation = GameObject.Find("Mice_Hideout").transform.position;
        //}
        //else
        //{
        //    hideoutLocation = GameObject.Find("Rats_Hideout").transform.position;
        //}

        //Starts moving towards the closest cheese
        //Move(TrackCheese());
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //agent = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player");

        //Will need an AddCheese method at some point in order to manage the sprite
        //if (cheeseAmount >= 1)
        //{
        //    transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //}
        //If holding the max cheese go back to faction
        //if (cheeseAmount == CHEESE_MAX)
        //{
        //    Move(hideoutLocation);
        //}
        //Otherwise keep picking up cheese
        //else
        //if (DistanceToPlayer() < ratTriggerDistance)
        //{
        //    Move(playerStats.transform.position);
        //}

        {
            //Move(TrackCheese());

            ////What happens when they pick up cheese
            //if (Vector3.Distance(this.transform.position, closestCheese.transform.position) <= 0.5f)
            //{
            //    cheeseAmount++;
            //    Destroy(closestCheese.gameObject);
            //    closestCheese = null;

            //}
        }

        //If they turn cheese into their faction
        //if (Vector3.Distance(this.transform.position, hideoutLocation) < 0.5f)
        //{
        //    agent.isStopped = true;

        //    //Unloads cheese for 5 seconds then moves
        //    Invoke("WentHome", 5);
        //}

    }

    public int GetCheeseAmount()
    {
        return cheeseAmount;
    }

    //Resets cheese amount and starts them towards the next cheese
    //NOTE: Should only be used after they have turned in cheese
    private void WentHome()
    {
        cheeseAmount = 0;
        //The following line will be changed at some point (i.e. runners will be differentiated)
        transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        agent.isStopped = false;
        Move(TrackCheese());
    }

    public virtual void Move(Vector3 target)
    {
        agent.destination = target;
    }

    private Vector3 TrackCheese()
    {
        Vector3 runnerLocation = this.transform.position;
        cheeseList = null;
        //find cheese game objects
        cheeseList = GameObject.FindGameObjectsWithTag("Cheese");

        //first item in the list starts as the closest
        closestCheese = cheeseList[0];

        //find closest one in the list
        for (int index = 1; index < cheeseList.Length; index++)
        {
            //if the distance between the runner and the cheese in the list is less, change the closest cheese
            if (Vector3.Distance(closestCheese.transform.position, runnerLocation) > Vector3.Distance(cheeseList[index].transform.position, runnerLocation))
            {
                closestCheese = cheeseList[index];
            }
        }

        //return location of closest one
        return closestCheese.transform.position;
    }

    //If a runner is killed another is spawned 
    public override void Die()
    {
        //if (faction == FactionType.rat)
        //{
        //    GameObject.Find("Spawner").GetComponent<SpawnerScript>().SpawnRatRunner();
        //}
        //else
        //{
        //    //GameObject.Find("Spawner").GetComponent<SpawnerScript>().SpawnMouseRunner();
        //}

        for (int i = 0; i < cheeseAmount; i++)
        {
            Instantiate(cheesePrefab, transform.position, Quaternion.identity);
        }

        base.Die();
    }

    public virtual void StealCheese(int cheeseToSteal, float cheeseStealRate)
    {
        StartCoroutine(StealCheeseOverTime(cheeseToSteal, cheeseStealRate));
    }

    private IEnumerator StealCheeseOverTime(int cheeseToSteal, float cheeseStealRate)
    {
        while (cheeseToSteal > 0)
        {
            yield return new WaitForSeconds(cheeseStealRate / 100);
            playerStats.LoseCheese(1);
            cheeseToSteal--;
        }
    }
}
