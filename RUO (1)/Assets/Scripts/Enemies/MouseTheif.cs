using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTheif : RunnerScript
{
    // Start is called before the first frame update

    [SerializeField]
    private float cheeseStealDistance;

    private GameObject[] hideyHoles;

    private GameObject exit = null;

    private bool stoleCheese;
    private bool runningAway;

    [SerializeField]
    private GameObject cheeseSprite;
    
    void Start()
    {
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");

        hideyHoles = GameObject.FindGameObjectsWithTag("Hidey Hole");

        initialHealth = health;
    }

    // Update is called once per frame
    public override void Update()
    {

        base.Update();

        if (!stoleCheese)
        {
            FollowPlayer();
        }

        if (DistanceToPlayer() <= cheeseStealDistance && !stoleCheese)
        {

            if (isTarget && !runningAway)
            {
                runningAway = true;
                RunAway();
                return;
            }
            transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Idle");

            UnfollowPlayer();
            GoHome();
            stoleCheese = true;
            StealCheese(1, 0);
            cheeseSprite.SetActive(true);
        }

        if (exit != null && Vector3.Distance(transform.position, exit.transform.position) <= 5.0f)
        {
            SetIsTarget(false);


            if (assignedObjective != null)
            {
                Debug.Log("Failed obshmective");
                assignedObjective.Fail();
            }

            Destroy(gameObject);
        }

        Debug.DrawLine(transform.position, agent.destination, Color.red);
    }

     void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void RunAway()
    {

        Vector3 randomPoint = GameObject.FindGameObjectsWithTag("BossPoint")[Random.Range(0, (int)GameObject.FindGameObjectsWithTag("BossPoint").Length)].transform.position;

        agent.SetDestination(randomPoint);

        //agent.SetDestination((transform.position - playerStats.transform.position) * 3f);
        //Debug.Log((transform.position - playerStats.transform.position).ToString());
        //Debug.Log("Agent Destination" + agent.destination.ToString());

        //Debug.DrawLine(playerStats.transform.position, agent.destination);
        //if (distancetoplayer < 3)
        //agent.isStopped = true;
        //else
        //agent.isStopped = false;
    }

    public void GoHome()
    {
        Debug.Log("Generating Random Hidey Hole");
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");
        int randomNum = Random.Range(0, hideyHoles.Length);
        exit = hideyHoles[randomNum];
        agent.SetDestination(exit.transform.position);
    }
}
