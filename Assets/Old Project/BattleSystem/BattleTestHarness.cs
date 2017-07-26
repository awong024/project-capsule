using UnityEngine;
using System.Collections;

public class BattleTestHarness : MonoBehaviour
{
  [SerializeField] BattleSystem battleSystem;
  [SerializeField] FigurineModel[] homeTeam;
  [SerializeField] FigurineModel[] awayTeam;

  public void StartBattle() {
    battleSystem.Init(homeTeam, awayTeam);
  }
}
