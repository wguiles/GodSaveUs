using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float moveSpeed = 7f;

    Rigidbody rb;
    public GameObject target;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, 0.67f);
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            col.GetComponent<PlayerStats>().TakeDamage(1);
            Destroy(gameObject);
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            
            Destroy(gameObject);
        }
        if (col.gameObject.name.Equals("Slash"))
        {
            Debug.Log("deflect");
            Destroy(gameObject);
        }
    }
}
