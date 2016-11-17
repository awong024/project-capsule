using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUnitView : MonoBehaviour
{
  [SerializeField] Image image;

  private BattleUnit battleUnit;

  public void Render(BattleUnit battleUnit) {
    this.battleUnit = battleUnit;
    image.sprite = battleUnit.FigurineModel.Sprite;
  }

  private void Update() { 
    //Update UI
  }
}
