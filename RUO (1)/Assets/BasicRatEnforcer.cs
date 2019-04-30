using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRatEnforcer : EnemyStats
{
    public bool tethered;
    private Vector3 tetherPos;

    public float followRange;

    public virtual void Start()
    {
        if (Random.Range(0, 10) < 4)
        {
            tethered = true;
        }

        agent.speed += Random.Range(-3.0f, 3.0f);

        if (tethered)
        {
            tetherPos = SpawnManager.instance.RandomSpawnerPosition();
            followRange = 20f;
        }
            
    }
    

    public virtual void Update()
    {
        if (DistanceToPlayer() <= followRange)
        {
            FollowPlayer();
        }
        else if (tethered)
        {
            agent.destination = tetherPos;
        }

        base.Update();
    }

    public override void TakeDamage(int amount)
    {
        followRange = 1000f;

        base.TakeDamage(amount);
    }


}
