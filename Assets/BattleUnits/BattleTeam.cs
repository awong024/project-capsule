using UnityEngine;
using System.Collections;

public class BattleTeam
{
  public BattleUnit[] battleUnits;
	
  public void Init(FigurineModel[] models) {
    battleUnits = new BattleUnit[models.Length];

    for (int i = 0; i < battleUnits.Length; i++) {
      battleUnits[i] = new BattleUnit();
      battleUnits[i].Init(models[i]);
    }
  }

  public void DeliverAction(UnitAction action) {
    if (action.type == UnitAction.ActionType.AutoAttack) {
      BattleUnit unit = battleUnits[HighestThreatUnit()];
      unit.Damage(action.Damage);
    }
  }

  private int HighestThreatUnit() {
    int index = 0;
    int highestThreat = 0;

    for (int i = 0; i < battleUnits.Length; i++) {
      if (battleUnits[i].IsAlive && battleUnits[i].Threat >= highestThreat) {
        index = i;
        highestThreat = battleUnits[i].Threat;
      }
    }
    return index;
  }

  public bool AllDead {
    get {
      for (int i = 0; i < battleUnits.Length; i++) {
        if (battleUnits[i].IsAlive) {
          return false;
        }
      }
      return true;
    }
  }
}
