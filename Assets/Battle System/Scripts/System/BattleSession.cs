﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSession : MonoBehaviour
{
  private BattleController battleController;

  public void Init(BattleController battleController, UnitPlacementSlot[] unitNodes) {
    this.battleController = battleController;
    this.unitNodes = unitNodes;
    StartCoroutine(GameLoop());
  }

  #region FrameProcessing
  public enum FrameSpeed {
    Medium,
    Fast,
    Slow
  }

  //FRAME RATE -> Average AutoAttack ~= 20 frames
  const float FRAME_DELAY_MEDIUM = 0.2f; // 5 FPS = 4 seconds per auto-attack
  const float FRAME_DELAY_LOW = 0.125f; // 8 FPS = 2.5 seconds per auto-attack
  const float FRAME_DELAY_HIGH = 0.3f;  // 3.33 FPS = 6 seconds per auto-attack

  private FrameSpeed frameSpeed = FrameSpeed.Medium;

  private bool running = false;

  public void ResumeGame() { running = true; }
  public void PauseGame() { running = false; }

  private IEnumerator GameLoop() {
    while(true) {
      if (running) {
        ProcessFrame();

        if (frameSpeed == FrameSpeed.Medium)    { yield return new WaitForSeconds(FRAME_DELAY_MEDIUM); }
        else if (frameSpeed == FrameSpeed.Fast) { yield return new WaitForSeconds(FRAME_DELAY_LOW); }
        else if (frameSpeed == FrameSpeed.Slow) { yield return new WaitForSeconds(FRAME_DELAY_HIGH); }
      } else {
        yield return new WaitForEndOfFrame();
      }
    }
  }

  private void ProcessFrame() {
    DistributeActionPoints(4);
    CheckEndGame();
  }
  #endregion

  #region Unit Management
  private List<BattleFigurineUnit> battleUnits = new List<BattleFigurineUnit>();
  [SerializeField] BattleFigurineUnit bossUnit; //Test only, load in boss through BattleController
  [SerializeField] FigurineModel bossModel; //Test only
  [SerializeField] BossAbilityGenerator bossAbilityGenerator; //Test only

  private UnitPlacementSlot[] unitNodes;

  //Test load boss unit, Remove
  void Start() {
    bossUnit.Init(bossModel, null);
  }

  public bool CanDeployUnit(UnitCard card) {
    return manaSystem.PlayUnit(card.DeployCost);
  }

  public void DeployUnit(BattleFigurineUnit unit) {
    unit.ConnectSession(this);
    battleUnits.Add(unit);
  }

  public void RemoveUnit(BattleFigurineUnit unit) {
    battleUnits.Remove(unit);
  }

  private List<BattleFigurineUnit> UnProtectedUnits() {
    List<BattleFigurineUnit> unprotectedUnits = new List<BattleFigurineUnit>();
    foreach(BattleFigurineUnit unit in battleUnits) {
      if (!unit.IsProtected()) {
        unprotectedUnits.Add(unit);
      }
    }
    return unprotectedUnits;
  }

  private List<BattleFigurineUnit> SelectUnProtectedUnits(List<BattleFigurineUnit> unitPool, int numTargets) {
    List<BattleFigurineUnit> selectedUnits = new List<BattleFigurineUnit>();
    for (int i = 0; i < numTargets; i++) {
      if (unitPool.Count > 0) {
        int index = UnityEngine.Random.Range(0, unitPool.Count);
        selectedUnits.Add(unitPool[index]);
        unitPool.RemoveAt(index);
      }
    }
    return selectedUnits;
  }

  public void UnitDeath(BattleFigurineUnit unit) {
    RemoveUnit(unit);
  }

  public void UseUnitAbility(BattleFigurineUnit unit) {

  }
  #endregion

  #region Battle System
  private void DistributeActionPoints(int actionPoints) {
    foreach(BattleFigurineUnit unit in battleUnits) {
      if (unit.ProcessAction(actionPoints)) {
        unit.DealDamage(bossUnit);
      }
    }
    if (battleUnits.Count > 0) {
      if (bossUnit.ProcessAction(actionPoints * 2)) {
        foreach(BattleFigurineUnit battleUnit in SelectUnProtectedUnits( UnProtectedUnits(), 1 )) {
          bossUnit.DealDamage(battleUnit);
        }
      }
      bossAbilityGenerator.ProcessActionPoints(actionPoints);
    }

    DistributeMana();
  }

  public void ExecuteAbility(BattleFigurineUnit sourceUnit, AbilityModel ability) {
    List<BattleFigurineUnit> targets = new List<BattleFigurineUnit>();

    if (ability.Target_Mode == AbilityModel.TargetMode.Enemy) {
      targets.Add(bossUnit);
    } else if (ability.Target_Mode == AbilityModel.TargetMode.Team) {
      foreach(BattleFigurineUnit unit in battleUnits) {
        targets.Add(unit);
      }
    } else if (ability.Target_Mode == AbilityModel.TargetMode.Self) {
      targets.Add(sourceUnit);
    }

    foreach(BattleFigurineUnit unit in targets) {
      ApplyAbilityToUnit(ability, sourceUnit, unit);
    }
  }

  private void ApplyAbilityToUnit(AbilityModel ability, BattleFigurineUnit sourceUnit, BattleFigurineUnit target) {
    if (ability.Effect != null) {
      if (ability.Effect.Health_Effect == AbilityEffectModel.HealthEffect.Damage) {
        sourceUnit.DealDamage(target);
      } else if (ability.Effect.Health_Effect == AbilityEffectModel.HealthEffect.Heal) {
        sourceUnit.Heal(target, ability);
      } else if (ability.Effect.Health_Effect == AbilityEffectModel.HealthEffect.Interrupt) {
        bossAbilityGenerator.InterruptCast();
      }
    }

    if (ability.GeneratedBuff != null) {
      target.AddBuff( new BattleFigurineUnit.UnitBuff(ability.GeneratedBuff) );
    }
  }

  private void CheckEndGame() {
    if (manaSystem.IsTimerUp()) {
      GameOver(win:false);
    }
    if (bossUnit.CurrentHealth <= 0) {
      GameOver(win:true);
    }
  }

  private void GameOver(bool win) {
    PauseGame();
    manaSystem.DisplayGameOver(win);
  }

  #endregion

  #region Boss Mechanics

  //Setup a better system later
  public void ExecuteBossAbility(BossAbilityModel ability) {
    List<BattleFigurineUnit> targets = new List<BattleFigurineUnit>();

    if (ability.AbilityName == "Firestorm") {
      foreach(BattleFigurineUnit unit in battleUnits) {
        targets.Add(unit);
      }
    } else if (ability.AbilityName == "Crushing Blow") {
      if (battleUnits.Count > 0) {
        int randIndex = UnityEngine.Random.Range(0, UnProtectedUnits().Count);
        targets.Add(UnProtectedUnits()[randIndex]);
      }
    }

    foreach(BattleFigurineUnit unit in targets) {
      bossUnit.DealAbilityDamage(unit, ability);
    }
  }

  #endregion

  #region Mana System
  [SerializeField] ManaSystem manaSystem;
  [SerializeField] GameObject helpPopup;

  private void DistributeMana() {
    manaSystem.GainMana();
  }

  public void ToggleHelp() {
    if (!helpPopup.activeSelf) {
      PauseGame();
      manaSystem.PauseTimer();
      helpPopup.SetActive(true);
    } else {
      ResumeGame();
      manaSystem.ResumeTimer();
      helpPopup.SetActive(false);
    }
  }

  #endregion
}
