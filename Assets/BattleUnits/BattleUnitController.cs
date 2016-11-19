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
    battleUnitView.UpdateView(battleUnit);
  }

  public void AnimateFX(ActionFX fx) {
    battleUnitView.AnimateFX(fx);
  }
}
