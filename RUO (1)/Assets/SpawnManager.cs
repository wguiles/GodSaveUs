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

    public EnemyStats.FactionType enemiesToSpawn;


    public float spawnRate;
    int numberOfEnemies;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        spawners = new Spawner[GameObject.FindGameObjectsWithTag("Spawner").Length];

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Spawner").Length; i++)
        {
            spawners[i] = GameObject.FindGameObjectsWithTag("Spawner")[i].GetComponent<Spawner>();
        }
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

            yield return new WaitForSeconds(spawnRate);

            Spawner spawnpoint = spawners[Random.Range(0, spawners.Length)];

            while (spawnpoint.GetIsVisible())
            {
                yield return new WaitForSeconds(0.1f);
                spawnpoint = spawners[Random.Range(0, spawners.Length)];
            }

            //check if it's rats or mice

            int RandomNum = Random.Range(0, 10);

            if (enemiesToSpawn == EnemyStats.FactionType.rat)
            {
                //Spawn Rats over time

                //20% suicide bombers, 30% shielded Rats, 50%

                if (RandomNum <= 1)
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 2), ratWithShield);
                }
                else if (RandomNum <= 6)
                {
                    spawnpoint.SpawnBunch(Random.Range(3, 6), ratEnforcerBasic);
                }
                else
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 3), suicideBomberRat);
                }
            }
            else
            {
                //spawn mice over time

                //33% shielded Mice, 66% normal mice

                if (RandomNum <= 2)
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 3), MouseBruiserWithShield);
                }
                else
                {
                    spawnpoint.SpawnBunch(Random.Range(1, 3), MouseBruiser);
                }
            }
        }
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
}
