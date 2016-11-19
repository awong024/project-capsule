using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleTeamController : MonoBehaviour
{
  [SerializeField] BattleUnitController[] battleUnitControllers;

  private BattleTeamController opposingTeam;
	
  public void Init(FigurineModel[] models) {
    for (int i = 0; i < models.Length; i++) {
      battleUnitControllers[i].Init(models[i]);
    }
  }

  public void ProcessFrame() {
    for (int i = 0; i < battleUnitControllers.Length; i++) {
      UnitAction action = battleUnitControllers[i].ProcessFrame();
      if (action != null) {
        action.unit = battleUnitControllers[i].BattleUnit;
        PackageAction(action, battleUnitControllers[i]);
      }
    }
  }

  private void PackageAction(UnitAction action, BattleUnitController owner) {
    List<BattleUnitController> targets = new List<BattleUnitController>();

    if (action.type == UnitAction.ActionType.AutoAttack) {
      targets.Add(opposingTeam.HighestThreatUnit());
      owner.AnimateFX(ActionFX.AutoAttack);
    }

    opposingTeam.DeliverAction(action, targets);
  }

  public void DeliverAction(UnitAction action, List<BattleUnitController> targets) {
    for (int i = 0; i < targets.Count; i++) {
      targets[i].ReceiveAction(action);
    }
  }

  public BattleUnitController HighestThreatUnit() {
    int index = 0;
    int highestThreat = 0;

    for (int i = 0; i < battleUnitControllers.Length; i++) {
      if (battleUnitControllers[i].BattleUnit.IsAlive && battleUnitControllers[i].BattleUnit.Threat >= highestThreat) {
        highestThreat = battleUnitControllers[i].BattleUnit.Threat;
        index = i;
      }
    }
    return battleUnitControllers[index];
  }

//  public bool AllDead {
//    get {
//      for (int i = 0; i < battleUnits.Length; i++) {
//        if (battleUnits[i].IsAlive) {
//          return false;
//        }
//      }
//      return true;
//    }
//  }
}
