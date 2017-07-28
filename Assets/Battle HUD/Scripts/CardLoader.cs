using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardLoader : MonoBehaviour {
  [SerializeField] FigurineModel[] modelList;

  [SerializeField] GameObject cardPrefab;
  [SerializeField] Transform unitBar;

  private const int MAX_CARD_SLOTS = 4;

  private int cardCount = 0;

  private BattleHUD battleHUD;

  public void Init(BattleHUD battleHUD) {
    this.battleHUD = battleHUD;
  }

  public void DealStartingHand() {
    for (int i = 0; i < MAX_CARD_SLOTS; i++) {
      AddCardToBelt();
    }
  }

  public void AddCardToBelt() {
    if (cardCount >= MAX_CARD_SLOTS) {
      return;
    }

    GameObject card = GameObject.Instantiate(cardPrefab) as GameObject;
    card.transform.SetParent(unitBar, false);

    //Randomize Card
    int index = UnityEngine.Random.Range(0, modelList.Length);
    UnitCard unitCard = card.GetComponent<UnitCard>();
    unitCard.Init(battleHUD, modelList[index]);

    cardCount++;
  }

  public void PlayCard(UnitCard card) {
    card.PlayCard();
    cardCount--;
  }
}
