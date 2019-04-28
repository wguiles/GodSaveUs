using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    private PlayerController playerController;

    private bool isMoving = false;

    void Start()
    {
        playerController = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.GetIsDashing())
        {
            if (Mathf.Abs(Input.GetAxis("AttackX")) >= 0.2f || Mathf.Abs(Input.GetAxis("AttackY")) >= 0.2f)
            {
                if (!isMoving)
                {
                    GetComponent<Animator>().SetTrigger("Moving");
                    isMoving = true;
                }


                transform.up = new Vector3(Input.GetAxis("AttackX"), Input.GetAxis("AttackY"));
            }
            else
            {
                if (isMoving)
                {
                    isMoving = false;
                    GetComponent<Animator>().SetTrigger("Idle");
                }
            }

            if (Mathf.Abs(Input.GetAxis("RightStickX")) >= 0.2f || Mathf.Abs(Input.GetAxis("RightStickY")) >= 0.2f)
            {
                transform.up = new Vector3(Input.GetAxis("RightStickX"), Input.GetAxis("RightStickY"));
            }
        }
    }
}
