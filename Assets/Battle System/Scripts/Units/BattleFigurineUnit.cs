using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleFigurineUnit : MonoBehaviour
{
  [SerializeField] BattleFigurineView view;

  private FigurineModel figurineModel;
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

  public void Init(FigurineModel model) {
    view.Render(model);

    figurineModel = model;
    currentHealth = MaxHealth;
  }

  public bool ProcessAction(int actionPoints) {
    attackTimer -= actionPoints;
    Debug.Log("AttackTimer: " + attackTimer);
    if (attackTimer <= 0) {
      attackTimer = START_ATTACK_TIMER;
      return true;
    }
    return false;
  }

  public void DealDamage(BattleFigurineUnit target) {
    target.ChangeHealth(20);
//    Debug.Log("Deal " + figurineModel.Attack + " damage");
  }

  public void ChangeHealth(int delta) {
    currentHealth = Mathf.Max(currentHealth - delta, 0);
    view.UpdateHealthBar((float)currentHealth / (float)MaxHealth);
  }
}
