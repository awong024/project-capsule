using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
  private BattleTeam myTeam;
  private BattleTeam opponentTeam;

  public void Init(BattleTeam t1, BattleTeam t2) {
    myTeam = t1;
    opponentTeam = t2;
  }

  private void ProcessFrame() {
    for (int i = 0; i < myTeam.battleUnits.Length; i++) {
      BattleUnit unit = myTeam.battleUnits[i];
      unit.ProcessFrame();

      UnitAction action = unit.ReadiedAction();
      if (action != null) {
        opponentTeam.DeliverAction(action);
      }
    }

    for (int i = 0; i < opponentTeam.battleUnits.Length; i++) {
      BattleUnit unit = opponentTeam.battleUnits[i];
      unit.ProcessFrame();

      UnitAction action = unit.ReadiedAction();
      if (action != null) {
        myTeam.DeliverAction(action);
      }
    }
  }
}
