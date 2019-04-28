using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject MouseRunner;
    public GameObject MouseEnforcer;
    public GameObject RatRunner;
    public GameObject RatEnforcer;
    public GameObject Cheese;

    public Vector3[] CheesePositions;
    public GameObject[] CheeseObjects;

    private Transform MousePosition;
    private Transform RatPosition;

    private void Start()
    {
        CheeseObjects = GameObject.FindGameObjectsWithTag("Cheese");
        CheesePositions = new Vector3[GameObject.FindGameObjectsWithTag("Cheese").Length];

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Cheese").Length; i++)
        {
            CheesePositions[i] = GameObject.FindGameObjectsWithTag("Cheese")[i].transform.position;
        }

        MousePosition = GameObject.Find("Mice_Hideout").transform;
        RatPosition = GameObject.Find("Rats_Hideout").transform;

        StartCoroutine(SpawnCheese());
    }

    IEnumerator SpawnCheese()
    {
        while (true)
        {
            for (int i = 0; i < CheesePositions.Length; i++)
            {
                if (CheeseObjects[i] == null)
                {
                    yield return new WaitForSeconds(1.0f);
                    spawnCheese(CheesePositions[i]);
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void SpawnMouseRunner()
    {
        Instantiate(MouseRunner, MousePosition.transform.position, Quaternion.identity);
    }

    public void SpawnMouseEnforcer()
    {
        Instantiate(MouseEnforcer, MousePosition.transform.position, Quaternion.identity);
    }

    public void SpawnRatRunner()
    {
        Instantiate(RatRunner, RatPosition.transform.position, Quaternion.identity);
    }

    public void SpawnRatEnforcer()
    {
        Instantiate(RatEnforcer, RatPosition.transform.position, Quaternion.identity);
    }

    public void spawnCheese(Vector3 pos)
    {
        Instantiate(Cheese, pos, Quaternion.identity);
    }
}
