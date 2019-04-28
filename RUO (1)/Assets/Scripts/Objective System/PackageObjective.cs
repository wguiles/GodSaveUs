using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageObjective : Objective
{
    private Vector2 dropOffLocation;
    public GameObject package;
    private bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
        SpawnPackage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetIsCollected()
    {
        return isCollected;
    }

    protected void SetIsCollected(bool collected)
    {
        isCollected = collected;
    }

    //Sets the drop off location 
    public void SetDropOffLocation()
    {
        //Makes an array of the dropoff points
        dropOffLocation = GameObject.FindGameObjectWithTag("DropOffPoint").transform.position;

        //updates the goalLocation
        base.SetGoalLocation(dropOffLocation);
    }

    //Spawns in the package at a random package spawn
    public virtual void SpawnPackage()
    {
        //Makes an array of the package spawn points
        GameObject[] packageSpawns = GameObject.FindGameObjectsWithTag("PackageSpawns");

        //Randomly picks a spawn point
        int rnd = Random.Range(0, packageSpawns.Length);

        //Checks if the package spawner already has a package
        if(packageSpawns[rnd].GetComponent<PackageSpawner>().GetHasPackage())
        {
            //if it has a package choose a new one
            foreach(GameObject spawn in packageSpawns)
            {
                if(!spawn.GetComponent<PackageSpawner>().GetHasPackage())
                {
                    //Spawns the package
                    Instantiate(package, spawn.transform);
                    spawn.GetComponent<PackageSpawner>().SetHasPackage(true);
                    break;
                }
            }
        }
        else
        {
            //Spawns the package
            Instantiate(package, packageSpawns[rnd].transform);
            packageSpawns[rnd].GetComponent<PackageSpawner>().SetHasPackage(true);
        }
            
        //Updates the goalLocation
        base.SetGoalLocation(package.transform.position);
    }

    //Player picks up the package
    private void CollectPackage()
    {
        //Gives player UI icon for having the package


        //notes that it has been collected
        isCollected = true;

        //Removes the package from the world
        Destroy(package);

        //Updates the goalLocation after picking one
        SetDropOffLocation();
    }
}
