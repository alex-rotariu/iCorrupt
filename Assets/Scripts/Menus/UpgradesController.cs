﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public class Upgrade
{
    private int id;
    private string name;
    private ulong price;
    private int type;   // 0 is MPS; 1 is MPC
    private ulong value;
    private int politician;

    public Upgrade(int id, string name, ulong price, int type, ulong value, int politician)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.type = type;
        this.value = value;
        this.politician = politician;
    }

    public int getId()
    {
        return this.id;
    }

    public string getName()
    {
        return this.name;
    }

    public ulong getPrice()
    {
        return this.price;
    }

    public int getType()
    {
        return this.type;
    }

    public ulong getValue()
    {
        return this.value;
    }
    public int getPolitician()
    {
        return this.politician;
    }
}

public class UpgradesController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Upgrade1Name;
    [SerializeField] TextMeshProUGUI Upgrade1Effect;
    [SerializeField] TextMeshProUGUI Upgrade1Price;
    [SerializeField] TextMeshProUGUI Upgrade2Name;
    [SerializeField] TextMeshProUGUI Upgrade2Effect;
    [SerializeField] TextMeshProUGUI Upgrade2Price;
    [SerializeField] TextMeshProUGUI Upgrade3Name;
    [SerializeField] TextMeshProUGUI Upgrade3Effect;
    [SerializeField] TextMeshProUGUI Upgrade3Price;
    [SerializeField] TextMeshProUGUI Upgrade4Name;
    [SerializeField] TextMeshProUGUI Upgrade4Effect;
    [SerializeField] TextMeshProUGUI Upgrade4Price;
    [SerializeField] TextMeshProUGUI Upgrade5Name;
    [SerializeField] TextMeshProUGUI Upgrade5Effect;
    [SerializeField] TextMeshProUGUI Upgrade5Price;
    [SerializeField] TextMeshProUGUI Upgrade6Name;
    [SerializeField] TextMeshProUGUI Upgrade6Effect;
    [SerializeField] TextMeshProUGUI Upgrade6Price;
    [SerializeField] TextMeshProUGUI Upgrade7Name;
    [SerializeField] TextMeshProUGUI Upgrade7Effect;
    [SerializeField] TextMeshProUGUI Upgrade7Price;
    [SerializeField] Stats stats;

    List<Upgrade> UpgradesList = new List<Upgrade>();


    // Start is called before the first frame update
    void Start()
    {
        UpdateUpgrades();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] UpdateUpgrades()
    {
        TextAsset Up = Resources.Load<TextAsset>("Upgrades");
        string UpData = Up.text.Replace("\r", "");
        string[] Upgrades = UpData.Split(new char[] { '\n' });
        UpgradesList.Clear();
        for(int i=1;i<Upgrades.Length-1;i++)
        {
            string[] row = Upgrades[i].Split(new char[] { ',' });
            Upgrade u = new Upgrade(int.Parse(row[0]),row[1], ulong.Parse(row[2]),int.Parse(row[3]), ulong.Parse(row[4]),int.Parse(row[5]));
            if ((stats.getPlayer().upgradeBought[u.getId()] == false ) && (u.getPolitician() == ScenesData.GetPolitician() || u.getPolitician() == 99))
            {
                UpgradesList.Add(u);
            }
        }

        if (UpgradesList.Count >= 0)
        {
            upgradeSetText(Upgrade1Name, Upgrade1Effect, Upgrade1Price, 0);
        }
        if (UpgradesList.Count >= 1)
        {
            upgradeSetText(Upgrade2Name, Upgrade2Effect, Upgrade2Price, 1);
        }
        if (UpgradesList.Count >= 2)
        {
            upgradeSetText(Upgrade3Name, Upgrade3Effect, Upgrade3Price, 2);
        }
        if (UpgradesList.Count >= 3)
        {
            upgradeSetText(Upgrade4Name, Upgrade4Effect, Upgrade4Price, 3);
        }
        if (UpgradesList.Count >= 4)
        {
            upgradeSetText(Upgrade5Name, Upgrade5Effect, Upgrade5Price, 4);
        }
        if (UpgradesList.Count >= 5)
        {
            upgradeSetText(Upgrade6Name, Upgrade6Effect, Upgrade6Price, 5);
        }
        if (UpgradesList.Count >= 6)
        {
            upgradeSetText(Upgrade7Name, Upgrade7Effect, Upgrade7Price, 6);
        }
        return Upgrades;
    }

    public void upgradeSetText(TextMeshProUGUI Name, TextMeshProUGUI Effect, TextMeshProUGUI Price, int UpgradeIndex)
    {
        Name.text = UpgradesList[UpgradeIndex].getName();
        Price.text = "Price: " + UpgradesList[UpgradeIndex].getPrice().ToString();
        switch (UpgradesList[UpgradeIndex].getType())
        {
            case 0:
            case 4:
                {
                    Effect.text = "Effect: +" + UpgradesList[UpgradeIndex].getValue().ToString() + " Money/Sec";
                }break;
            case 1:
            case 5:
                {
                    Effect.text = "Effect: +" + UpgradesList[UpgradeIndex].getValue().ToString() + " Money/Click";
                }break;
            case 2:
            case 6:
                {
                    Effect.text = "Effect: +" + UpgradesList[UpgradeIndex].getValue().ToString() + "% Money/Sec";
                }break;
            case 3:
            case 7:
                {
                    Effect.text = "Effect: +" + UpgradesList[UpgradeIndex].getValue().ToString() + "% Money/Click";
                }break;
        }
    }

    public void buyUpgrade(int index)
    {
        string[] Upgrades = UpdateUpgrades();
        Player player = stats.getPlayer();
        for (int i = 1; i < Upgrades.Length-1; i++)
        {
            string[] row = Upgrades[i].Split(new char[] { ',' });
            if(UpgradesList[index].getId()==int.Parse(row[0]))
            {
                if (stats.getMoney() >= UpgradesList[index].getPrice())
                {
                    if (UpgradesList[index].getType() < 4)
                    {
                        player.upgradeBought[UpgradesList[index].getId()] = true;
                        stats.setPlayer(player);
                    }
                    stats.removeMoney(UpgradesList[index].getPrice());
                    if (UpgradesList[index].getType() % 4 == 0)
                    {
                        stats.addMoneyPerSecond(UpgradesList[index].getValue());
                    }
                    else if (UpgradesList[index].getType() % 4 == 1)
                    {
                        stats.addMoneyPerClick(UpgradesList[index].getValue());
                    }
                    else if (UpgradesList[index].getType() % 4 == 2)
                    {
                        stats.addPercentMoneyPerSecond(UpgradesList[index].getValue());
                    }
                    else if (UpgradesList[index].getType() % 4 == 3)
                    {
                        stats.addPercentMoneyPerClick(UpgradesList[index].getValue());
                    }
                }
            }
        }
        UpdateUpgrades();
    }
}
