using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Spawn(GameObject objToSpawn)
    {
          Instantiate(objToSpawn, transform.position, Quaternion.identity);
    }

    public void SpawnBunch(int numberOfEnemies, GameObject objToSpawn)
    {
        StartCoroutine(BunchSpawn(numberOfEnemies, objToSpawn));
    }

    private IEnumerator BunchSpawn(int numberOfEnemies, GameObject objToSpawn)
    {
        for (int i = 0; i < numberOfEnemies; i ++)
        {
            yield return new WaitForSeconds(0.3f);
            Spawn(objToSpawn);
        }
    }

    public bool GetIsVisible()
    {
        return _renderer.isVisible;
    }

    public float distanceToPlayer()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();

        if (player != null)
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        return 0.0f;
    }
}
