using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFigurineUnit : MonoBehaviour
{
  [SerializeField] Image unitSprite;

  private int currentHealth;

  public void Init(FigurineModel model) {
    unitSprite.sprite = model.Sprite;
  }
}
