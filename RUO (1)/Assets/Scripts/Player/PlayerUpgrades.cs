﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    //put the percent in as a decimal value
    private float percentChange = 1.25f;
    

    private PlayerStats playerStats;
    private PlayerController playerController;

    public static bool Rat1   = false;
    public static bool Mouse1 = false;

    public static bool Rat2   = false;
    public static bool Mouse2 = false;

    public static bool Rat3   = false;
    public static bool Mouse3 = false;

    public static PlayerUpgrades thisPlayerUpgrades;


    private void Awake()
    {
        if (thisPlayerUpgrades == null)
            thisPlayerUpgrades = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // -------------------- Upgrades for first level --------------------

    //Player gains a dash damange boost
    public void RatUpgrade1()
    {
        //Upgrade specific stuff
        //playerController.SetSlashDamage((int)(playerController.GetSlashDamage() * percentChange));
        Rat1 = true;
        //PlayerController.speed = 20;
        //Player1SpeedUpgrade = true;
        //Update UI (if applicable)
        Debug.Log("RatUpgrade1  ----- ACTIVATED -----");
    }

    //Player gains a speed boost
    public void MiceUpgrade1()
    {
        //Upgrade specific stuff
        //playerController.SetSpeed(playerController.GetSpeed() * percentChange);
        Mouse1 = true;
        //Update UI (if applicable)
        Debug.Log("MiceUpgrade1 ----- ACTIVATED -----");
    }

    // -------------------- Upgrades for second level --------------------

    //Player gains a health increase
    public void RatUpgrade2()
    {
        //Gets the max health and multiplies it by the percent change
        //Then casts the result to an int and sets the new max health to that value
        //playerStats.SetMaxHealth((int)(playerStats.GetMaxHealth() * percentChange));
        Rat2 = true;

        //Update UI (if applicable)
        Debug.Log("RatUpgrade2 ----- ACTIVATED -----");
    }

    //Player gains a decrease in the dash recharge
    public void MiceUpgrade2()
    {
        //Upgrade specific stuff
        //PlayerController.dashRechargeTime *= percentChange;
        Mouse2 = true;
        //Update UI (if applicable)
        Debug.Log("MiceUpgrade2 ----- ACTIVATED -----");
    }

    // -------------------- Upgrades for third level --------------------

    public void RatUpgrade3()
    {
        //Upgrade specific stuff
        Rat3 = true;
        PlayerController.hasBombUpgrade = true;
        //increase bomb radius

        //Update UI (if applicable)
        Debug.Log("RatUpgrade3 ----- ACTIVATED -----");
    }

    public void MiceUpgrade3()
    {
        //Upgrade specific stuff

        //faster projectile
        Mouse3 = true;
        //PlayerController.fireRate *= 2;

        //Update UI (if applicable)
        Debug.Log("MiceUpgrade3 ----- ACTIVATED -----");
    }

    // -------------------- Upgrades for fourth level --------------------

    public void RatUpgrade4()
    {
        //Upgrade specific stuff


        //Update UI (if applicable)
        Debug.Log("RatUpgrade4 ----- ACTIVATED -----");
    }

    public void MiceUpgrade4()
    {
        //Upgrade specific stuff


        //Update UI (if applicable)
        Debug.Log("MiceUpgrade4 ----- ACTIVATED -----");
    }
}
