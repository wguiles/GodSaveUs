using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Spawner[] spawners;

    public GameObject ratEnforcerBasic;
    public GameObject ratWithShield;
    public GameObject suicideBomberRat;

    public GameObject MouseBruiser;
    public GameObject MouseBruiserWithShield;
    public GameObject BlueThrower;

    [HideInInspector]
    public int RatCount = 0;

    [HideInInspector]
    public int MouseCount = 0;

    [Header("Random Spawn Settings")]

    public int RatLimit = 25;

    [Range(0f, 1.0f)]
    public float basicRatPercentage;

    [Range(0f, 1.0f)]
    public float shieldRatPercentage;

    [Range(0f, 1.0f)]
    public float suicideBomberPercentage;

    public float ratSpawnRate;


    public int MouseLimit = 20;

    [Range(0f, 1.0f)]
    public float basicMousePercentage;

    [Range(0f, 1.0f)]
    public float shieldMousePercentage;

    [Range(0f, 1.0f)]
    public float flameThrowerPercentage;

    public float mouseSpawnRate;
   


    public EnemyStats.FactionType enemiesToSpawn;


    public float spawnRate;
    int numberOfEnemies;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;

        spawners = new Spawner[GameObject.FindGameObjectsWithTag("Spawner").Length];

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Spawner").Length; i++)
        {
            spawners[i] = GameObject.FindGameObjectsWithTag("Spawner")[i].GetComponent<Spawner>();
        }
    }

    void Start()
    {


    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void SpawnRats()
    {
        enemiesToSpawn = EnemyStats.FactionType.rat;
    }

    public void SpawnMice()
    {
        enemiesToSpawn = EnemyStats.FactionType.mouse;
    }

    IEnumerator SpawnEnemies()
    {
        while(true)
        {

            if (enemiesToSpawn == EnemyStats.FactionType.rat)
            {
                yield return new WaitForSeconds(ratSpawnRate);
            }
            else
            {
                yield return new WaitForSeconds(mouseSpawnRate);
            }

            Spawner spawnpoint = spawners[Random.Range(0, spawners.Length)];

            //while (spawnpoint.GetIsVisible())
            //{
            //    yield return new WaitForSeconds(0.1f);
            //    spawnpoint = spawners[Random.Range(0, spawners.Length)];
            //}

            //check if it's rats or mice

            float RandomNum = Random.Range(0, 1);

            if (enemiesToSpawn == EnemyStats.FactionType.rat && RatCount < RatLimit)
            {
               
                int secondRandomNum;
                //Spawn Rats over time

                //20% suicide bombers, 30% shielded Rats, 50%

                if (RandomNum < shieldRatPercentage)
                {
                    secondRandomNum = Random.Range(1, 2);
                    spawnpoint.SpawnBunch(secondRandomNum, ratWithShield);
                    RatCount += secondRandomNum;

                    //closestSpawner().SpawnBunch(Random.Range(1, 2), ratWithShield);
                }

                if (RandomNum < basicRatPercentage)
                {
                    secondRandomNum = Random.Range(1, 2);
                    spawnpoint.SpawnBunch(secondRandomNum, ratEnforcerBasic);
                    RatCount += secondRandomNum;

                    //closestSpawner().SpawnBunch(Random.Range(3, 6), ratEnforcerBasic);
                }

                if (RandomNum < suicideBomberPercentage)
                {
                    secondRandomNum = Random.Range(1, 3);
                    spawnpoint.SpawnBunch(secondRandomNum, suicideBomberRat);
                    RatCount += secondRandomNum;

                    //closestSpawner().SpawnBunch(Random.Range(1, 3), suicideBomberRat);
                }
            }
            else if (MouseCount < MouseLimit && enemiesToSpawn == EnemyStats.FactionType.mouse)
            {
                MouseCount++;
                //spawn mice over time

                //33% shielded Mice, 66% normal mice

                if (RandomNum >= basicMousePercentage)
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 3), MouseBruiserWithShield);
                    //closestSpawner().SpawnBunch(Random.Range(1, 3), MouseBruiserWithShield);
                }

                if (RandomNum >= shieldRatPercentage)
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 3), MouseBruiser);
                    //closestSpawner().SpawnBunch(Random.Range(1, 3), MouseBruiser);
                }
            }
        }
    }

    public Vector3 RandomSpawnerPosition()
    {
        return spawners[Random.Range(0, spawners.Length)].transform.position;
    }

    public void SpawnTargetEnemy()
    {
        Spawner spawnpoint = spawners[Random.Range(0, spawners.Length)];

        while (spawnpoint.GetIsVisible())
        {
            spawnpoint = spawners[Random.Range(0, spawners.Length)];
        }

        Vector3 positionToSpawn = spawnpoint.transform.position;

        int RandomNum = Random.Range(0, 10);

        if (enemiesToSpawn == EnemyStats.FactionType.rat)
        {
            //Spawn Rats over time

            //20% suicide bombers, 30% shielded Rats, 50%

            if (RandomNum <= 1)
            {
                SpawnTargetScript.instance.SpawnTarget(ratWithShield, positionToSpawn);
            }
            else if (RandomNum <= 6)
            {
                SpawnTargetScript.instance.SpawnTarget(ratEnforcerBasic, positionToSpawn);
            }
            else
            {
                SpawnTargetScript.instance.SpawnTarget(suicideBomberRat, positionToSpawn);
            }
        }
        else
        {
            //spawn mice over time

            //30% shielded Mice, 50% normal mice, 20% Blue Thowers

            if (RandomNum <= 2)
            {
                SpawnTargetScript.instance.SpawnTarget(MouseBruiserWithShield, positionToSpawn);
            }
            else if (RandomNum <= 5)
            {
                SpawnTargetScript.instance.SpawnTarget(MouseBruiser, positionToSpawn);
            }
            else
            {
                SpawnTargetScript.instance.SpawnTarget(BlueThrower, positionToSpawn);
            }
        }
    }



    Spawner closestSpawner()
    {
        float closestSpawnerDistance = float.MaxValue;
        Spawner closest = spawners[0];

        foreach (Spawner s in spawners)
        {
            if (!s.GetIsVisible() && s.distanceToPlayer() < closestSpawnerDistance)
            {
                closestSpawnerDistance = s.distanceToPlayer();
                closest = s;
            }
        }

        return closest;
    }
}
