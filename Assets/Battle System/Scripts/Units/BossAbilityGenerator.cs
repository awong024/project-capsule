using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAbilityGenerator : MonoBehaviour {

  [SerializeField] BossAbilityModel[] abilities;

  private int waitDuration = 100;
  private int chargingDuration = 0;

  private int selectedIndex = 0;

  void Start() {
    SelectRandomAbility();
  }

  private void SelectRandomAbility() {
    selectedIndex = UnityEngine.Random.Range(0, abilities.Length);
    waitDuration = UnityEngine.Random.Range(100, 250);
    chargingDuration = 0;

    //Disable Charge Display
  }

  public void ProcessActionPoints(int ap) {
    if (waitDuration > 0) {
      waitDuration -= ap;
    } else {
      chargingDuration += ap;

      //Enable and Update Charge Display
      if (chargingDuration >= abilities[selectedIndex].ChargeTime) {
        
        //Use ability

        SelectRandomAbility();
      }
    }
  }
}
