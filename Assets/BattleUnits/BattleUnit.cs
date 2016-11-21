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
  public int Energy { get { return energy; } }
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

  int energy;

  const int MAX_ENERGY = 1000;

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
    SetEnergyTimer(0);
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

  private void SetEnergyTimer(int amount) {
    energy = amount;
  }

  private void ProcessEnergyTimer() {
    energy = Mathf.Min( MAX_ENERGY, energy + StatCalculator.EnergyPerTurnFromAgilityAndIntellect(model.Agility, model.Intellect) );
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

  //Use Ability from UnitFrame
  public void UseAbility(int num) {
    if (energy >= MAX_ENERGY) {
      if (num == 1 && FigurineModel.Ability1 != null) {
        QueueAction( new UnitAction(FigurineModel.Ability1) );
        SetEnergyTimer(0);
      } else if (num == 2 && FigurineModel.Ability2 != null) {
        QueueAction( new UnitAction(FigurineModel.Ability2) );
        SetEnergyTimer(0);
      }
    }
  }

  //Processing Acting Turn
  public void ProcessFrame() {
    if (!IsAlive) {
      return;
    }

    //Ability Queued
    if (actionQueued) {
      return;
    }

    //Auto-Attack
    if (autoAttackTimer <= 0) {
      QueueAction( new UnitAction(UnitAction.ActionType.AutoAttack) );
      threat += 5; //for testing
      SetAutoAttackTimer();
    } else {
      ProcessAutoAttackTimer();
    }

    //Gain Energy
    ProcessEnergyTimer();
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
