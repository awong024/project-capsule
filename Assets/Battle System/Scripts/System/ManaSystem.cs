using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour {
  [SerializeField] Image barFill;
  [SerializeField] Text manaNumber;

  private float mana = 0;

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
}
