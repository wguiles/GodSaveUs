using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRatEnforcer : EnemyStats
{
    public float followRange;

    public virtual void Start()
    {
        agent.speed += Random.Range(-3.0f, 3.0f);
    }

    public virtual void Update()
    {
        if (DistanceToPlayer() <= followRange)
        {
            FollowPlayer();
        }

        base.Update();
    }


}
