using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] private float flashTime = 0;
   [SerializeField] private GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        light = this.gameObject.transform.Find("Light").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //If the flash time is 0 it will start a countdown
        if (flashTime == 0)
        {
            FlashLights();
        }
    }

    //Method for breaking the lamp
    //To be called by explosions
    //NOTE: THIS IS SPECIFICALLY FOR THE STREET LAMP AND NOT THE BAR LIGHT
    public void BreakLights()
    {
        //play sound effect


        //turn off the light
        light.SetActive(false);

        // turn on broken glass
        this.gameObject.transform.Find("Shards").gameObject.SetActive(true);
    }


    //Method for the lights to flash
    public void FlashLights()
    {
        //Picks a number between 5 and 15 seconds
        flashTime = Random.Range(5f, 15f);

        //when it hits that point it flickers (on/off about 4 times)
        StartCoroutine("StartFlash");
    }

    private IEnumerator StartFlash()
    {
        Debug.Log("StartFlash start");

        yield return new WaitForSeconds(flashTime);

        //first flash
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        //second flash
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        //third flash
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        //fourth flash
        light.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        light.SetActive(true);

        Debug.Log("StartFlash end");

        //Resets the flash time
        flashTime = 0;
    }

}
