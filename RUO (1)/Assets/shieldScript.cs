using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldScript : MonoBehaviour
{
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shield());
    }

    IEnumerator Shield()
    {

        while(true)
        {
            yield return new WaitForSeconds(waitTime);

            for (int i = 0; i < 6; i++)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                yield return new WaitForSeconds(0.1f);
                GetComponent<SpriteRenderer>().color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;

            yield return new WaitForSeconds(waitTime);

            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;

            yield return new WaitForSeconds(waitTime);
        }
        //flash shield
        //disable shield
        //wait a couple of seconds 
        //enable shield
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
