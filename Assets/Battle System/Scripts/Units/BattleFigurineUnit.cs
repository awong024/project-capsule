using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFigurineUnit : MonoBehaviour
{
  [SerializeField] BattleFigurineView view;

  private FigurineModel figurineModel;
  private UnitPlacementSlot unitPosition;
  private BattleSession battleSession;
  private const int START_ATTACK_TIMER = 100;

  private int currentHealth;
  private int attackTimer = START_ATTACK_TIMER;

  //Base Stats
  private int baseMaxHealth;
  private int baseAttack;
  private int baseArmor;

  //Accessors
  public int CurrentHealth  { get { return currentHealth; } }
  public int MaxHealth      { get { return figurineModel.Health; } }
  public int Attack         { get { return figurineModel.Attack; } }
  public int Armor          { get { return figurineModel.Armor; } }

  public FigurineModel FigurineModel { get { return figurineModel; } }

  public void Init(FigurineModel model, UnitPlacementSlot slot) {
    view.Render(model);

    figurineModel = model;
    unitPosition = slot;

    currentHealth = MaxHealth;

    if (figurineModel.CastAbility != null) {
      abilityCooldown = figurineModel.CastAbility.Cooldown;
    }
  }

  //Combine this process with Init in the future
  public void ConnectSession(BattleSession battleSession) {
    this.battleSession = battleSession;

    if (figurineModel.DeployAbility != null) {
      battleSession.ExecuteAbility(this, figurineModel.DeployAbility);
    }
  }

  public bool ProcessAction(int actionPoints) {
    attackTimer -= actionPoints;

    ProcessBuffs(actionPoints);
    ProcessAbilityCooldown(actionPoints);

    if (attackTimer <= 0) {
      attackTimer = START_ATTACK_TIMER;
      return true;
    }
    return false;
  }

  public void DealDamage(BattleFigurineUnit target) {
    target.ChangeHealth(Attack);
    Debug.Log(FigurineModel.Name + " deals " + Attack + " damage to " + target.FigurineModel.Name);

    view.PlayAttackAnimation();
  }

  public void DealAbilityDamage(BattleFigurineUnit target, AbilityModel ability) {
    target.ChangeHealth(ability.Effect.Power);
    target.PlayAnimation(ability.AbilityName == "Firestorm" ? "Fireball" : "Hit");
  }

  public void Heal(BattleFigurineUnit target, AbilityModel ability) {
    target.ChangeHealth(-ability.Effect.Power);
  }

  private void ChangeHealth(int delta) {
    foreach(UnitBuff buff in activeBuffs) {
      if (buff.BuffType == UnitBuffModel.BuffType.Shield && delta > 0) {        
        int absorbed = buff.CurrentPower >= delta ? delta : buff.CurrentPower;
        delta -= absorbed;
        buff.CurrentPower -= absorbed;
      }
    }

    currentHealth = Mathf.Clamp(currentHealth - delta, 0, figurineModel.Health);

    view.UpdateHealthBar((float)currentHealth / (float)MaxHealth);

    if (delta > 0) {
      view.PlayHitAnimation();
    }

    if (currentHealth <= 0) {      
      DeleteUnit();
    }
  }

  private void DeleteUnit() {
    //Contact Battle Session to remove from system
    battleSession.UnitDeath(this);

    //Contact UnitPlacementSlot
    unitPosition.UnDeployUnit();

    view.PlayDeathAnimation();

    StartCoroutine(DelayedDestroy());
  }

  private IEnumerator DelayedDestroy() {
    yield return new WaitForSeconds(1f);
    GameObject.DestroyImmediate(gameObject);
  }

  public bool IsProtected() {
    if (unitPosition != null) {      
      foreach(UnitPlacementSlot protectorSlot in unitPosition.ProtectorNodes) {
        if (protectorSlot.UnitPresent()) { 
          return true;
        }
      }
    }
    return false;
  }

  public void PlayAnimation(string anim) {
    view.PlayFX_Animation(anim);
  }

  #region Buffs

  public class UnitBuff {
    
    private UnitBuffModel model;
    private int currentDuration;
    private int currentPower;

    public UnitBuff(UnitBuffModel model) {
      this.model = model;
      currentDuration = model.BuffDuration;
      currentPower = model.BuffPower;
    }

    public string Name                      { get { return model.BuffName; } }
    public UnitBuffModel.BuffType BuffType  { get { return model.Buff_Type; } }
    public int BaseBuffDuration             { get { return model.BuffDuration; } }

    public int CurrentDuration              { get { return currentDuration; } set { currentDuration = value; } }
    public int CurrentPower                 { get { return currentPower; } set { currentPower = value; } }
  }

  public List<UnitBuff> activeBuffs = new List<UnitBuff>();

  public void AddBuff(UnitBuff buff) {
    activeBuffs.Add(buff);
    view.EnableBuffSprite(true);
  }

  public void RemoveBuff(UnitBuff buff) {
    activeBuffs.Remove(buff);
    view.EnableBuffSprite(false);
  }

  private void ProcessBuffs(int actionPoints) {
    //Tick down buffs
    List<UnitBuff> expiredBuffs = new List<UnitBuff>();

    foreach (UnitBuff buff in activeBuffs) {
      buff.CurrentDuration -= actionPoints;
      if (buff.CurrentDuration <= 0 || buff.CurrentPower <= 0) {
        expiredBuffs.Add(buff);
      }
    }

    foreach (UnitBuff buff in expiredBuffs) {
      RemoveBuff(buff);
    }
  }

  #endregion

  #region Abilities

  private int abilityCooldown = 0;

  private void ProcessAbilityCooldown(int actionPoints) {
    if (figurineModel.CastAbility != null && abilityCooldown > 0) {
      abilityCooldown -= actionPoints;
    }
    if (figurineModel.CastAbility != null && abilityCooldown <= 0) {
      view.EnableAbilityButton(true);
    }
  }

  public void UseAbility() {
    abilityCooldown = figurineModel.CastAbility.Cooldown;
    battleSession.ExecuteAbility(this, figurineModel.CastAbility);
    view.EnableAbilityButton(false);
  }

  #endregion
}
