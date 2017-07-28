using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {
  [SerializeField] UnitPlacementSlot[] unitSlots;

  //Testing only
  [SerializeField] UnitCard[] firstCards;

  private BattleController battleController;

  private UnitCard heldCard = null;
  private bool isCardDragging = false;
	
  public void Init(BattleController battleController) {
    this.battleController = battleController;

    foreach(UnitPlacementSlot slot in unitSlots) {
      slot.Init(this);
    }

    //Testing only
    foreach(UnitCard card in firstCards) {
      card.Init(this);
    }
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
    battleController.TryDeployUnit(heldCard, slot);
  }

  public BattleFigurineUnit ConfirmDeployUnit(UnitCard card, UnitPlacementSlot slot) {    
    BattleFigurineUnit BFU = slot.DeployCard(card);
    heldCard.PlayCard();

    StopCardDragging();

    return BFU;
  }
}
