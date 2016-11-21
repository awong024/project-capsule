using UnityEngine;
using System.Collections;

public class BattleUnitController : MonoBehaviour
{
  private BattleUnit battleUnit;
  private BattleUnitView battleUnitView;

  public BattleUnit BattleUnit { get { return battleUnit; } }

  void Awake() {
    battleUnitView = GetComponent<BattleUnitView>(); 
  }

  public void Init(FigurineModel model) {
    battleUnit = new BattleUnit();
    battleUnit.Init(model);

    battleUnitView.Render(battleUnit);
  }

  public UnitAction ProcessFrame() {
    battleUnit.ProcessFrame();
    battleUnitView.UpdateView(battleUnit);

    UnitAction action = battleUnit.ReadiedAction();
    if (action != null) {
      return action;
    }

    return null;
  }

  public void ReceiveAction(UnitAction action) {
    if (action.type == UnitAction.ActionType.AutoAttack) {
      battleUnit.Damage(action.Damage);
      AnimateFX(ActionFX.Hit);
    }
    else if (action.type == UnitAction.ActionType.Ability) {
      if (action.unitAbility.AbilityEffect == UnitAbility.EffectType.Damage) {
        battleUnit.Damage(action.Damage);
        AnimateFX(ActionFX.Hit);
      } else if (action.unitAbility.AbilityEffect == UnitAbility.EffectType.Healing) {
        battleUnit.Heal(action.Damage);
        AnimateFX(ActionFX.Heal);
      }
    }
    battleUnitView.UpdateView(battleUnit);
  }

  public void UseAbility(int num) {
    battleUnit.UseAbility(num);
  }

  public void AnimateFX(ActionFX fx) {
    battleUnitView.AnimateFX(fx);
  }
}
