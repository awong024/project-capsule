using UnityEngine;
using System.Collections;

public class BattleTestHarness : MonoBehaviour
{
  [SerializeField] BattleSystem battleSystem;
  [SerializeField] FigurineModel[] myTeam;
  [SerializeField] FigurineModel[] opponentTeam;

  void Start() {
    BattleTeam homeTeam = new BattleTeam();
    homeTeam.Init(myTeam);

    BattleTeam awayTeam = new BattleTeam();
    awayTeam.Init(opponentTeam);

    battleSystem.Init(homeTeam, awayTeam);
  }
}
