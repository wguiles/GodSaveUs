using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        SoundManager.instance.PlaySound("MineExplosion");
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void OnTriggerEnter(Collider collision)
    {


        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

        if (enemy != null)
        {
            enemy.TakeDamage(playerController.GetMineDamage());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {

    }

}
