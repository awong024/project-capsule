using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour {
  [SerializeField] CardLoader cardLoader;
  [SerializeField] UnitPlacementSlot[] unitSlots;

  private BattleController battleController;

  private UnitCard heldCard = null;
  private bool isCardDragging = false;

  public UnitPlacementSlot[] UnitNodes { get { return unitSlots; } }
	
  public void Init(BattleController battleController) {
    this.battleController = battleController;

    cardLoader.Init(this);

    foreach(UnitPlacementSlot slot in unitSlots) {
      slot.Init(this);
    }

    cardLoader.DealStartingHand();
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

    cardLoader.PlayCard(heldCard);
    cardLoader.AddCardToBelt();

    StopCardDragging();

    return BFU;
  }
}
