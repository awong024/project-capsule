﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour {
  [SerializeField] Image barFill;
  [SerializeField] Text manaNumber;

  [SerializeField] Text gameTimer;

  [SerializeField] GameObject gameOverPopup;
  [SerializeField] Text gameOverText;

  private float mana = 5;
  private float timer = 59;

  private bool paused = false;

  public void GainMana() {
    mana += 0.1f;

    if (mana >= 10f) {
      mana = 10f;
    }

    barFill.fillAmount = mana / 10f;
    manaNumber.text = ((int)mana).ToString();
  }

  public bool PlayUnit(int manaCost) {
    if (manaCost <= (int)mana) {
      mana -= (float)manaCost;
      return true;
    }
    return false;
  }

  void Start() {
    StartCoroutine(Clock());
  }

  private IEnumerator Clock() {
    while(true) {
      gameTimer.text = "0:" + ((int)timer).ToString("D2");
      yield return new WaitForSeconds(1f);
      if (!paused) {
        timer = Mathf.Max(timer - 1, 0);
      }
    }
  }

  public bool IsTimerUp() {
    return timer <= 0;
  }

  public void DisplayGameOver(bool win) {
    gameOverPopup.SetActive(true);
    gameOverText.text = win ? "BOSS DEFEATED" : "TIME UP";
  }

  public void PauseTimer() {
    paused = true;
  }

  public void ResumeTimer() {
    paused = false;
  }
}
