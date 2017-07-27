using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {
  [SerializeField] UnitPlacementSlot[] unitSlots;

  public UnitCard heldCard = null;
  private bool isCardDragging = false;
	
  void Start() {
    foreach(UnitPlacementSlot slot in unitSlots) {
      slot.Init(this);
    }

    //Testing
    heldCard.Init(this, null);
  }

  public void CardDragging(UnitCard card) {
    heldCard = card;
    isCardDragging = true;
  }

  public bool IsCardDragging() {
    return isCardDragging;
  }

  public void StopCardDragging() {
    isCardDragging = false;
  }

  public void CardDroppedOnSlot(UnitPlacementSlot slot) {
    //TODO: Check validity of Deploy
    if (true) {
      heldCard.PlayCard();
      slot.DeployCard(heldCard);
    }
  }
}
