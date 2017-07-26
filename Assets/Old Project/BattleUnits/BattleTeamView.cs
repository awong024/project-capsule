using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleTeamView : MonoBehaviour
{
  [SerializeField] GameObject battleUnitPrefab;

  [SerializeField] GameObject[] teamSlots;

  public void Render(FigurineModel[] figurines, ref BattleUnitController[] controllers) {
    for (int i = 0; i < figurines.Length; i++) {
      GameObject obj = GameObject.Instantiate(battleUnitPrefab) as GameObject;
      obj.transform.SetParent(teamSlots[i].transform, false);

      controllers[i] = obj.GetComponent<BattleUnitController>();
    }
  }
}
