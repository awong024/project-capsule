using UnityEngine;
using System.Collections;

public class StatWeightModel : ScriptableObject {
  [SerializeField] int strength;
  [SerializeField] int agility;
  [SerializeField] int intellect;
  [SerializeField] int vitality;

  public int Strength { get { return strength; } }
  public int Agility { get { return agility; } }
  public int Intellect { get { return intellect; } }
  public int Vitality { get { return vitality; } }
}
