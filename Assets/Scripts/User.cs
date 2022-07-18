using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class User : MonoBehaviour
{
    public static User instance;
    public GameObject turretPre;
    public GameObject buildTurret;
    public GameObject gameOverUI;
    public TextMeshProUGUI moneytxt;
    public TextMeshProUGUI hptxt;
    public int money;
    public int turretCost;
    public int health;
    public bool lose;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        gameOverUI.SetActive(false);
        buildTurret = turretPre;
        UpdateMoney();
        UpdateHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getTurret()
    {
        if (money >= turretCost)
        {
            money -= turretCost;
            UpdateMoney();
            return buildTurret;
        } else
        {
            return null;
        }
        
    }

    public void LoseHP()
    {
        health -= 1;
        UpdateHP();
        if (health == 0)
        {
            lose = true;
            gameOverUI.SetActive(true);
        }
    }

    private void UpdateMoney()
    {
        moneytxt.text = string.Format("${0}", money);
    }

    private void UpdateHP()
    {
        hptxt.text = string.Format("♥: {0}", health);
    }

    public bool GameOver()
    {
        return lose;
    }

    public bool spend(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UpdateMoney();
            return true;
        }
        return false;
    }

    public void GainMoney(int amount)
    {
        money += amount;
        UpdateMoney();
    }
}
