using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnforcerScript : EnemyStats
{
    [SerializeField] protected GameObject[] patrolPoints; //Note: there is no setter

    private GameObject player;
    private int rng;
    protected bool reachedPatrolPoint;
    private bool isAttacking;

    [SerializeField] private float attackRadius;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        initialHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        reachedPatrolPoint = true;
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (DistanceToPlayer() <= attackRadius)
        {
            FollowPlayer();
        }
        else
        {
            UnfollowPlayer();
        }

        //Determines if they reached a patrol point
        if (agent.remainingDistance <= 0.5f)
        {
            reachedPatrolPoint = true;
        }

        //Picks new patrol point when they reach one
        if (reachedPatrolPoint)
        {
            reachedPatrolPoint = false;
            Patrol();
            
        }

        //Determines if they are attacking
        if (isAttacking)
        {
            Attack();
            
        }
    }

    public GameObject[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    //Determies which faction it was part of and spawns new Enforcer for the appropriate faction
    //Uses the EnemyStats (inherited) Die() method
    public override void Die()
    {
        //if (faction == FactionType.rat)
        //{
        //    GameObject.Find("Spawner").GetComponent<SpawnerScript>().SpawnRatEnforcer();
        //}
        //else
        //{
        //    GameObject.Find("Spawner").GetComponent<SpawnerScript>().SpawnMouseEnforcer();
        //}
        //Uses the Die() method in EnemyStats
        base.Die();
    }

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
        if (coll.gameObject.tag == "Player")
        {
            //If the player is dashing
            //if (!coll.gameObject.GetComponent<PlayerController>().GetIsDashing())
            //{
                
                isAttacking = true;
                StartCoroutine(damageOverTime());
                StartCoroutine(SpeedNormal());
            //}
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
            //else
            //{
            //    Destroy(gameObject);
            //}

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
            
            
            //SetSpeed(15);
            agent.destination = TrackPlayer();
        }
    }

    //Patrols when player is out of range
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            //SetSpeed(10);
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
     IEnumerator SpeedNormal()
    {
        Debug.Log("speed lowered");
    
        //player.GetComponent<PlayerController>().speed = 0
        yield return new WaitForSeconds(1.5f);
        Debug.Log("speed normal");
        //PlayerController.speed = 10.0f; 
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------
}

