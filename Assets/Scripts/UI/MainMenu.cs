using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   [SerializeField] private GameObject mainMenu;
   [SerializeField] private GameObject score;
   [SerializeField] private int kills = 0;
   [SerializeField] private Text scoreText;

   private void Start()
   {
      mainMenu.SetActive(true);
   }

   public void ActivateMainMenu(bool state)
   {
      mainMenu.SetActive(state);
      score.SetActive(!state);
      SetScore();
   }

   private void SetScore()
   {
      if (kills < PlayerPrefs.GetInt("Score"))
      {
         kills = PlayerPrefs.GetInt("Score");
      }
      scoreText.text = "You killed " + kills + " zombies!";
   }
   public void Play()
   {
      SceneManager.LoadScene(1);
   }

   public void Quit()
   {
      Application.Quit();
   }
}
