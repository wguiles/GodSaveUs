using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BruiserScript : EnemyStats
{
    int numEnemies = 5;
    public GameObject mouseBabies;
    private GameObject player;
    private int rng;
    //private NavMeshAgent agent;
    private bool reachedPatrolPoint;
    private bool isAttacking;
    private bool isPatroling = true;

    [SerializeField] protected GameObject[] patrolPoints; //Note: there is no setter
    

    //private void Awake()
    //{
    //    patrolPoints = GameObject.FindGameObjectsWithTag("BossPoint");
    //    agent = GetComponent<NavMeshAgent>();
    //}

    void Start()
    {
        initialHealth = health;

        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");

        player = GameObject.FindGameObjectWithTag("Player");
        patrolPoints = GameObject.FindGameObjectsWithTag("BossPoint");

        reachedPatrolPoint = true;
    }

    // Update is called once per frame
   public override void Update()
    {

        ////Determines if they reached a patrol point
        //if (agent.remainingDistance <= 0.5f)
        //{
        //    reachedPatrolPoint = true;
        //}

        ////Picks new patrol point when they reach one
        //if (reachedPatrolPoint && isPatroling)
        //{
        //    reachedPatrolPoint = false;
        //    Patrol();
        //}

        ////Determines if they are attacking
        //if (isAttacking)
        //{
        //    Attack();
        //}
        base.Update();
        FollowPlayer();

        
    }

    public GameObject[] GetPatrolPoints()
    {
        return patrolPoints;
    }

    public override void TakeDamage(int amount)
    {
        //StartCoroutine(Flinch());

        base.TakeDamage(amount);
    }

    private IEnumerator Flinch()
    {
        //float intialSpeed = agent.speed;
        //agent.speed = 0f;
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Hit");
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.5f);

        //agent.speed = intialSpeed;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.white;
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");
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

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
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
            agent.destination = (transform.position - player.transform.position) * 3f;
            Debug.DrawLine(player.transform.position, agent.destination);

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

        StartCoroutine(SpawnChildren());

       

    }

    private IEnumerator SpawnChildren()
    {
        Debug.Log("it gets to spawn children");
        yield return new WaitForSeconds(.1f);
        Debug.Log("after spawning children");

        //for (int i = 0; i < numEnemies; i++)
        //{
        //    Instantiate(mouseBabies, transform.position, Quaternion.identity); 
        //}

        base.Die();
    }
}
