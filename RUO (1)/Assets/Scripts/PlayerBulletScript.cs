using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float despawnTime;
    [SerializeField] private float projectileSpeed;
    private Vector2 targetLocation;

    void Start()
    {
        targetLocation = (transform.position - GameObject.FindGameObjectWithTag("Player").transform.position) * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        despawnTime -= Time.deltaTime;
        transform.Translate(new Vector3(0f, projectileSpeed * Time.deltaTime, 0f));
        if(despawnTime <= 0)
        {
            //Debug.Log("it gets here");
            Destroy(gameObject);
        }
    }

    public void SetTransformUp(Vector3 vecToSet)
    {
        transform.up = vecToSet;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Wall") || other.tag.Equals("RatShield"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet collided with: " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.tag.Equals("RatShield"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Curve()
    {
        //Shoots ray (RaycastHit2D or sphere cast)
        //if hits something it curves it towards that enemy
        
    }


    //private GameObject closestEnemy()
    //{
    //    GameObject[] playerPositions = GameObject.FindObjectsWithTag("Enforcers");

    //    Vector3 shortestDistance = new Vector3(float.MAX, float.MAX, float.MAX);
    //    GameObject closestEnemy;

    //    foreach(GameObject g in playerPositions)
    //    {
    //        if (Vector3.Distance(g.transform.position, targetLocation) < shortestDistance)
    //        {
    //            shortestDistance = Vector3.Distance(g.transform.position, targetLocation);
    //            closestEnemy = g;
    //        }

    //    }

    //    return closestEnemy;
    //}
}
