using UnityEngine;
using System.Collections;

public class BattleTeam
{
  public BattleUnitModel[] battleUnits;
	
  public void Init(BattleUnitModel[] battleUnits) {
    this.battleUnits = battleUnits;
  }


}
