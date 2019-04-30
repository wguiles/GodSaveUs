using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float mineRecharge;
    private int cheeseAmount;
    [SerializeField] private bool canMove;
    private bool canMelee;
    private MovementType moveType = MovementType.standard;
    public float speed = 10f;
    public float fireRate;

    public bool gameStarted;


    [Header("Dash")]
    public float dashSpeed;
    public float dashTime;
    public float dashRechargeTime;
    private Vector3 dashDirection;
    private bool isDashing = false;
    private bool canDash = true;

    private float fireTime;
    private float direction = 1.0f;
    private float timer;
    private float dashChargeTime = 0.2f;
    private GameObject gunChild;
    private GameObject slashObject;
    public GameObject mine;
    public GameObject slashProjectile;
    private Vector3 moveDirection;
    private PlayerStats _playerStats;
    //upgrades
    private bool timeFreezeUpgrade = false;

    public enum MovementType { standard, scurry, auto };
    RaycastHit hit;

    //Nicole added these in becuase they were hardcoded elsewhere
    public int slashDamage = 2;
    public int mineDamage = 6;

    private bool triggerInUse;

    void Start()
    {
        GetComponentInChildren<Animator>().SetTrigger("Idle");

        canMove = true;
        canMelee = true;

        _playerStats = GetComponent<PlayerStats>();

        gunChild = transform.GetChild(0).gameObject;
        slashObject = gunChild.transform.GetChild(0).GetChild(0).gameObject;
        hit = new RaycastHit();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {

            foreach (CheeseScript g in GameObject.FindObjectsOfType<CheeseScript>())
            {

                    if (Vector3.Distance(transform.position, g.transform.position) <= 6.0f)
                    {
                    Vector3 directionToPlayer = (transform.position - g.transform.position);

                    g.transform.Translate(directionToPlayer * Time.deltaTime * 10f);
                }

            }

            //RaycastHit 1f in front of player
            Physics.Raycast(this.transform.position, gunChild.transform.up, out hit, 2f);
            Debug.DrawRay(this.transform.position, gunChild.transform.up);

            fireTime += Time.deltaTime * fireRate;

            if (!isDashing && canMove)
            {

                transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed * Time.deltaTime * direction);

                // if (Input.GetButton("A Button") || Input.GetButton("A Button Windows"))
                // {
                //    timer += Time.deltaTime;
                //}

                // if (timer >= dashChargeTime)
                //{
                //   direction = 0.0f;
                //  gunChild.SetActive(true);
                //}

                if (((Input.GetButtonDown("Left Bumper") && canDash) || (Input.GetButtonDown("A Button Windows") && canDash) && !MenuManager.isPaused))
                {
                    timer = 0.0f;

                    dashDirection = gunChild.transform.up;

                    if (hit.collider == null)
                    {
                        StartCoroutine(Dashing());
                    }


                    timer = 0.0f;
                    direction = 1.0f;
                    hit = new RaycastHit();
                }

                if (canMelee && (Input.GetAxis("Right Trigger") >= 1.0f && !triggerInUse))
                {
                    triggerInUse = true;
                    StartCoroutine(Attack());
                }
                else if (Input.GetButton("Y Button") && fireTime > 1.0f
                    || (Mathf.Abs(Input.GetAxis("RightStickX")) >= 0.2f && fireTime > 1.0f || Mathf.Abs(Input.GetAxis("RightStickY")) >= 0.2f) && fireTime > 1)
                {
                    SoundManager.instance.PlaySound("PlayerFired");
                    fireTime = 0.0f;
                    LaunchProjectile();
                }

            }
            else if (canMove)
            {
                Dash();
            }

            SwitchObjectives();
        }

        if (Input.GetAxis("Right Trigger") <= 0.0f)
        {
            triggerInUse = false;
        }
    }

    public int GetSlashDamage()
    {
        return slashDamage;
    }

    public void SetSlashDamage(int amount)
    {
        slashDamage += amount;
    }

    public int GetMineDamage()
    {
        return mineDamage;
    }

    public void SetMineDamage(int amount)
    {
        mineDamage += amount;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float amount)
    {
        speed += amount;
    }

    void SwitchObjectives()
    {
        //if (Input.GetButtonDown("Right Bumper"))
        //{
        //    GameObject.FindObjectOfType<ObjectiveManager>().ToggleObjective(1);
        //}
        //else if (Input.GetButtonDown("Left Bumper"))
        //{
        //    GameObject.FindObjectOfType<ObjectiveManager>().ToggleObjective(-1);
        //}
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
    }

    public void SetCanMove(bool ableToMove)
    {
        canMove = ableToMove;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    //Changes the player's speed to dashing
    private void Dash()
    {
        //check destination
        transform.Translate(dashDirection * Time.deltaTime * dashSpeed);
    }

    //Activates the player's attack UI
    private IEnumerator Attack()
    {
        canMelee = false;
        SoundManager.instance.PlaySound("SlashS");
        //slashObject.GetComponent<SpriteRenderer>().flipX = !slashObject.GetComponent<SpriteRenderer>().flipX;
        //slashObject.SetActive(true);

        //yield return new WaitForSeconds(0.05f);

        //slashObject.SetActive(false);
        DropMine();

        yield return new WaitForSeconds(mineRecharge);

        canMelee = true;
    }

    public void DropMine()
    {
        GameObject mine = Instantiate(this.mine, transform.position, Quaternion.identity);
        //mine.GetComponent<ProjectileScript>().StartTimer();
    }

    public void LaunchProjectile()
    {
        GameObject bullet = Instantiate(slashProjectile, transform.position, Quaternion.identity);
        bullet.GetComponent<PlayerBulletScript>().SetTransformUp(gunChild.transform.up);
    }

    //Makes the sprite reflect the dashing movement
    private IEnumerator Dashing()
    {
        GetComponentInChildren<Animator>().SetTrigger("Dashing");
        SoundManager.instance.PlaySound("DashAttackS");

        canDash = false;
        slashObject.SetActive(true);
        isDashing = true;

        gameObject.layer = 9;

        yield return new WaitForSeconds(dashTime);
        GetComponentInChildren<Animator>().SetTrigger("Idle");

        slashObject.SetActive(false);
        isDashing = false;
        gameObject.layer = 0;

        yield return new WaitForSeconds(0.6f);
        direction = 1.0f;
        canDash = true;


    }

    //If the player is dashing the enemy is destroyed
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("Wall"))
        //{
        //    canDash = false;
        //}

        //if (collision.gameObject.tag == "Dank" && isDashing)
        //{
        //    Destroy(collision.gameObject);
        //}

        if (collision.gameObject.tag == "RatShield")
        {
            _playerStats.TakeDamage(2);
        }
    }

    //Changes what the player can do depending on what they come into contact with
    private void OnTriggerEnter(Collider other)
    {
        //Player picks up cheese
        if (other.gameObject.tag == "Cheese")
        {
            _playerStats.CollectCheese(other.GetComponent<CheeseScript>().GetCheesePoints());
            SoundManager.instance.PlaySound("CollectCheese");
            Destroy(other.gameObject);
        }
        //Player turns into cheese to the Mice
        else if (other.gameObject.tag == "Mice_Hideout")
        {
            //GameObject.Find("FactionReputation").GetComponent<FactionReputation>().AddToMice(_playerStats.GetCheeseAmount());
            //GameObject.Find("FactionReputation").GetComponent<FactionReputation>().ModifyReputationPoints(_playerStats.GetCheeseAmount());
            _playerStats.LoseCheese(_playerStats.GetCheeseCount());
        }
        //Player turns in cheese to the Rats
        else if (other.gameObject.tag == "Rats_Hideout")
        {
            //GameObject.Find("FactionReputation").GetComponent<FactionReputation>().AddToRats(_playerStats.GetCheeseAmount());
            //GameObject.Find("FactionReputation").GetComponent<FactionReputation>().ModifyReputationPoints(-_playerStats.GetCheeseAmount());
            _playerStats.LoseCheese(_playerStats.GetCheeseCount());
        }

        else if (other.gameObject.tag == "RatShield")
        {
            _playerStats.TakeDamage(2);
        }

    }

}