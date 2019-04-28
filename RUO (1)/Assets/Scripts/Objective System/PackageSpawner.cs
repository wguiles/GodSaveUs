using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSpawner : MonoBehaviour
{
    [SerializeField] private bool hasPackage;

    // Start is called before the first frame update
    void Start()
    {
        hasPackage = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHasPackage(bool pack)
    {
        hasPackage = pack;
    }

    public bool GetHasPackage()
    {
        return hasPackage;
    }
}
