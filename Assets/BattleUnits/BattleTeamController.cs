using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BattleTeamController : MonoBehaviour
{
  [SerializeField] BattleTeamView battleTeamView;
  [SerializeField] UnitFrame[] unitFrames;
  [SerializeField] Animator teamAnimator;

  private BattleUnitController[] battleUnitControllers;

  public BattleTeamController opposingTeam { private get; set; }
	
  public void Init(FigurineModel[] models) {
    battleUnitControllers = new BattleUnitController[models.Length];

    //Create units from prefab into slots and retrieve their controllers
    battleTeamView.Render(models, ref battleUnitControllers);

    for (int i = 0; i < models.Length; i++) {
      battleUnitControllers[i].Init(models[i]);
    }

    //Assign Unit Frames to units
    for (int i = 0; i < battleUnitControllers.Length && i < unitFrames.Length; i++) {
      unitFrames[i].Init(battleUnitControllers[i]);
    }
  }

  public void ProcessFrame() {
    for (int i = 0; i < battleUnitControllers.Length; i++) {
      UnitAction action = battleUnitControllers[i].ProcessFrame();
      if (action != null) {
        action.unit = battleUnitControllers[i].BattleUnit;
        PackageAction(action, battleUnitControllers[i]);
      }

      if (unitFrames.Length > i) {
        unitFrames[i].Render();
      }
    }
  }

  private void PackageAction(UnitAction action, BattleUnitController owner) {
    List<BattleUnitController> targets = new List<BattleUnitController>();

    if (action.type == UnitAction.ActionType.AutoAttack) {
      targets.Add(opposingTeam.HighestThreatUnit());
      owner.AnimateFX(ActionFX.AutoAttack);
    }
    else if (action.type == UnitAction.ActionType.Ability) {
      if (action.unitAbility.AbilityEffect == UnitAbility.EffectType.Damage) {
        //Multi-target
        for (int i = 0; i < action.unitAbility.NumTargets && i < opposingTeam.battleUnitControllers.Length; i++) {
          targets.Add(opposingTeam.battleUnitControllers[i]);
          owner.AnimateFX(ActionFX.CastSpell);
        }
        if (action.unitAbility.AnimationName == "MegaSlash") {
          AnimateTeamFX(action.unitAbility.AnimationName);
        } else {
          opposingTeam.AnimateTeamFX(action.unitAbility.AnimationName);
        }
      } else if (action.unitAbility.AbilityEffect == UnitAbility.EffectType.Healing) {
        targets.Add(this.LowestHealthUnit());
        owner.AnimateFX(ActionFX.CastSpell);
      }
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

  public BattleUnitController LowestHealthUnit() {
    int index = 0;
    int lowestHealth = 99999;

    for (int i = 0; i < battleUnitControllers.Length; i++) {
      if (battleUnitControllers[i].BattleUnit.IsAlive && battleUnitControllers[i].BattleUnit.CurrentHealth < lowestHealth) {
        lowestHealth = battleUnitControllers[i].BattleUnit.CurrentHealth;
        index = i;
      }
    }
    return battleUnitControllers[index];
  }

  public bool AllDead {
    get {
      for (int i = 0; i < battleUnitControllers.Length; i++) {
        if (battleUnitControllers[i].BattleUnit.IsAlive) {
          return false;
        }
      }
      return true;
    }
  }

  public void AnimateTeamFX(string fx) {
    teamAnimator.SetTrigger(fx);
  }
}
