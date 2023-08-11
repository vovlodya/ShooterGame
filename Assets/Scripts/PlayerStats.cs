using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int health = 100;
    public bool isDead = false;
    private HUDManager hud;
    private MenuManager menu;
    private GameObject gameManager;
    private int kills;

    private void Start()
    {
        hud = GetComponent<HUDManager>();
        hud.UpdateHealth(health, maxHealth);
        menu = GetComponent<MenuManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        
    }

    public void Hit(int damage)
    { 
        health -= damage;
        if(health <=0 ) 
        {
            menu.SetActiveHud(false);
            kills = gameManager.GetComponent<GameManager>().enemiesKilled;
            if(PlayerPrefs.GetInt("Score")<kills)
            PlayerPrefs.SetInt("Score", kills);
            isDead = true;
        }
        hud.UpdateHealth(health, maxHealth);
    }

    public void Heal()
    {
        health = maxHealth;
        hud.UpdateHealth(health, maxHealth);
    }
   
}
