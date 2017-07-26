using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
  private BattleHUD battleHUD;

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  public void DeployToSlot(UnitPlacementSlot slot) {
    
  }

  //UI Dragging Mechanics
  private Vector3 startDragPosition;

  public void OnBeginDrag(PointerEventData eventData) {
    RectTransform rect = GetComponent<RectTransform>();
    startDragPosition = rect.position;

    battleHUD.CardDragging(this);
    GetComponent<CanvasGroup>().blocksRaycasts = false;
  }

  public void OnDrag(PointerEventData eventData) {
    RectTransform rect = GetComponent<RectTransform>();
    var currentPosition = rect.position;
    currentPosition.x += eventData.delta.x;
    currentPosition.y += eventData.delta.y;
    rect.position = currentPosition;
  }

  public void OnEndDrag(PointerEventData eventData) {
    RectTransform rect = GetComponent<RectTransform>();
    rect.position = startDragPosition;

    GetComponent<CanvasGroup>().blocksRaycasts = true;
  }
}
