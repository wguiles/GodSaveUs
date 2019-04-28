using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class childAttackerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool stoleCheese;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !stoleCheese)
        {
            //Debug.Log("I'm gonna debug this");

            stoleCheese = true;

            RatTheif rTheif = transform.parent.GetComponent<RatTheif>();

            if (rTheif != null)
            {
                rTheif.StealCheese(5, 0);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            stoleCheese = true;
        }
    }


}
