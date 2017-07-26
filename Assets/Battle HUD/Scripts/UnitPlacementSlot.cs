using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitPlacementSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] Image slotHighlight;

  private BattleHUD battleHUD;

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  private void HighlightSlot(bool enable) {
    slotHighlight.gameObject.SetActive(enable);
  }

  public void OnPointerEnter(PointerEventData eventData) {
    HighlightSlot(true);
  }

  public void OnPointerExit(PointerEventData eventData) {
    HighlightSlot(false);
  }

  public void OnDrop(PointerEventData eventData) {
    Debug.Log("Dropped on!");
    battleHUD.CardDroppedOnSlot(this);
  }
}
