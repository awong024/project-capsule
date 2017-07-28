using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
  [SerializeField] BattleHUD battleHUD;
  [SerializeField] BattleSession battleSession;

  void Start() {
    LaunchBattle();
  }

  private void LaunchBattle() {
    battleHUD.Init(this);
    battleSession.Init(this, battleHUD.UnitNodes);

    battleSession.ResumeGame();
  }

  public void TryDeployUnit(UnitCard card, UnitPlacementSlot slot) {
    if (battleSession.CanDeployUnit(card)) {
      BattleFigurineUnit createdBFU = battleHUD.ConfirmDeployUnit(card, slot);
      battleSession.DeployUnit(createdBFU);
    }
  }
}
