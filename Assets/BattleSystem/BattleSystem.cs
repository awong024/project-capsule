using UnityEngine;
using System.Collections;

public class BattleSystem : MonoBehaviour
{
  [SerializeField] BattleTeamController homeTeam;
  [SerializeField] BattleTeamController awayTeam;

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

  public void Init(FigurineModel[] t1, FigurineModel[] t2) {
    homeTeam.Init(t1);
    awayTeam.Init(t2);

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
    homeTeam.ProcessFrame();
    awayTeam.ProcessFrame();

    //Game Over conditions
//    if (myTeam.AllDead) {
//      //TODO: My team's dead
//    } else if (opponentTeam.AllDead) {
//      //TODO: Their team's dead
//    } else if (myTeam.AllDead && opponentTeam.AllDead) {
//      //TODO: Both team's dead
//    }
  }
}
