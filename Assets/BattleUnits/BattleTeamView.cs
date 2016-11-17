﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleTeamView : MonoBehaviour
{
  [SerializeField] GameObject battleUnitPrefab;

  [SerializeField] GameObject[] teamSlots;

  public void Render(BattleTeam team) {
    for (int i = 0; i < team.battleUnits.Length; i++) {
      GameObject obj = GameObject.Instantiate(battleUnitPrefab) as GameObject;
      obj.transform.SetParent(teamSlots[i].transform, false);

      BattleUnitView view = obj.GetComponent<BattleUnitView>();
      view.Render(team.battleUnits[i]);
    }
  }
}