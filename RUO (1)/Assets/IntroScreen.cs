﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{
    public GameObject startMenuEventSystem;
    private static bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlaySound("CrawlMusic");

        if (!gameStarted)
        {
            gameStarted = true;
            StartCoroutine(WaitToLoadStartScreen());
        }
        else if (SceneManager.GetActiveScene().name == "TownsSpaceScene") 
        {
            SkipIntro();
        }

        Time.timeScale = 1.0f;
    }

    IEnumerator WaitToLoadStartScreen()
    {
        yield return new WaitForSeconds(50.0f);
        SkipIntro();
    }

    private void SkipIntro()
    {
        startMenuEventSystem.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start Button"))
        {
            SkipIntro();
        }
    }

    private void OnDisable()
    {
        SoundManager.instance.StopSound("CrawlMusic");
        SoundManager.instance.PlaySound("TitleMusic");
    }
}
