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

  public int Damage {
    get
    {
      if (type == ActionType.AutoAttack) {
        return StatCalculator.AutoDamageFromStrength(model.Strength);
      }
      return 0;
    }
  }
}

public class BattleUnit {
  private FigurineModel model;

  #region Accessors
  public FigurineModel FigurineModel { get { return model; } }
  public int Threat { get { return threat; } }
  #endregion

  #region Battle State
  int currentHealth;
  int maxHealth;

  int threat;

  int autoAttackTimer;
  int castTimer;

  UnitAction nextAction;
  bool actionQueued = false;
  #endregion

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
    SetAutoAttackTimer(true);
    castTimer = 0;
    actionQueued = false;
  }

  #region Timers
  //Reset AutoAttack Timer
  private void SetAutoAttackTimer(bool randomHeadstart = false) {
    autoAttackTimer = StatCalculator.AttackTimerFromAgility(model.Agility);

    //At battle start, units will stagger auto-attack timers slightly
    if (randomHeadstart) {
      autoAttackTimer += UnityEngine.Random.Range( -10, 10 );
    }
  }

  //Each Frame: Decrement AutoAttack timer by 1 (unless casting)
  private void ProcessAutoAttackTimer() {
    //Freeze autoattack while casting
    if (castTimer > 0) {
      return;
    }

    autoAttackTimer--;
  }

  private void StartCastTime() {

  }

  //Each Frame: Decrement Cast timer by 1 while queuing up Ability
  private void ProcessCastTimer() {

  }
  #endregion

  #region Health
  public bool IsAlive { get { return currentHealth > 0; } }

  private void SetMaxHealth() {
    currentHealth = maxHealth = StatCalculator.HealthFromVitality(model.Vitality);
  }

  public void Damage(int amount) {
    currentHealth = Mathf.Max( currentHealth - amount, 0 );
  }

  public void Heal(int amount) {
    currentHealth = Mathf.Min( currentHealth + amount, maxHealth );
  }
  #endregion

  private void QueueAction(UnitAction action) {
    nextAction = action;
    actionQueued = true;
  }

  //Processing Acting Turn
  public void ProcessFrame() {
    if (!IsAlive) {
      return;
    }

    //Auto-Attack
    if (autoAttackTimer <= 0) {
      QueueAction( new UnitAction(model, UnitAction.ActionType.AutoAttack, 0) );
      SetAutoAttackTimer();
    } else {
      ProcessAutoAttackTimer();
    }
  }

  public UnitAction ReadiedAction() {
    if (actionQueued) {
      actionQueued = false;
      return nextAction;
    } else {
      return null;
    }
  }
}
