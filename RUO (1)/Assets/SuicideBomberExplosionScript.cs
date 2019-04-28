using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomberExplosionScript : MonoBehaviour
{
    public float LingerTime;

    private void Start()
    {
        SoundManager.instance.PlaySound("MineExplosion");
        StartCoroutine(Disappear());
    }
    public void OnTriggerEnter(Collider collision)
    {

        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

        if (player != null)
        {
            player.TakeDamage(1);
        }
            

    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(LingerTime);
        Destroy(gameObject);
    }
}
