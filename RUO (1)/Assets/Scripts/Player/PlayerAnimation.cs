using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer thisRenderer;

    void Start()
    {
        thisRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(tailAnim());
    }

    IEnumerator tailAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            thisRenderer.flipX = !thisRenderer.flipX;
        }
    }

    void Update()
    {

    }
}
