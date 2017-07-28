using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitPlacementSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] Image slotHighlight;

  [SerializeField] GameObject battleFigurinePrefab;
  [SerializeField] Transform figureSpawnPosition;

  private BattleHUD battleHUD;

  private BattleFigurineUnit placedUnit;

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  #region UI Interactions
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
  #endregion

  public BattleFigurineUnit DeployCard(UnitCard card) {
    GameObject figurine = GameObject.Instantiate(battleFigurinePrefab) as GameObject;
    figurine.transform.SetParent(figureSpawnPosition, false);

    BattleFigurineUnit figurineUnit = figurine.GetComponent<BattleFigurineUnit>();
    figurineUnit.Init(card.FigurineModel);

    placedUnit = figurineUnit;

    return figurineUnit;
  }

  private void UnDeployCard() {
    foreach(Transform child in figureSpawnPosition) {
      GameObject.Destroy(child);
    }
    placedUnit = null;
  }

  private bool SlotAvailable() {
    if (placedUnit == null) {
      return true;
    }
    return false;
  }
}
