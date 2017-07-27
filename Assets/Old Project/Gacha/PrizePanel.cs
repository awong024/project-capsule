using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PrizePanel : MonoBehaviour {
  [SerializeField] Text nameLabel;
  [SerializeField] Text rarityLabel;
  [SerializeField] Image figurineSprite;
  [SerializeField] Text strengthLabel;
  [SerializeField] Text agilityLabel;
  [SerializeField] Text intellectLabel;
  [SerializeField] Text vitalityLabel;

  public void Render(FigurineModel model) {
    nameLabel.text = model.Name;
    rarityLabel.text = model.Rarity.ToString();
    figurineSprite.sprite = model.Sprite;
//    strengthLabel.text = model.Strength.ToString();
//    agilityLabel.text = model.Agility.ToString();
//    intellectLabel.text = model.Intellect.ToString();
//    vitalityLabel.text = model.Vitality.ToString();
  }
}
