using UnityEngine;
using System.Collections;

public class FigurineModel : ScriptableObject {
  [SerializeField] int id;
  [SerializeField] string name;
  [SerializeField] Sprite sprite;
  [SerializeField] StatWeightModel statWeightModel;

  public int Id { get { return id; } }
  public string Name { get { return name; } }
  public Sprite Sprite { get { return sprite; } }

  public int Strength { get { return statWeightModel.Strength; } }
  public int Agility { get { return statWeightModel.Agility; } }
  public int Intellect { get { return statWeightModel.Intellect; } }
  public int Vitality { get { return statWeightModel.Vitality; } }
}
