using UnityEngine;
using System.Collections;

public class BattleTeam
{
  public BattleUnit[] battleUnits;
	
  public void Init(BattleUnit[] battleUnits) {
    this.battleUnits = battleUnits;
  }

  public void DeliverAction(UnitAction action) {

  }
}
