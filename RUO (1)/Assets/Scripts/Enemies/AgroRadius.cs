using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgroRadius : MonoBehaviour
{
    private SphereCollider aggroRadius;

    //store the faction reputation points
    private FactionReputation factionScript;
    private int repPoints;

    // Use this for initialization
    void Start()
    {
        //factionScript = GameObject.Find("FactionReputation").GetComponent<FactionReputation>();
        //UpdateRepPoints();
        //aggroRadius = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateRepPoints();

        //if (gameObject.GetComponentInParent<EnforcerScript>().GetFactionType() == EnemyStats.FactionType.mouse)
        //{
        //    aggroRadius.radius = (float)(/*3 * */(100f - repPoints) / 100f);
        //}
        //else
        //{
        //    aggroRadius.radius = (float)((3 * repPoints / 100f));
        //}
    }

    //Updates the number of reputation points they have
    private void UpdateRepPoints()
    {
//        repPoints = factionScript.GetReputationPoints();
    }
}

