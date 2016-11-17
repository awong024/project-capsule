using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
  [SerializeField] BattleTeamView myTeamView;
  [SerializeField] BattleTeamView opponentTeamView;

  private BattleTeam myTeam;
  private BattleTeam opponentTeam;

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

  bool gameOver = false;

  public void Init(BattleTeam t1, BattleTeam t2) {
    myTeam = t1;
    opponentTeam = t2;

    myTeamView.Render(myTeam);
    opponentTeamView.Render(opponentTeam);

    StartGame();
  }

  private void StartGame() {
    gameOver = false;
    StartCoroutine(GameLoop());
  }

  private IEnumerator GameLoop() {
    while(!gameOver) {
      ProcessFrame();

      if (frameSpeed == FrameSpeed.Medium)    { yield return new WaitForSeconds(FRAME_DELAY_MEDIUM); }
      else if (frameSpeed == FrameSpeed.Fast) { yield return new WaitForSeconds(FRAME_DELAY_LOW); }
      else if (frameSpeed == FrameSpeed.Slow) { yield return new WaitForSeconds(FRAME_DELAY_HIGH); }
    }
  }

  private void ProcessFrame() {
    for (int i = 0; i < myTeam.battleUnits.Length; i++) {
      BattleUnit unit = myTeam.battleUnits[i];
      unit.ProcessFrame();

      UnitAction action = unit.ReadiedAction();
      if (action != null) {
        opponentTeam.DeliverAction(action);
      }
    }

    for (int i = 0; i < opponentTeam.battleUnits.Length; i++) {
      BattleUnit unit = opponentTeam.battleUnits[i];
      unit.ProcessFrame();

      UnitAction action = unit.ReadiedAction();
      if (action != null) {
        myTeam.DeliverAction(action);
      }
    }

    //Game Over conditions
    if (myTeam.AllDead) {
      //TODO: My team's dead
    } else if (opponentTeam.AllDead) {
      //TODO: Their team's dead
    } else if (myTeam.AllDead && opponentTeam.AllDead) {
      //TODO: Both team's dead
    }
  }
}
