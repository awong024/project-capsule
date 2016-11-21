using UnityEngine;
using System.Collections;

public enum FigurineRarity {
  Common,
  Rare,
  Mythic
}

public class FigurineModel : ScriptableObject {
  [SerializeField] int id;
  [SerializeField] FigurineRarity rarity;
  [SerializeField] string figureName;
  [SerializeField] Sprite sprite;
  [SerializeField] StatWeightModel statWeightModel;
  [SerializeField] UnitAbility ability1;
  [SerializeField] UnitAbility ability2;

  public int Id { get { return id; } }
  public FigurineRarity Rarity { get { return rarity; } }
  public string Name { get { return figureName; } }
  public Sprite Sprite { get { return sprite; } }

  public int Strength { get { return statWeightModel.Strength; } }
  public int Agility { get { return statWeightModel.Agility; } }
  public int Intellect { get { return statWeightModel.Intellect; } }
  public int Vitality { get { return statWeightModel.Vitality; } }

  public UnitAbility Ability1 { get { return ability1; } }
  public UnitAbility Ability2 { get { return ability2; } }
}
