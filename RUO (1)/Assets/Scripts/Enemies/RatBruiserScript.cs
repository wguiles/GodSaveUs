using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBruiserScript : EnemyStats
{
    int numEnemies = 10;
    public GameObject mouseBabies;
    private GameObject player;
    private int rng;
    private bool reachedPatrolPoint;
    private bool isAttacking;
    private bool isPatroling = true;
    private float attackDistance;


    [SerializeField] protected GameObject[] patrolPoints; //Note: there is no setter


    private void Awake()
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("RatBossPoint");
    }

    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoints = GameObject.FindGameObjectsWithTag("RatBossPoint");
        reachedPatrolPoint = true;
    }

    // Update is called once per frame
    public override void Update()
    {

        //Determines if they reached a patrol point
        if (agent.remainingDistance <= 0.5f)
        {
            reachedPatrolPoint = true;
        }

        //Picks new patrol point when they reach one
        if (reachedPatrolPoint && isPatroling)
        {
            reachedPatrolPoint = false;
            Patrol();
        }

        //Determines if they are attacking
        if (isAttacking)
        {
            Attack();
        }

        FollowPlayer();

        base.Update();

        if (DistanceToPlayer() <= attackDistance && !isAttacking)
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            agent.isStopped = false;
        }

    }

    public GameObject[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    //Determies which faction it was part of and spawns new Enforcer for the appropriate faction
    //Uses the EnemyStats (inherited) Die() method


    //Enforcer randomly chooses a patrol point and moves towards it
    public void Patrol()
    {
        //Choses a random patrol point
        int rngMax = GetPatrolPoints().Length;
        rng = Random.Range(0, rngMax);
        //Moves to that patrol point
        agent.destination = GetPatrolPoints()[rng].transform.position;
    }

    //Tracks the position of the player so they can attack it.
    private Vector3 TrackPlayer()
    {
        return player.transform.position;
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    //SOME OF THIS WILL GET MOVED TO PLAYER SCRIPT
    //Attack the player if they are in range
    private void OnCollisionEnter(Collision coll)
    {
        base.OnCollisionEnter(coll);

        if (coll.gameObject.tag == "Player")
        {
            //If the player is dashing
            if (!coll.gameObject.GetComponent<PlayerController>().GetIsDashing())
            {
                isAttacking = true;
                

                StartCoroutine(damageOverTime());
            }
            else
            {
                //Destroy(gameObject);
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------------------

    //Nicole is unsure of what exactly this does. Someone please look into it and comment it.
    private void OnCollisionExit(Collision collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<PlayerController>().GetIsDashing())
            {
                isAttacking = false;
            }
            else
            {
                //Destroy(gameObject);
            }

        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------
    //NEEDS TO BE MOVED TO ENEMYSTATS SCRIPT
    private IEnumerator damageOverTime()
    {
        while (isAttacking)
        {
            yield return new WaitForSeconds(2.0f);
            player.GetComponent<PlayerStats>().TakeDamage(this.GetAttackDamage());
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------

    //Follow player when they move in range
    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isPatroling = false;
            Debug.Log("players here");
            // reachedPatrolPoint = false;
            agent.isStopped = false;
            agent.destination = (transform.position - playerStats.transform.position) * 3f;
            Debug.DrawLine(playerStats.transform.position, agent.destination);

            //SetSpeed(15);
            // agent.destination = TrackPlayer();
        }
    }

    //Patrols when player is out of range
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            isPatroling = true;
            reachedPatrolPoint = true;
            //agent.isStopped = true;
            SetSpeed(10);
            Patrol();
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------
    //MOVE TO ENEMYSTATS SCRIPT
    //Attacks the player
    private void Attack()
    {
        player.GetComponent<PlayerStats>().TakeDamage(this.GetAttackDamage());
        isAttacking = false;
    }
    //spawns thiefs from the dead body of this mouse
    public override void Die()
    {
        Debug.Log("plz die");
        

        Destroy(this.gameObject);
    }
    
}
