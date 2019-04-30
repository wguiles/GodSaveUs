using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : BasicRatEnforcer
{
    public float explodeDistance;
    public GameObject explosion;
    bool exploding;
    public float detonationTime;

    [SerializeField]
    private SpriteRenderer[] bombLights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanceToPlayer() <= explodeDistance && !exploding)
        {
            exploding = true;

            StartCoroutine(Kaboom());
        }
         
        base.Update();
    }

    IEnumerator Kaboom()
    {
        GetComponent<BoxCollider>().enabled = false;
        agent.speed = 0;
        _animator.SetTrigger("Idle");

        float timesToFlash = 0.0f;

        while (timesToFlash <= detonationTime)
        {
            SoundManager.instance.PlaySound("RatExplosionBeep");

            for (int i = 0; i < bombLights.Length; i ++)
            {
                bombLights[i].color = Color.red;
            }

            yield return new WaitForSeconds(0.2f);

            for (int i = 0; i < bombLights.Length; i++)
            {
                bombLights[i].color = Color.white;
            }

            yield return new WaitForSeconds(0.2f);

            timesToFlash += 0.4f;
        }
        //yield return new WaitForSeconds(detonationTime);
        Explode();
    }

    void Explode()
    {

        if (SpawnTargetScript.instance.GetTargetIsActive() && isTarget)
        {
            isTarget = false;
            SpawnTargetScript.instance.SetTargetIsActive(false);
        }

        gameObject.SetActive(false);
        GameObject tempExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
        Debug.Log("Exploded");

    }

    //public override void Die()
    //{
    //    if (SpawnTargetScript.instance.GetTargetIsActive() && isTarget)
    //    {
    //        //assignedObjective.Complete();
    //        isTarget = false;
    //        numCheese = 20;
    //        SpawnTargetScript.instance.SetTargetIsActive(false);
    //    }

    //    for (int i = 0; i < numCheese; i++)
    //    {
    //        Instantiate(CheeseStuff, transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0f), Quaternion.identity);
    //    }

    //    Explode();

    //}
}
