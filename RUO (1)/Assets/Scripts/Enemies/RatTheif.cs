using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTheif : RunnerScript
{
    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private float followDistance;

    private bool isAttacking = false;

    private void Start()
    {
        initialHealth = health;
        transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");
    }


    private void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
       
    }

    public override void Update()
    {

        base.Update();

        if (DistanceToPlayer() <= attackDistance && !isAttacking)
        {
            transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Idle");
            agent.isStopped = true;
            Attack();
        }
        else if (DistanceToPlayer() <= followDistance && !isAttacking)
        {
            transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Moving");
            FollowPlayer();
            agent.isStopped = false;
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<Animator>().SetTrigger("Idle");
            UnfollowPlayer();
        }
    }
}
