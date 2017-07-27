using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSession : MonoBehaviour
{
  private BattleController battleController;

  public void Init(BattleController battleController) {
    this.battleController = battleController;
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
  }
  #endregion

  #region Unit Management
  private List<BattleFigurineUnit> battleUnits = new List<BattleFigurineUnit>();
  [SerializeField] BattleFigurineUnit bossUnit; //Test only, load in boss through BattleController

  public bool CanDeployUnit(UnitCard card) {
    //TODO: Determine if card can be played
    return true;
  }

  public void DeployUnit(BattleFigurineUnit unit) {
    battleUnits.Add(unit);
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
        bossUnit.DealDamage(battleUnits[0]);
      }
    }
  }

  #endregion
}
