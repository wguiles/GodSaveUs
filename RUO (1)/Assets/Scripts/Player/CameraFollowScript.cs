using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    private Vector3 initalOffset;

    public Transform follow;
    public float smoothSpeed = 10f;
    public Vector3 offSet;

    private void Start()
    {
        initalOffset = offSet;

    }

    private void Update()
    {
        if (Input.GetAxis("Left_Trigger") >= 1.0f || Input.GetAxis("Left_Trigger Windows") >= 1.0f)
        {
            offSet = new Vector3(0f, 0f, -45f);

            if (transform.position.z <= -44f)
            {
                Time.timeScale = 0.1f;
            }

            GameObject.FindObjectOfType<PlayerController>().SetCanMove(false);
        }
        else if ((Input.GetAxis("Left_Trigger") <= 0.0f || Input.GetAxis("Left_Trigger Windows") <= 0.0f) && !MenuManager.isPaused)
        {
            Time.timeScale = 1.0f;
 
            offSet = initalOffset;
            GameObject.FindObjectOfType<PlayerController>().SetCanMove(true);
        }
    }

    private void LateUpdate()
    {
        if (follow != null)
        {
            Vector3 desiredPosition = follow.position + offSet;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}
