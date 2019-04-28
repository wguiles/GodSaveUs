using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideBuilding : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .3f);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
        
    }
}
