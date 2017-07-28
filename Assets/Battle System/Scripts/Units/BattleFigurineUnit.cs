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
  }

  public void ConnectSession(BattleSession battleSession) {
    this.battleSession = battleSession;
  }

  public bool ProcessAction(int actionPoints) {
    attackTimer -= actionPoints;
    if (attackTimer <= 0) {
      attackTimer = START_ATTACK_TIMER;
      return true;
    }
    return false;
  }

  public void DealDamage(BattleFigurineUnit target) {
    target.ChangeHealth(Attack);
    Debug.Log(FigurineModel.Name + " deals " + Attack + " damage to " + target.FigurineModel.Name);
  }

  private void ChangeHealth(int delta) {
    currentHealth = Mathf.Max(currentHealth - delta, 0);
    view.UpdateHealthBar((float)currentHealth / (float)MaxHealth);

    if (currentHealth <= 0) {      
      DeleteUnit();
    }
  }

  private void DeleteUnit() {
    //Contact Battle Session to remove from system
    battleSession.UnitDeath(this);

    //Contact UnitPlacementSlot
    unitPosition.UnDeployUnit();

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
}
