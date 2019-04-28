using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    [SerializeField] private CheeseSizes cheeseSize;

    private const int WEDGE_POINTS = 1;
    private const int HALF_POINTS = 5;
    private const int WHEEL_POINTS = 10;

    private int cheesePoints;

    public enum CheeseSizes { wedge, half, wheel};
    
    // Start is called before the first frame update
    void Start()
    {
        if(cheeseSize == CheeseSizes.wedge)
        {
            cheesePoints = WEDGE_POINTS;
        }
        else if(cheeseSize == CheeseSizes.half)
        {
            cheesePoints = HALF_POINTS;
        }
        else if(cheeseSize == CheeseSizes.wheel)
        {
            cheesePoints = WHEEL_POINTS;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerStats>() != null)
        {
            if (Vector3.Distance(transform.position, FindObjectOfType<PlayerStats>().transform.position) <= 6.0f)
            {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = (FindObjectOfType<PlayerStats>().transform.position - transform.position);

        transform.Translate(directionToPlayer * Time.deltaTime * 3f);
    }

    public int GetCheesePoints()
    {
        return cheesePoints;
    }
}
