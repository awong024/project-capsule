using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitPlacementSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] Image slotHighlight;

  private BattleHUD battleHUD;

  private BattleFigurineUnit placedUnit;

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  public void DeployCard(UnitCard card) {

  }

  private bool SlotAvailable() {
    if (placedUnit == null) {
      return true;
    }
    return false;
  }

  private void HighlightSlot(bool enable) {
    slotHighlight.gameObject.SetActive(enable);
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (battleHUD.IsCardDragging() && SlotAvailable()) {
      HighlightSlot(true);
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    HighlightSlot(false);
  }

  public void OnDrop(PointerEventData eventData) {
    if (SlotAvailable()) {
      battleHUD.CardDroppedOnSlot(this);
    }
  }
}
