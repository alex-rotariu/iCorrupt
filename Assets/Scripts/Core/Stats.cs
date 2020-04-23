﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI moneyPerSecondText;

    private int money = 0;
    private int moneyPerSecond = 0;

    private float timePassedSinceLastSecondUpdate = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Add the time from the last update to the time passed and check if more than a second passed
        timePassedSinceLastSecondUpdate += Time.deltaTime;
        if (timePassedSinceLastSecondUpdate >= 1.0f)
        {
            //If more than a second passed then reset time passed and add moneyPerSecond to total money
            timePassedSinceLastSecondUpdate = 0.0f;

            money += moneyPerSecond;
        }


        moneyText.text = money.ToString();
        moneyPerSecondText.text = moneyPerSecond.ToString();
    }

    public void addMoney() {
        money++;
    }



    public void buyUpgrade(string costAndMPSIncrease)
    {
        int cost, moneyPerSecondIncrease;
        cost = int.Parse(costAndMPSIncrease.Substring(0, 10));
        moneyPerSecondIncrease = int.Parse(costAndMPSIncrease.Substring(13, 10));
        if(costAndMPSIncrease[11]!='0')
        {
            moneyPerSecondIncrease *= -1;
        }
        if (money>=cost&&moneyPerSecond+moneyPerSecondIncrease>=0)
        {
            money -= cost;
            moneyPerSecond += moneyPerSecondIncrease;
        }
    }
}