using UnityEngine;
using System.Collections;

public class BattleUnit {
  private FigurineModel model;

  #region Accessors
  public FigurineModel FigurineModel { get { return model; } }
  public int CurrentHealth { get { return currentHealth; } }
  public int MaxHealth { get { return maxHealth; } }
  public int AutoAttackTimer { get { return autoAttackTimer; } }
  public int MaxAttackTimer { get { return maxAttackTimer; } }
  public int Threat { get { return threat; } }
  #endregion

  #region Stats
  public int Strength { get { return model.Strength; } }
  public int Agility { get { return model.Agility; } }
  public int Intellect { get { return model.Intellect; } }
  public int Vitality { get { return model.Vitality; } }
  #endregion

  #region Battle State
  int currentHealth;
  int maxHealth;

  int threat;

  int autoAttackTimer;
  int maxAttackTimer;

  int castTimer;
  int maxCastTimer;

  UnitAction nextAction;
  bool actionQueued = false;
  #endregion

  public void Init(FigurineModel model) {
    this.model = model;
    InitBattleState();
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
    autoAttackTimer = maxAttackTimer = StatCalculator.AttackTimerFromAgility(model.Agility);

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
      QueueAction( new UnitAction(UnitAction.ActionType.AutoAttack) );
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
