using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFigurineView : MonoBehaviour {
  [SerializeField] Image unitSprite;
  [SerializeField] Image healthBar;
	
  public void Render(FigurineModel model) {
    unitSprite.sprite = model.Sprite;
  }

  public void UpdateHealthBar(float fillValue) {
    healthBar.fillAmount = fillValue;
  }
}
