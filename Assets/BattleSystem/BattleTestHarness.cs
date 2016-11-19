using UnityEngine;
using System.Collections;

public class BattleTestHarness : MonoBehaviour
{
  [SerializeField] BattleSystem battleSystem;
  [SerializeField] FigurineModel[] homeTeam;
  [SerializeField] FigurineModel[] awayTeam;

  void Start() {
    battleSystem.Init(homeTeam, awayTeam);
  }
}
