using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(destroySelf());
    }

    private IEnumerator destroySelf()
    {
        SoundManager.instance.PlaySound("EnemyDeath");
        yield return new WaitForSeconds(0.35f);
        Destroy(this.gameObject);
    }
}
