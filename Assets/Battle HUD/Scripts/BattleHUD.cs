using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {
  [SerializeField] UnitPlacementSlot[] unitSlots;

  public UnitCard heldCard = null; 
	
  void Start() {
    foreach(UnitPlacementSlot slot in unitSlots) {
      slot.Init(this);
    }

    //Testing
    heldCard.Init(this);
  }

  public void CardDragging(UnitCard card) {
    heldCard = card;
  }

  public void CardDroppedOnSlot(UnitPlacementSlot slot) {
    heldCard.DeployToSlot(slot);
  }
}
