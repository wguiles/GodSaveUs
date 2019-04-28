using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTargetScript : MonoBehaviour
{
    private EnemyStats CurrentTarget;

    public static SpawnTargetScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private  bool targetIsActive = false;

    [SerializeField]
    private GameObject targetMarker;

    public void setCurrentTarget(EnemyStats enemy)
    {
        CurrentTarget = enemy;
        enemy.SetIsTarget(true);
        targetIsActive = true;
    }
    

    public EnemyStats GetCurrentTarget()
    {
        return CurrentTarget;
    }

    public void SetTargetIsActive(bool boolToSet)
    {
        targetIsActive = boolToSet;
    }

    public bool GetTargetIsActive()
    {
        return targetIsActive;
    }

    public void SpawnTarget(GameObject targetToSpawn, Vector3 spawnLocation)
    {
        if (!targetIsActive)
        {
            GameObject spawnedObj = Instantiate(targetToSpawn, spawnLocation, Quaternion.identity);
            setCurrentTarget(spawnedObj.GetComponent<EnemyStats>());
            targetMarker.GetComponent<TargetScript>().setTarget(spawnedObj.transform);

            Debug.Log("Spawning Target");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!targetIsActive)
        {
            FindObjectOfType<SpawnManager>().SpawnTargetEnemy();
        }
    }
}
