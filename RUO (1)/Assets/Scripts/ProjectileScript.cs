using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;


    [SerializeField]
    private GameObject explosion;


    [SerializeField]
    private float detonationTime;

    private bool triggerInUse;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, projectileSpeed * Time.deltaTime, 0f));

        if (Input.GetAxis("Right Trigger") <= 0.0f && !triggerInUse)
        {
            triggerInUse = true;
            StartCoroutine(Detonate());
        }

        
    }

    public void SetTransformUp(Vector3 vecToSet)
    {
        transform.up = vecToSet;
    }

    public void StartTimer()
    {
        StartCoroutine(Detonate());
    }

    public IEnumerator Detonate()
    {
        
        yield return new WaitForSeconds(detonationTime);
        GameObject Temp = Instantiate(explosion, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.1f);

        Destroy(Temp);
        Destroy(gameObject);
    }


}
