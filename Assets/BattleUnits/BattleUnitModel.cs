using UnityEngine;
using System.Collections;

public class UnitAction
{
  public enum ActionType {
    AutoAttack,
    Ability1,
    Ability2
  }

  public FigurineModel model;
  public ActionType type;
  public int target; //0 = no target

  public UnitAction(FigurineModel model, ActionType type, int target = 0) {
    this.model = model;
    this.type = type;
    this.target = target;
  }
}

public class BattleUnitModel {
  private FigurineModel model;

  //Battle State
  int currentHealth;
  int maxHealth;

  int threat;

  int autoAttackTimer;
  int castTimer;

  UnitAction nextAction;
  bool actionQueued = false;

  public void Init(FigurineModel model) {
    this.model = model;

    Render(model);
    InitBattleState();
  }

  private void Render(FigurineModel model) {
    //TODO: Render View
  }

  private void InitBattleState() {
    SetMaxHealth();
    SetAutoAttackTimer();
    actionQueued = false;
  }

  private void SetMaxHealth() {
    currentHealth = maxHealth = StatCalculator.HealthFromVitality(model.Vitality);
  }

  //Reset AutoAttack Timer
  private void SetAutoAttackTimer() {
    autoAttackTimer = StatCalculator.AttackTimerFromAgility(model.Agility);
  }

  //Each Frame: Decrement AutoAttack timer by 1 (unless casting)
  private void ProcessAutoAttackTimer() {

  }

  private void StartCastTime() {

  }

  //Each Frame: Decrement Cast timer by 1 while queuing up Ability
  private void ProcessCastTimer() {

  }

  private void QueueAction(UnitAction action) {
    nextAction = action;
    actionQueued = true;
  }

  //Processing Acting Turn
  public UnitAction CheckForAction() {
    if (actionQueued) {
      return nextAction;
    } else {
      return null;
    }
  }
}
