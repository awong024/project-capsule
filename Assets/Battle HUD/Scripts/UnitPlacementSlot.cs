using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitPlacementSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
  [SerializeField] bool headNode = false;
  [SerializeField] UnitPlacementSlot[] protectorNodes;

  [SerializeField] Image slotHighlight;

  [SerializeField] GameObject battleFigurinePrefab;
  [SerializeField] Transform figureSpawnPosition;

  private BattleHUD battleHUD;

  private BattleFigurineUnit placedUnit;

  public BattleFigurineUnit PlacedUnit { get { return placedUnit; } }
  public bool HeadNode { get { return headNode; } }
  public UnitPlacementSlot[] ProtectorNodes { get { return protectorNodes; } }

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  #region UI Interactions
  private void HighlightSlot(bool enable) {
    slotHighlight.gameObject.SetActive(enable);
  }

  public void OnPointerEnter(PointerEventData eventData) {
    if (battleHUD.IsCardDragging() && !UnitPresent()) {
      HighlightSlot(true);
    }
  }

  public void OnPointerExit(PointerEventData eventData) {
    HighlightSlot(false);
  }

  public void OnDrop(PointerEventData eventData) {
    if (!UnitPresent()) {
      battleHUD.CardDroppedOnSlot(this);
    }
  }
  #endregion

  public BattleFigurineUnit DeployCard(UnitCard card) {
    GameObject figurine = GameObject.Instantiate(battleFigurinePrefab) as GameObject;
    figurine.transform.SetParent(figureSpawnPosition, false);

    BattleFigurineUnit figurineUnit = figurine.GetComponent<BattleFigurineUnit>();
    figurineUnit.Init(card.FigurineModel, this);

    placedUnit = figurineUnit;

    return figurineUnit;
  }

  public void UnDeployUnit() {
    placedUnit = null;
  }

  public bool UnitPresent() {
    if (placedUnit != null) {
      return true;
    }
    return false;
  }
}
