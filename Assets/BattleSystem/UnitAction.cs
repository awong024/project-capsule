using UnityEngine;
using System.Collections;

public class UnitAction
{
  public enum ActionType {
    AutoAttack,
    Ability
  }

  public BattleUnit unit;
  public ActionType type;
  public UnitAbility unitAbility;

  public UnitAction(ActionType type) {
    this.type = type;
  }

  public UnitAction(UnitAbility ability) {
    this.type = ActionType.Ability;
    this.unitAbility = ability;
  }

  public int Damage {
    get
    {
      if (type == ActionType.AutoAttack) {
        return StatCalculator.AutoDamageFromStrength(unit.Strength);
      } else if (type == ActionType.Ability) {
        return StatCalculator.AbilityDamageFromIntellect(unit.Intellect);
      }
      return 0;
    }
  }
}
