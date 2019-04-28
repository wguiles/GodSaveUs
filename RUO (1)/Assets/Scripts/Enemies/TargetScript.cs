using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        //if (target != null)
        //{
        //    GetComponent<SpriteRenderer>().enabled = true;
        //    transform.position = target.position + offset;
        //}
        //else if (!target.GetComponent<EnemyStats>().GetIsTarget())
        //{
        //    GetComponent<SpriteRenderer>().enabled = false;
        //}

        if (target != null)
        {
            transform.position = target.position + offset;
        }

    }

    public void setTarget(Transform targetToSet)
    {
        target = targetToSet;
    }


}
