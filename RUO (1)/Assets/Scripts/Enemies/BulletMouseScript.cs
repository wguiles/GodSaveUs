using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BulletMouseScript : MonoBehaviour
{
    

    public GameObject bullet;
    public float fireRate;
    public float nextFire;

    private SpriteRenderer canon;
    private float t;
    private NavMeshAgent agent;
    private bool timeToFire;
    
    // Use this for initialization
    void Start()
    {
        //fireRate = 1f;
        nextFire = Time.time;
        canon = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        agent = transform.parent.parent.GetComponent<NavMeshAgent>();


    }
    // Update is called once per frame
    void Update()
    {
        CheckIfTimeToFire();

    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire && Vector3.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= 10.0f
            && !timeToFire)
        {
            nextFire = Time.time + fireRate;
            timeToFire = true;
            StartCoroutine(Fire());
        }


        if (timeToFire)
            LerpCannonColor();
    }

    private void LerpCannonColor()
    {
        canon.color = Color.Lerp(canon.color, new Color(0, 140, 255), t);

        t += Time.deltaTime * 100f;
    }

    private IEnumerator Fire()
    {
        //stop the bruiser
        float initialSpeed = agent.speed;
        agent.speed = 0f;


        yield return (canon.color == new Color(0, 140f, 255));

        agent.speed = initialSpeed;
        SoundManager.instance.PlaySound("BulletSoundS");
        Instantiate(bullet, transform.position, Quaternion.identity);

        timeToFire = false;
    }


}
