using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUnitView : MonoBehaviour
{
  [SerializeField] Image image;
  [SerializeField] Image healthBar;
  [SerializeField] Image attackBar;
  [SerializeField] Animator fxAnimator;

  private BattleUnit battleUnit;

  public void Render(BattleUnit battleUnit) {
    this.battleUnit = battleUnit;
    image.sprite = battleUnit.FigurineModel.Sprite;
  }

  public void UpdateView(BattleUnit unit) { 
    healthBar.fillAmount = (float)unit.CurrentHealth / (float)unit.MaxHealth;
    attackBar.fillAmount = 1f - ((float)unit.AutoAttackTimer / (float)unit.MaxAttackTimer);
  }

  public void AnimateFX(UnitAction.ActionFX fx) {
    fxAnimator.SetTrigger(fx.ToString());
  }
}
