using UnityEngine;
using System.Collections;

public class BattleUnitModel {
  private FigurineModel model;

  //Battle State
  int currentHealth;
  int maxHealth;

  public void Init(FigurineModel model) {
    this.model = model;

    Render(model);
    InitBattleState();
  }

  private void Render(FigurineModel model) {
    //TODO: Render View
  }

  private void InitBattleState() {
    currentHealth = maxHealth = StatCalculator.HealthFromVitality(model.Vitality);
  }
}
