using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitFrame : MonoBehaviour {

  [SerializeField] Image portrait;
  [SerializeField] Image healthBar;
  [SerializeField] Image energyBar;
  [SerializeField] Image ability1_image;
  [SerializeField] Image ability2_image;

  private BattleUnitController unit;

  public void Init(BattleUnitController unitController) {
    this.unit = unitController;
    portrait.sprite = unitController.BattleUnit.FigurineModel.Sprite;

    if (unitController.BattleUnit.FigurineModel.Ability1 != null) {
      ability1_image.sprite = unitController.BattleUnit.FigurineModel.Ability1.AbilityIcon;
    } else if (unitController.BattleUnit.FigurineModel.Ability2 != null) {
      ability2_image.sprite = unitController.BattleUnit.FigurineModel.Ability2.AbilityIcon;
    }
  }

  public void Render() {
    healthBar.fillAmount = (float)unit.BattleUnit.CurrentHealth / (float)unit.BattleUnit.MaxHealth;
    energyBar.fillAmount = (float)unit.BattleUnit.Energy / 100f;
  }

  public void Ability1_Clicked() {
    unit.UseAbility(1);
  }

  public void Ability2_Clicked() {
    unit.UseAbility(2);
  }
}
