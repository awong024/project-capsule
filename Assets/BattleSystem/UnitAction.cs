using UnityEngine;
using System.Collections;

public class UnitAction
{
  public enum ActionType {
    AutoAttack,
    Ability1,
    Ability2
  }

  public BattleUnit unit;
  public ActionType type;

  public UnitAction(ActionType type) {
    this.type = type;
  }

  public int Damage {
    get
    {
      if (type == ActionType.AutoAttack) {
        return StatCalculator.AutoDamageFromStrength(unit.Strength);
      }
      return 0;
    }
  }
}
