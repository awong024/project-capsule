using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitFrame : MonoBehaviour {

  [SerializeField] Image portrait;
  [SerializeField] Image healthBar;
  [SerializeField] Image ability1_image;
  [SerializeField] Image ability2_image;

  private BattleUnitController unit;

  public void Init(BattleUnitController unitController) {
    this.unit = unitController;
    portrait.sprite = unitController.BattleUnit.FigurineModel.Sprite;
  }

  public void Render() {
    healthBar.fillAmount = (float)unit.BattleUnit.CurrentHealth / (float)unit.BattleUnit.MaxHealth;
  }

  public void Ability1_Clicked() {

  }

  public void Ability2_Clicked() {

  }
}
