using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostageCrisis : PackageObjective
{
    private GameObject hostage;
    private GameObject bruiser;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Override packageSpawn to spawn the hostage
    //Spawns the hostage with bruiser patrol points around it
    public override void SpawnPackage()
    {
        //Makes an array of the package spawn points
        GameObject[] packageSpawns = GameObject.FindGameObjectsWithTag("PackageSpawns");

        //Randomly picks a spawn point
        int rnd = Random.Range(0, packageSpawns.Length);

        //Spawns the package
        Instantiate(hostage, packageSpawns[rnd].transform);

        //Updates the goalLocation
        base.SetGoalLocation(hostage.transform.position);
        StartCoroutine(GenerateEnemies(hostage.transform));
    }

    //Spawns bruisers in the area
    IEnumerator GenerateEnemies(Transform location)
    {
        Instantiate(bruiser, location);
        yield return new WaitForSeconds(0.5f);
        Instantiate(bruiser, location);
    }
}
