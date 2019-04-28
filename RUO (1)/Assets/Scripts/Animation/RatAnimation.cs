using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAnimation : MonoBehaviour
{
    private SpriteRenderer thisRenderer;
    public Sprite ratSprite1;
    public Sprite ratSprite2;

    void Start()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
        thisRenderer.sprite = ratSprite1;
        StartCoroutine(ratAnim());
    }

    IEnumerator ratAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (thisRenderer.sprite == ratSprite1)
            {
                thisRenderer.sprite = ratSprite2;
            }
            else
            {
                thisRenderer.sprite = ratSprite1;
            }
        }
    }
}
