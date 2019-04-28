using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{
    public Vector3 offset;
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
