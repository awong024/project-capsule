using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAbilityGenerator : MonoBehaviour {

  [SerializeField] BossAbilityModel[] abilities;

  [SerializeField] BattleSession battleSession;
  [SerializeField] GameObject castingBar;
  [SerializeField] Image castingBarFill;
  [SerializeField] Text castingBarText;

  private int waitDuration = 100;
  private int chargingPoints = 0;

  private int selectedIndex = 0;

  void Start() {
    SelectRandomAbility();
  }

  private void SelectRandomAbility() {
    selectedIndex = UnityEngine.Random.Range(0, abilities.Length);
    waitDuration = UnityEngine.Random.Range(100, 250);
    chargingPoints = 0;

    //Disable Charge Display
  }

  public void ProcessActionPoints(int ap) {
    
    if (waitDuration > 0) {
      waitDuration -= ap;
    } else {
      chargingPoints += ap;

      //Enable and Update Charge Display
      if (chargingPoints >= abilities[selectedIndex].ChargeTime) {
        
        //Use ability
        battleSession.ExecuteBossAbility(abilities[selectedIndex]);

        SelectRandomAbility();
      }
    }

    UpdateCastBar(chargingPoints);
  }

  private void UpdateCastBar(int chargeAP) {
    castingBar.SetActive(chargeAP > 0);
    if (chargeAP == 0) {
      return;
    }

    castingBarFill.fillAmount = (float)chargeAP / (float)abilities[selectedIndex].ChargeTime;
    castingBarText.text = abilities[selectedIndex].AbilityName;
  }

  public void InterruptCast() {
    SelectRandomAbility();
  }
}
