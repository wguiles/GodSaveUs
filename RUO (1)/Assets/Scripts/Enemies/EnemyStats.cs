using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    //navMesh Stuff
    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected int health;
    protected int initialHealth;

    [SerializeField] private int speed;
    [SerializeField] private int attackDamage;
    [SerializeField] private Image enemyHealthBar;

    public int numCheese = 1;
    public GameObject CheeseStuff;
    public GameObject enemyDeathExplosion;

    //private bool isAggressive;
    private bool wasAttacked;
    private int factionPoints;
    private float startDistFromPlayer;
    private bool followingPlayer;

    protected bool isTarget = false;
    [SerializeField] private GameObject target;
    protected Objective assignedObjective;

    public enum FactionType { mouse, rat };
    public FactionType faction;
    public PlayerStats playerStats;
    public PlayerController playerController;

    //Animation Stuff
    public Animator _animator;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        initialHealth = health;
        agent = GetComponent<NavMeshAgent>();
        _animator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");

    }

    // Start is called before the first frame update
    public void Start()
    {
        _animator = transform.GetChild(1).GetComponent<Animator>();



        Debug.Log((_animator.GetParameter(1).name));
        //Debug.Log(_animator == null);

        //Debug.Log("Animator name " + _animator.gameObject.name);

        wasAttacked = false;
        startDistFromPlayer = Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        //this.transform.Find("Target").GetComponent<SpriteRenderer>().enabled = false;
    }

    public float GetStartDistFromPlayer()
    {
        return startDistFromPlayer;
    }

    public int GetHealth()
    {
        
        return health;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public FactionType GetFactionType()
    {
        return faction;
    }

    public void SetHealth(int amount)
    {
        health = amount;
    }

    public void SetSpeed(int amount)
    {
        speed = amount;
    }

    public void SetAttackDamage(int amount)
    {
        attackDamage = amount;
    }

    public void SetFactionType(FactionType type)
    {
        faction = type;
    }

    public virtual void TakeDamage(int amount)
    {
        //_animator = GetComponentInChildren<Animator>();
        //SoundManager.instance.PlaySound("EnemyHurtS");
        SoundManager.instance.PlayRandomSqueak();
        StartCoroutine(Flinch());
        health -= amount;
        UpdateHealthBar();
        //if they die
       /* if (health <= 0 && this.gameObject.tag == "Runner")
        {
            int runnerCheese = this.GetComponent<RunnerScript>().GetCheeseAmount();
            Destroy(this.gameObject);
        }
        else if (health <= 0 && this.gameObject.tag != "Runner")
        {
            Die();
        }*/
        //if they don't die and were hit by the player
        if (health > 0 && wasAttacked)
        {
            wasAttacked = false;
            //GameObject.FindObjectOfType<SpawnTargetScript>().SetTargetIsActive(false);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    private IEnumerator Flinch()
    {
        GetSpriteRenderer(transform.GetChild(0)).color = Color.red;
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Hit");
        float initalspeed = agent.speed;
        agent.speed = 0;

        yield return new WaitForSeconds(0.5f);

        agent.speed = initalspeed;
        GetSpriteRenderer(transform.GetChild(0)).color = Color.white;
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");
    }

    public virtual void Die()
    {
        //if it's a rat that the player attacked, modify reputation points accordingly
        /* if (faction == FactionType.rat && wasAttacked)
         {
             Debug.Log("Killed a rat: " + GameObject.Find("FactionReputation").GetComponent<FactionReputation>().GetReputationPoints());
             GameObject.Find("FactionReputation").GetComponent<FactionReputation>().ModifyReputationPoints(2);
             Debug.Log("Modified Reputation: " + GameObject.Find("FactionReputation").GetComponent<FactionReputation>().GetReputationPoints());
         }
         //if it's a mouse that the player attacked, modify reputation points accordingly
         else if (faction == FactionType.mouse && wasAttacked)
         {
             Debug.Log("Killed a Mouse");
             GameObject.Find("FactionReputation").GetComponent<FactionReputation>().ModifyReputationPoints(-2);
         }*/

        if (SpawnTargetScript.instance.GetTargetIsActive() && isTarget)
        {
            //assignedObjective.Complete();
            isTarget = false;
            numCheese = 20;
            SoundManager.instance.PlaySound("KillConfirmed");
            GameObject.FindGameObjectWithTag("Level").GetComponent<LevelStats>().AddTime();
            SpawnTargetScript.instance.SetTargetIsActive(false);
        }

        if (faction == FactionType.rat)
        {
            SpawnManager.instance.RatCount--;
        }
        else
        {
            SpawnManager.instance.MouseCount--;
        }

        for (int i = 0; i < numCheese; i++)
        {
            Instantiate(CheeseStuff, transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0f), Quaternion.identity);
        }

        SoundManager.instance.PlaySound("EnemyDeath");
        Instantiate(enemyDeathExplosion, transform.position, Quaternion.identity);

        Invoke("DestroyEnemy", 0.3f);
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------
    //THIS SHOULD BE MANAGED BY THE PLAYER
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "slash")
        {
                Destroy(other.gameObject);
                wasAttacked = true;
                StartCoroutine(TakeDamageIE());
                TakeDamage(playerController.GetSlashDamage());
        }
    }

    public void OnCollisionEnter(Collision other)
    {
         if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<PlayerController>().GetIsDashing())
        {
            other.gameObject.GetComponent<PlayerStats>().TakeDamage(this.attackDamage);
        }
        if (other.gameObject.tag == "slash")
        {
            //Debug.Log("This is how the player was killed");

            Destroy(other.gameObject);
            wasAttacked = true;
            StartCoroutine(TakeDamageIE());
            TakeDamage(playerController.GetSlashDamage());
        }
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------

    IEnumerator TakeDamageIE()
    {
        GetSpriteRenderer(transform.GetChild(0)).color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetSpriteRenderer(transform.GetChild(0)).color = Color.white;
    }

    protected SpriteRenderer GetSpriteRenderer(Transform t)
    {
        if (t.gameObject.name == "Sprite")
        {
            return t.gameObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            return GetSpriteRenderer(t.parent.GetChild(t.GetSiblingIndex() + 1));
        }
    }

    protected float DistanceToPlayer()
    {
        if (playerStats == null)
        {
            return 0f;
        }

        return Vector3.Distance(playerStats.transform.position, transform.position);
    }

    protected void FollowPlayer()
    {
        followingPlayer = true;
    }

    protected void UnfollowPlayer()
    {
        followingPlayer = false;
    }

    public virtual void Update()
    {
        if (followingPlayer)
        {
            agent.SetDestination(playerStats.transform.position);
        }
        ////If its a target it has a target on it
        //if(isTarget && !this.transform.Find("Target").GetComponent<SpriteRenderer>().enabled)
        //{
        //    Debug.Log("Enabling target");
        //    this.transform.Find("Target").GetComponent<SpriteRenderer>().enabled = true;
        //}
    }

    public void SetIsTarget(bool boolToSet)
    {
        isTarget = boolToSet;

        if (isTarget == true)
        {
            Debug.Log("IS targt being set to Poo");
        }
    }

    public bool GetIsTarget()
    {
        return isTarget;
    }


    public void setAssignedObjective(Objective obj)
    {
        assignedObjective = obj;
    }

    private void UpdateHealthBar()
    {
        enemyHealthBar.fillAmount = ((float)health / initialHealth);
    }
    
    //public void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Wall")
    //    {
    //        ChangeDirection();
    //    }
    //}

    //private void ChangeDirection()
    //{
    //    float newX = gameObject.transform.position.x * -1;
    //    float newY = gameObject.transform.position.y * -1;
    //    Vector3 newDest = new Vector3(newX, newY);

    //    agent.SetDestination(newDest);
    //}
}
