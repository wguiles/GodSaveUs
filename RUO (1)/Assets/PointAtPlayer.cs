using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    public bool iThinkGarretIsAlrightIGuess;

    // Update is called once per frame
    void Update()
    {
        if (!iThinkGarretIsAlrightIGuess)
        {
            transform.up = (GameObject.Find("Player").transform.position - transform.position);
            transform.up = new Vector3(transform.up.x, transform.up.y, 0f);
        }
    }
}
